using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace ZahidAGA
{
    public class OV_PlayerInput
    {
        public static List<PlayerInputPacket> ClientsidePackets
        {
            get
            {
                if (!DrawUtilities.ShouldRun() || !OV_PlayerInput.Run)
                {
                    return null;
                }
                return (List<PlayerInputPacket>)OV_PlayerInput.ClientsidePacketsField.GetValue(OptimizationVariables.MainPlayer.input);
            }
        }

        public static void OV_askAck(PlayerInput instance, CSteamID steamId, int ack)
        {
            if (!(steamId != Provider.server))
            {
                for (int i = OV_PlayerInput.Packets.Count - 1; i >= 0; i--)
                {
                }
            }
        }

        public static void OV_FixedUpdate()
        {
            Player mainPlayer = OptimizationVariables.MainPlayer;

            // Улучшенная логика для Silent Aim
            if (MiscOptions.PunchSilentAim || RaycastOptions.Enabled)
            {
                OV_DamageTool.OVType = OverrideType.PlayerHit;

                // Генерируем правильный RaycastInfo через наши утилиты
                RaycastInfo raycastInfo;
                if (RaycastUtilities.GenerateRaycast(out raycastInfo))
                {
                    // Определяем правильный тип использования на основе оружия
                    ERaycastInfoUsage usage = GetRaycastUsage();

                    // Создаем клиентский ввод с правильными данными
                    PlayerInputPacket.ClientRaycast clientRaycast = new PlayerInputPacket.ClientRaycast(raycastInfo, usage);

                    // Добавляем в список клиентских вводов
                    List<PlayerInputPacket> currentClientsidePackets = OV_PlayerInput.ClientsidePackets;
                    if (currentClientsidePackets != null && currentClientsidePackets.Count > 0)
                    {
                        PlayerInputPacket lastPacket = currentClientsidePackets.Last();
                        if (lastPacket.clientsideInputs == null)
                        {
                            lastPacket.clientsideInputs = new List<PlayerInputPacket.ClientRaycast>();
                        }
                        lastPacket.clientsideInputs.Add(clientRaycast);
                    }
                }
            }

            // Оригинальный вызов
            OverrideUtilities.CallOriginal(null, new object[0]);

            List<PlayerInputPacket> updatedClientsidePackets = OV_PlayerInput.ClientsidePackets;
            OV_PlayerInput.LastPacket = ((updatedClientsidePackets != null) ? updatedClientsidePackets.Last<PlayerInputPacket>() : null);
        }

        // Метод для определения правильного типа использования Raycast
        private static ERaycastInfoUsage GetRaycastUsage()
        {
            Player player = OptimizationVariables.MainPlayer;
            if (player?.equipment?.asset == null)
                return ERaycastInfoUsage.Gun;

            ItemAsset itemAsset = player.equipment.asset;

            // Простая проверка по типу предмета
            if (itemAsset.type == EItemType.MELEE)
            {
                return ERaycastInfoUsage.Melee;
            }
            else if (itemAsset.type == EItemType.FOOD || itemAsset.type == EItemType.WATER || itemAsset.type == EItemType.MEDICAL)
            {
                return ERaycastInfoUsage.ConsumeableAid;
            }
            else
            {
                // Для всего остального (GUN, THROWABLE, etc.) используем Gun
                return ERaycastInfoUsage.Gun;
            }
        }

        [Override(typeof(PlayerInput), "InitializePlayer", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
        public static void OV_InitializePlayer(PlayerInput instance)
        {
            if (instance.player != Player.LocalPlayer)
            {
                OverrideUtilities.CallOriginal(instance, new object[0]);
                return;
            }
            OptimizationVariables.MainPlayer = Player.LocalPlayer;
            OV_PlayerInput.Rate = 4;
            OV_PlayerInput.Count = 0;
            OV_PlayerInput.Buffer = 0;
            OV_PlayerInput.Packets.Clear();
            OV_PlayerInput.LastPacket = null;
            OV_PlayerInput.SequenceDiff = 0;
            OV_PlayerInput.ClientSequence = 0;
            OverrideUtilities.CallOriginal(instance, new object[0]);
        }

        // Новый метод для правильной отправки пакетов с попаданиями
        [Override(typeof(PlayerInput), "sendInput", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
        public static void OV_sendInput(PlayerInput instance)
        {
            if (instance.player != Player.LocalPlayer || !RaycastOptions.Enabled)
            {
                OverrideUtilities.CallOriginal(instance, new object[0]);
                return;
            }

            try
            {
                // Получаем текущий пакет
                List<PlayerInputPacket> currentPackets = OV_PlayerInput.ClientsidePackets;
                if (currentPackets == null || currentPackets.Count == 0)
                {
                    OverrideUtilities.CallOriginal(instance, new object[0]);
                    return;
                }

                PlayerInputPacket currentPacket = currentPackets.Last();

                // Если у нас есть Silent Aim цели, заменяем RaycastInfo
                if (currentPacket.clientsideInputs != null && currentPacket.clientsideInputs.Count > 0)
                {
                    // Фильтруем только валидные попадания
                    List<PlayerInputPacket.ClientRaycast> validInputs = new List<PlayerInputPacket.ClientRaycast>();

                    foreach (var clientInput in currentPacket.clientsideInputs)
                    {
                        if (IsValidRaycast(clientInput.info))
                        {
                            validInputs.Add(clientInput);
                        }
                    }

                    // Ограничиваем количество входов как в оригинальном коде
                    if (validInputs.Count > 16)
                    {
                        validInputs = validInputs.Take(16).ToList();
                    }

                    currentPacket.clientsideInputs = validInputs;
                }
            }
            catch (Exception e)
            {
                // В случае ошибки используем оригинальный метод
                OverrideUtilities.CallOriginal(instance, new object[0]);
                return;
            }

            // Вызываем оригинальный метод
            OverrideUtilities.CallOriginal(instance, new object[0]);
        }

        // Проверка валидности RaycastInfo на основе серверной логики
        private static bool IsValidRaycast(RaycastInfo info)
        {
            if (info.transform == null) return false;

            // Проверяем дистанцию для игроков
            if (info.player != null)
            {
                float maxDistance = 64f;
                if (info.player.movement.getVehicle() != null)
                {
                    float multiplier = info.player.movement.getVehicle().asset?.ValidHitDistanceMultiplier ?? 1f;
                    maxDistance = 64f * multiplier;
                }

                float sqrDistance = (info.point - info.player.transform.position).sqrMagnitude;
                float maxSqrDistance = maxDistance * maxDistance;

                return sqrDistance < maxSqrDistance;
            }

            // Проверяем дистанцию для зомби
            if (info.zombie != null)
            {
                float sqrDistance = new Vector2(info.point.x - info.zombie.transform.position.x,
                    info.point.z - info.zombie.transform.position.z).sqrMagnitude;
                return sqrDistance < 256f; // 16 * 16
            }

            // Проверяем дистанцию для животных
            if (info.animal != null)
            {
                float sqrDistance = (info.point - info.animal.transform.position).sqrMagnitude;
                return sqrDistance < 256f;
            }

            // Для других объектов проверяем базовую валидность
            return info.point != Vector3.zero && info.transform != null;
        }

        public static FieldInfo ClientsidePacketsField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);

        public static PlayerInputPacket LastPacket;

        public static float Yaw;

        public static float Pitch;

        public static int Count;

        public static int Buffer;

        public static int Choked;

        public static uint Clock = 1U;

        public static int Rate;

        public static int ClientSequence = 1;

        public static int SequenceDiff;

        public static List<PlayerInputPacket> Packets = new List<PlayerInputPacket>();

        public static Queue<PlayerInputPacket> WaitingPackets = new Queue<PlayerInputPacket>();

        public static float LastReal;

        public static bool Run;

        public static FieldInfo SimField = typeof(PlayerInput).GetField("_simulation", BindingFlags.Instance | BindingFlags.NonPublic);

        public static Vector3 lastSentPositon = Vector3.zero;
    }
}