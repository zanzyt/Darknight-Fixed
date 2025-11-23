using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
    public class RaycastCoroutines
    {
        public static IEnumerator UpdateObjects()
        {
            for (; ; )
            {
                if (!DrawUtilities.ShouldRun())
                {
                    RaycastUtilities.Objects.Clear();
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                try
                {
                    // ---- 1. Один раз вычисляем радиус ----
                    ItemGunAsset gun = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
                    float radius = (gun != null ? gun.range : 15.5f) + 10f;

                    // ---- 2. Один вызов OverlapSphere ----
                    Collider[] hits = Physics.OverlapSphere(
                        OptimizationVariables.MainPlayer.transform.position,
                        radius
                    );

                    // ---- 3. Преобразуем в GameObjects БЕЗ LINQ ----
                    int len = hits.Length;
                    GameObject[] objects = new GameObject[len];
                    for (int i = 0; i < len; i++)
                        objects[i] = hits[i].gameObject;

                    RaycastUtilities.Objects.Clear();

                    // ---- 4. Локальный список, а не статический ----
                    List<Player> localPlayers = null;

                    if (RaycastOptions.Enabled)
                    {
                        // ---- Players ----
                        if (RaycastOptions.TargetPlayers)
                        {
                            localPlayers = new List<Player>(16);

                            for (int i = 0; i < objects.Length; i++)
                            {
                                Player p = DamageTool.getPlayer(objects[i].transform);
                                if (p != null && p != OptimizationVariables.MainPlayer && !p.life.isDead)
                                    localPlayers.Add(p);
                            }

                            foreach (var p in localPlayers)
                                RaycastUtilities.Objects.Add(p.gameObject);
                        }

                        // ---- Zombies ----
                        if (RaycastOptions.TargetZombies)
                        {
                            for (int i = 0; i < objects.Length; i++)
                                if (objects[i].TryGetComponent<Zombie>(out _))
                                    RaycastUtilities.Objects.Add(objects[i]);
                        }

                        // ---- Sentries ----
                        if (RaycastOptions.TargetSentries)
                        {
                            for (int i = 0; i < objects.Length; i++)
                                if (objects[i].TryGetComponent<InteractableSentry>(out _))
                                    RaycastUtilities.Objects.Add(objects[i]);
                        }

                        // ---- Beds ----
                        if (RaycastOptions.TargetBeds)
                        {
                            for (int i = 0; i < objects.Length; i++)
                                if (objects[i].TryGetComponent<InteractableBed>(out _))
                                    RaycastUtilities.Objects.Add(objects[i]);
                        }

                        // ---- Animals ----
                        if (RaycastOptions.TargetAnimal)
                        {
                            for (int i = 0; i < objects.Length; i++)
                                if (objects[i].TryGetComponent<Animal>(out _))
                                    RaycastUtilities.Objects.Add(objects[i]);
                        }

                        // ---- Claim Flags ----
                        if (RaycastOptions.TargetClaimFlags)
                        {
                            for (int i = 0; i < objects.Length; i++)
                                if (objects[i].TryGetComponent<InteractableClaim>(out _))
                                    RaycastUtilities.Objects.Add(objects[i]);
                        }

                        // ---- Vehicles ----
                        if (RaycastOptions.TargetVehicles)
                        {
                            for (int i = 0; i < objects.Length; i++)
                                if (objects[i].TryGetComponent<InteractableVehicle>(out _))
                                    RaycastUtilities.Objects.Add(objects[i]);
                        }

                        // ---- Storage ----
                        if (RaycastOptions.TargetStorage)
                        {
                            for (int i = 0; i < objects.Length; i++)
                                if (objects[i].TryGetComponent<InteractableStorage>(out _))
                                    RaycastUtilities.Objects.Add(objects[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"[RaycastCoroutines] Error: {ex}");
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
