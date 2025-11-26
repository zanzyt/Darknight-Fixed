using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DarkNight;
using SDG.NetPak;
using SDG.NetTransport;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
    // Token: 0x02000050 RID: 80
    public class PlayerCoroutines : MonoBehaviour
    {
        // Перехват вызова Player.ReceiveTakeScreenshot
        [Override(typeof(Player), "ReceiveTakeScreenshot", BindingFlags.Instance | BindingFlags.Public)]
        public static void OV_ReceiveTakeScreenshot(Player __instance)
        {
            try
            {
                // Запускаем coroutine на инстансе игрока — безопасно и совместимо с Unturned
                __instance.StartCoroutine(TakeScreenshot(__instance));
            }
            catch (Exception ex)
            {
                Debug.Log($"[PlayerCoroutines] OV_ReceiveTakeScreenshot exception: {ex}");
            }
        }

        // Основной корутин для создания/обработки скриншота
        public static IEnumerator TakeScreenshot(Player player)
        {
            if (player == null)
                yield break;

            // защита от частых запросов
            if (Time.realtimeSinceStartup - PlayerCoroutines.LastSpy < 0.3f || G.BeingSpied)
            {
                yield break;
            }

            G.BeingSpied = true;
            PlayerCoroutines.LastSpy = Time.realtimeSinceStartup;

            // Если используется режим, который прячет визуалы — вызываем его (как в оригинале)
            if (!MiscOptions.PanicMode)
            { //That is Darknight. 
                DisableAllVisuals();
            }

            // Подождать конец кадра — чтобы получить готовый рендер
            yield return new WaitForEndOfFrame();

            Texture2D raw = null;
            Texture2D resized = null;

            try
            {
                // захват экрана
                raw = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
                {
                    name = "Screenshot_Raw",
                    hideFlags = HideFlags.HideAndDontSave
                };
                raw.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0, false);
                raw.Apply();

                // ресайз в 640x480 (как в предыдущем рабочем коде)
                int targetW = 640;
                int targetH = 480;
                resized = new Texture2D(targetW, targetH, TextureFormat.RGB24, false)
                {
                    name = "Screenshot_Final",
                    hideFlags = HideFlags.HideAndDontSave
                };

                Color[] srcPixels = raw.GetPixels();
                Color[] dstPixels = new Color[targetW * targetH];

                float scaleX = (float)raw.width / (float)targetW;
                float scaleY = (float)raw.height / (float)targetH;

                for (int y = 0; y < targetH; y++)
                {
                    int srcRow = (int)(y * scaleY) * raw.width;
                    int dstRow = y * targetW;
                    for (int x = 0; x < targetW; x++)
                    {
                        int srcCol = (int)(x * scaleX);
                        dstPixels[dstRow + x] = srcPixels[srcRow + srcCol];
                    }
                }

                resized.SetPixels(dstPixels);
                resized.Apply();

                // опциональная нотификация/логика из настроек
                if (MiscOptions.AlertOnSpy)
                {
                    MiscOptions.spynofity = true;
                    // запускаем корутин нотификации на главном игроке, если он доступен
                    if (OptimizationVariables.MainPlayer != null)
                        OptimizationVariables.MainPlayer.StartCoroutine(writeinscreen());
                }

                // Отправляем данные серверу (через channel) — безопасно и работает в EgguWare-подходе 
                byte[] jpg = resized.EncodeToJPG(33); 
                if (jpg != null && jpg.Length < 30000)
                {
                    // Если сервер — локальный / dedicated, можно вызвать локальную обработку
                    if (Provider.isServer)
                    {
                        _HandleScreenshotData(jpg, player.channel);
                    }
                    else
                    {
                        try
                        {
                            // Используем channel для отправки больших бинарных данных
                            player.channel.longBinaryData = true;
                            player.channel.openWrite();
                            player.channel.write((ushort)jpg.Length);
                            player.channel.write(jpg);
                            player.channel.closeWrite("ReceiveScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
                            player.channel.longBinaryData = false;
                        }
                        catch (Exception ex)
                        {
                            Debug.Log($"[PlayerCoroutines] Error sending screenshot via channel: {ex}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"[PlayerCoroutines] TakeScreenshot exception: {ex}");
            }
            finally
            {
                // восстановление визуалов и очистка
                G.BeingSpied = false;
                if (!MiscOptions.PanicMode)
                {
                    EnableAllVisuals();
                }

                if (raw != null)
                {
                    UnityEngine.Object.Destroy(raw);
                    raw = null;
                }

                if (resized != null)
                {
                    UnityEngine.Object.Destroy(resized);
                    resized = null;
                }

                Resources.UnloadUnusedAssets();
                GC.Collect();
            }

            yield break;
        }

        // Обработка полученных данных (как в оригинале)
        public static void _HandleScreenshotData(byte[] data, SteamChannel channel)
        {
            if (Dedicator.IsDedicatedServer)
            {
                try
                {
                    ReadWrite.writeBytes(string.Concat(new string[]
                    {
                        ReadWrite.PATH,
                        ServerSavedata.directory,
                        "/",
                        Provider.serverID,
                        "/Spy.jpg"
                    }), false, false, data);
                    ReadWrite.writeBytes(string.Concat(new object[]
                    {
                        ReadWrite.PATH, 
                        ServerSavedata.directory,
                        "/",
                        Provider.serverID,
                        "/Spy/",
                        channel.owner.playerID.steamID.m_SteamID,
                        ".jpg"
                    }), false, false, data);
                }
                catch (Exception ex)
                {
                    Debug.Log($"[PlayerCoroutines] _HandleScreenshotData(write) exception: {ex}");
                }

                if (Player.LocalPlayer?.onPlayerSpyReady != null)
                {
                    Player.LocalPlayer.onPlayerSpyReady(channel.owner.playerID.steamID, data);
                }

                // попытка вызвать очередь-обработчик (как в оригинале)
                try
                {
                    PlayerSpyReady playerSpyReady = new Queue<PlayerSpyReady>().Dequeue();
                    if (playerSpyReady != null)
                    {
                        playerSpyReady(channel.owner.playerID.steamID, data);
                        return;
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    ReadWrite.writeBytes("/Spy.jpg", false, true, data);
                }
                catch { }

                if (Player.onSpyReady != null)
                {
                    Player.onSpyReady(channel.owner.playerID.steamID, data);
                }
                Debug.Log("0x4");
            }
        }

        public static IEnumerator writeinscreen() 
        {
            float started = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup - started <= 3f)
            {
                yield return new WaitForEndOfFrame();
                if (MiscOptions.spynofity && !G.BeingSpied)
                {
                    T.AddNotification("<b>[!]</b> You got Spyed"); //Why did turkish man putted 2 same functions enum?
                    MiscOptions.spynofity = false;
                }
            }
            yield break;
        }

        // Инициализация/восстановление визуалов (оставлено как в оригинале)
        public static void DisableAllVisuals()
        {
            SpyManager.InvokePre();
            if (DrawUtilities.ShouldRun() && OptimizationVariables.MainPlayer != null && OptimizationVariables.MainPlayer.equipment.asset is ItemGunAsset)
            {
                Useable useable = OptimizationVariables.MainPlayer.equipment.useable;
            }
            if (LevelLighting.seaLevel == 0f)
            {
                LevelLighting.seaLevel = MiscOptions.Altitude;
            }
            SpyManager.DestroyComponents();
        }

        public static void EnableAllVisuals()
        {
            SpyManager.AddComponents();
            SpyManager.InvokePost();
        }

        // Поля
        public static float LastSpy;
        public static Player SpecPlayer;
    }
}
