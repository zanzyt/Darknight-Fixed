using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;
using System.Collections.Generic;

namespace ZahidAGA
{
    public class OV_Hitmark
    {
        private static FieldInfo activeHitmarkersField;
        private static MethodInfo claimHitmarkerMethod;
        private static FieldInfo wantsWindowEnabledField;
        private static StaticResourceRef<AudioClip> hitCriticalSound = new StaticResourceRef<AudioClip>("Sounds/General/Hit");

        static OV_Hitmark()
        {
            // Get the internal fields and methods using reflection
            activeHitmarkersField = typeof(PlayerLifeUI).GetField("activeHitmarkers", BindingFlags.Static | BindingFlags.NonPublic);
            claimHitmarkerMethod = typeof(PlayerLifeUI).GetMethod("ClaimHitmarker", BindingFlags.Static | BindingFlags.NonPublic);
            wantsWindowEnabledField = typeof(PlayerUI).GetField("wantsWindowEnabled", BindingFlags.Static | BindingFlags.NonPublic);
        }

        [Override(typeof(PlayerUI), "hitmark", BindingFlags.Static | BindingFlags.Public, 0)]
        public static void OV_hitmark(Vector3 point, bool worldspace, EPlayerHit newHit)
        {
            if (wantsWindowEnabledField == null || activeHitmarkersField == null || claimHitmarkerMethod == null)
                return;

            if (!(bool)wantsWindowEnabledField.GetValue(null))
            {
                return;
            }

            if (!Provider.modeConfigData.Gameplay.Hitmarkers)
            {
                return;
            }

            // Create hitmarker using the new system with reflection
            HitmarkerInfo item = default(HitmarkerInfo);
            item.worldPosition = point;
            item.shouldFollowWorldPosition = worldspace || OptionsSettings.ShouldHitmarkersFollowWorldPosition;

            // Call ClaimHitmarker via reflection
            item.sleekElement = (SleekHitmarker)claimHitmarkerMethod.Invoke(null, null);
            item.sleekElement.SetStyle(newHit);

            if (OptionsSettings.hitmarkerStyle == EHitmarkerStyle.Animated)
            {
                item.sleekElement.PlayAnimation();
            }
            else
            {
                item.sleekElement.ApplyClassicPositions();
            }

            // Add to active hitmarkers list via reflection
            List<HitmarkerInfo> activeHitmarkers = (List<HitmarkerInfo>)activeHitmarkersField.GetValue(null);
            activeHitmarkers.Add(item);

            // Play sound based on hit type and settings
            if (newHit == EPlayerHit.CRITICAL)
            {
                if (WeaponOptions.HitmarkerSoundStatus)
                {
                    MainCamera.instance.GetComponent<AudioSource>().PlayOneShot(AssetUtilities.BonkClip, WeaponOptions.HitmarkerSoundVolume);
                }
                else
                {
                    MainCamera.instance.GetComponent<AudioSource>().PlayOneShot(hitCriticalSound, 0.5f);
                }

                // Play additional sounds based on settings
                if (WeaponOptions.skeet)
                {
                    MainCamera.instance.GetComponent<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["skeet"], WeaponOptions.HitmarkerSoundVolume);
                }
                if (WeaponOptions.csgo)
                {
                    MainCamera.instance.GetComponent<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["csgo"], WeaponOptions.HitmarkerSoundVolume);
                }
                if (WeaponOptions.rust)
                {
                    MainCamera.instance.GetComponent<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["rust"], WeaponOptions.HitmarkerSoundVolume);
                }
            }
        }
    }
}