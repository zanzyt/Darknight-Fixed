using System;
using System.Collections.Generic;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
    public class OV_UseableGun
    {
        public static FieldInfo BulletsField;

        [Initializer]
        public static void Load()
        {
            // В новых версиях поле по-прежнему приватное, берём его рефлексией
            BulletsField = typeof(UseableGun).GetField("bullets",
                BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public static bool IsRaycastInvalid(RaycastInfo info)
        {
            return info == null ||
                   (info.player == null &&
                    info.zombie == null &&
                    info.animal == null &&
                    info.vehicle == null &&
                    info.transform == null);
        }

        [Override(typeof(UseableGun), "ballistics", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
        public void OV_ballistics()
        {
            var mainPlayer = OptimizationVariables.MainPlayer;
            if (mainPlayer == null || mainPlayer.equipment == null)
            {
                return;
            }

            var useable = mainPlayer.equipment.useable as UseableGun;
            if (useable == null)
            {
                // если вдруг не UseableGun – просто оригинал
                OverrideUtilities.CallOriginal(mainPlayer.equipment.useable);
                return;
            }

            if (Provider.isServer)
            {
                // На сервере НИЧЕГО не мудрим
                OverrideUtilities.CallOriginal(useable);
                return;
            }

            var itemGunAsset = mainPlayer.equipment.asset as ItemGunAsset;
            if (itemGunAsset == null)
            {
                OverrideUtilities.CallOriginal(useable);
                return;
            }

            // Для гранатомётов / ракетниц с projectile лучше вообще не трогать баллистику
            if (itemGunAsset.projectile != null)
            {
                OverrideUtilities.CallOriginal(useable);
                return;
            }

            var bullets = BulletsField?.GetValue(useable) as List<BulletInfo>;
            if (bullets == null || bullets.Count == 0)
            {
                return; // нет выстрелов — нечего делать
            }

            RaycastInfo ri = null;

            // ***** Твой кастомный Raycast (Silent / Trigger / и т.д.) *****
            if (RaycastOptions.Enabled)
            {
                RaycastUtilities.GenerateRaycast(out ri);
                WeaponComponent.AddTracer(ri);
                WeaponComponent.AddDamage(ri);
            }

            var look = mainPlayer.look;

            // ***** NoAimbotDrop — стреляем прямо в голову без падения *****
            if (ri == null && AimbotOptions.NoAimbotDrop &&
                AimbotCoroutines.IsAiming && AimbotCoroutines.LockedObject != null)
            {
                Vector3 aimPos =
                    AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull");

                Ray aimRay = GetAimRay(look.aim.position, aimPos);
                float dist = (float)VectorUtilities.GetDistance(look.aim.position, aimPos);

                RaycastHit hit;
                if (!Physics.Raycast(aimRay, out hit, dist, RayMasks.DAMAGE_SERVER))
                {
                    // Оригинальный луч без дропа
                    ri = RaycastUtilities.GenerateOriginalRaycast(
                        aimRay,
                        itemGunAsset.range,
                        1024,          // маска как в старом коде
                        null);
                }
            }

            // Если кастомного луча нет, а NoDrop выключен – отдаём управление оригиналу
            if (ri == null && !WeaponOptions.NoDrop)
            {
                OverrideUtilities.CallOriginal(useable);
                return;
            }

            // Если вообще так и не смогли получить RaycastInfo – тоже в оригинал
            if (ri == null)
            {
                OverrideUtilities.CallOriginal(useable);
                return;
            }

            // ***** На этом этапе у нас есть ri, просто прожигаем все пули по нему *****
            for (int i = 0; i < bullets.Count; i++)
            {
                var hitType = CalcHitMarker(itemGunAsset, ref ri);
                PlayerUI.hitmark(Vector3.zero, false, hitType);
                mainPlayer.input.sendRaycast(ri, ERaycastInfoUsage.Gun);
            }

            // Чистим список пуль, чтобы игра не пыталась дальше их симулировать
            bullets.Clear();
        }

        public static EPlayerHit CalcHitMarker(ItemGunAsset PAsset, ref RaycastInfo ri)
        {
            EPlayerHit eplayerHit = EPlayerHit.NONE;

            if (ri == null || PAsset == null)
                return eplayerHit;

            if (ri.animal != null || ri.player != null || ri.zombie != null)
            {
                eplayerHit = EPlayerHit.ENTITIY;
                if (ri.limb == ELimb.SKULL)
                    eplayerHit = EPlayerHit.CRITICAL;
            }
            else if (ri.transform != null)
            {
                if (ri.transform.CompareTag("Barricade") && PAsset.barricadeDamage > 1f)
                {
                    var door = ri.transform.GetComponent<InteractableDoor>();
                    if (door != null)
                        ri.transform = door.transform.parent.parent;

                    if (!ushort.TryParse(ri.transform.name, out ushort id))
                        return eplayerHit;

                    var asset = Assets.find(EAssetType.ITEM, id) as ItemBarricadeAsset;
                    if (asset == null || (!asset.isVulnerable && !PAsset.isInvulnerable))
                        return eplayerHit;

                    if (eplayerHit == EPlayerHit.NONE)
                        eplayerHit = EPlayerHit.BUILD;
                }
                else if (ri.transform.CompareTag("Structure") && PAsset.structureDamage > 1f)
                {
                    if (!ushort.TryParse(ri.transform.name, out ushort id2))
                        return eplayerHit;

                    var asset2 = Assets.find(EAssetType.ITEM, id2) as ItemStructureAsset;
                    if (asset2 == null || (!asset2.isVulnerable && !PAsset.isInvulnerable))
                        return eplayerHit;

                    if (eplayerHit == EPlayerHit.NONE)
                        eplayerHit = EPlayerHit.BUILD;
                }
                else if (ri.transform.CompareTag("Resource") && PAsset.resourceDamage > 1f)
                {
                    if (!ResourceManager.tryGetRegion(ri.transform, out byte x, out byte y, out ushort ind))
                        return eplayerHit;

                    var res = ResourceManager.getResourceSpawnpoint(x, y, ind);
                    if (res == null || res.isDead || !PAsset.hasBladeID(res.asset.bladeID))
                        return eplayerHit;

                    if (eplayerHit == EPlayerHit.NONE)
                        eplayerHit = EPlayerHit.BUILD;
                }
                else if (PAsset.objectDamage > 1f)
                {
                    var rubble = ri.transform.GetComponent<InteractableObjectRubble>();
                    if (rubble == null)
                        return eplayerHit;

                    ri.section = rubble.getSection(ri.collider.transform);
                    if (rubble.isSectionDead(ri.section) ||
                        (!rubble.asset.rubbleIsVulnerable && !PAsset.isInvulnerable))
                        return eplayerHit;

                    if (eplayerHit == EPlayerHit.NONE)
                        eplayerHit = EPlayerHit.BUILD;
                }
            }
            else if (ri.vehicle != null &&
                     !ri.vehicle.isDead &&
                     PAsset.vehicleDamage > 1f &&
                     ri.vehicle.asset != null &&
                     (ri.vehicle.asset.isVulnerable || PAsset.isInvulnerable) &&
                     eplayerHit == EPlayerHit.NONE)
            {
                eplayerHit = EPlayerHit.BUILD;
            }

            return eplayerHit;
        }

        public static Ray GetAimRay(Vector3 origin, Vector3 pos)
        {
            Vector3 dir = VectorUtilities.Normalize(pos - origin);
            // В старом коде у тебя было new Ray(pos, dir) – это неправильно.
            return new Ray(origin, dir);
        }
    }
}
