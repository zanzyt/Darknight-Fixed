using System;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000006 RID: 6
	public class AimbotTab
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00004BDC File Offset: 0x00002DDC
		public static void Tab()
		{
			GUILayout.BeginArea(new Rect(340f, 20f, 350f, 400f), "Silent Aimbot", "box");
			GUILayout.Space(5f);
			AimbotTab.scrollPosition = GUILayout.BeginScrollView(AimbotTab.scrollPosition, Array.Empty<GUILayoutOption>());
			RaycastOptions.Enabled = GUILayout.Toggle(RaycastOptions.Enabled, "Silent Aim", Array.Empty<GUILayoutOption>());
			if (RaycastOptions.Enabled)
			{
				MiscOptions.PunchSilentAim = true;
				MiscOptions.ExtendMeleeRange = true;
				GUILayout.Label("Hitbox Size: " + AimbotOptions.HitboxSize.ToString(), Array.Empty<GUILayoutOption>());
				AimbotOptions.HitboxSize = (int)GUILayout.HorizontalSlider((float)AimbotOptions.HitboxSize, 0f, 30f, Array.Empty<GUILayoutOption>());
				GUILayout.Label("Hitbox Chance %: " + AimbotOptions.HitChance.ToString(), Array.Empty<GUILayoutOption>());
				AimbotOptions.HitChance = (int)GUILayout.HorizontalSlider((float)AimbotOptions.HitChance, 0f, 100f, Array.Empty<GUILayoutOption>());
				if (RaycastOptions.FovMethod == 0 && GUILayout.Button("Fov Type: Circle", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					RaycastOptions.FovMethod = 1;
					RaycastOptions.FovKalınlık = 2.098787f;
				}
				if (RaycastOptions.FovMethod == 1 && GUILayout.Button("Fov Type: Trigon", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					RaycastOptions.FovMethod = 2;
					RaycastOptions.FovKalınlık = 1.569352f;
				}
				if (RaycastOptions.FovMethod == 2 && GUILayout.Button("Fov Type: Square", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					RaycastOptions.FovMethod = 3;
					RaycastOptions.FovKalınlık = 1.048189f;
				}
				if (RaycastOptions.FovMethod == 3 && GUILayout.Button("Fov Type: Hexagon", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					RaycastOptions.FovMethod = 4;
					RaycastOptions.FovKalınlık = 0.7899502f;
				}
				if (RaycastOptions.FovMethod == 4 && GUILayout.Button("Fov Type: Octagon", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					RaycastOptions.FovMethod = 5;
					RaycastOptions.FovKalınlık = 0.3946678f;
				}
				if (RaycastOptions.FovMethod == 5 && GUILayout.Button("Fov Type: Hexadecimal", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					RaycastOptions.FovMethod = 0;
					RaycastOptions.FovKalınlık = 0.01f;
				}
				RaycastOptions.SilentAimUseFOV = GUILayout.Toggle(RaycastOptions.SilentAimUseFOV, "Pixel FOV Limit", Array.Empty<GUILayoutOption>());
				if (RaycastOptions.SilentAimUseFOV)
				{
					GUILayout.Label("FOV: " + RaycastOptions.SilentAimFOV.ToString(), Array.Empty<GUILayoutOption>());
					RaycastOptions.SilentAimFOV = (float)((int)GUILayout.HorizontalSlider(RaycastOptions.SilentAimFOV, 1f, 180f, Array.Empty<GUILayoutOption>()));
					RaycastOptions.ShowSilentAimUseFOV = GUILayout.Toggle(RaycastOptions.ShowSilentAimUseFOV, "Draw Pixel FOV Circle", Array.Empty<GUILayoutOption>());
				}
				WeaponOptions.DamageIndicators = GUILayout.Toggle(WeaponOptions.DamageIndicators, "Damage Indicators", Array.Empty<GUILayoutOption>());
				WeaponOptions.Tracers = GUILayout.Toggle(WeaponOptions.Tracers, "Tracers", Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(700f, 20f, 290f, 400f), "Silent Aim Targets", "box");
			GUILayout.Space(5f);
			if (RaycastOptions.Enabled)
			{
				RaycastOptions.TargetPlayers = GUILayout.Toggle(RaycastOptions.TargetPlayers, "Target: Players", Array.Empty<GUILayoutOption>());
				RaycastOptions.TargetBeds = GUILayout.Toggle(RaycastOptions.TargetBeds, "Target: Beds", Array.Empty<GUILayoutOption>());
				RaycastOptions.TargetZombies = GUILayout.Toggle(RaycastOptions.TargetZombies, "Target: Zombies", Array.Empty<GUILayoutOption>());
				RaycastOptions.TargetAnimal = GUILayout.Toggle(RaycastOptions.TargetAnimal, "Target: Animals", Array.Empty<GUILayoutOption>());
				RaycastOptions.TargetClaimFlags = GUILayout.Toggle(RaycastOptions.TargetClaimFlags, "Target: Claim Flag", Array.Empty<GUILayoutOption>());
				RaycastOptions.TargetVehicles = GUILayout.Toggle(RaycastOptions.TargetVehicles, "Target: Vehicle", Array.Empty<GUILayoutOption>());
				RaycastOptions.TargetStorage = GUILayout.Toggle(RaycastOptions.TargetStorage, "Target: Storage", Array.Empty<GUILayoutOption>());
				RaycastOptions.TargetSentries = GUILayout.Toggle(RaycastOptions.TargetSentries, "Target: Sentries", Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(340f, 430f, 350f, 200f), "Other", "box");
			GUILayout.Space(5f);
			if (GUILayout.Button("Silent Aim Limb: " + Enum.GetName(typeof(ELimb), RaycastOptions.TargetLimb), "NavBox", Array.Empty<GUILayoutOption>()))
			{
				RaycastOptions.TargetLimb = RaycastOptions.TargetLimb.Next<ELimb>();
			}
			RaycastOptions.UseRandomLimb = GUILayout.Toggle(RaycastOptions.UseRandomLimb, "Random Limb", Array.Empty<GUILayoutOption>());
			SphereOptions.SpherePrediction = GUILayout.Toggle(SphereOptions.SpherePrediction, "Sphere Prediction", Array.Empty<GUILayoutOption>());
			if (!SphereOptions.SpherePrediction)
			{
				GUILayout.Label(Math.Round((double)SphereOptions.SphereRadius, 2).ToString() + "m", Array.Empty<GUILayoutOption>());
				SphereOptions.SphereRadius = (float)((int)GUILayout.HorizontalSlider(SphereOptions.SphereRadius, 0f, 15.5f, Array.Empty<GUILayoutOption>()));
			}
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(700f, 430f, 290f, 200f), "Aimlock", "box");
			AimbotTab.scrollPosition2 = GUILayout.BeginScrollView(AimbotTab.scrollPosition2, Array.Empty<GUILayoutOption>());
			GUILayout.Space(5f);
			AimbotOptions.Enabled = GUILayout.Toggle(AimbotOptions.Enabled, "Aimlock", Array.Empty<GUILayoutOption>());
			if (AimbotOptions.Enabled)
			{
				AimbotOptions.Smooth = GUILayout.Toggle(AimbotOptions.Smooth, "Smooth", Array.Empty<GUILayoutOption>());
				if (AimbotOptions.Smooth)
				{
					GUILayout.Label("Speed: " + AimbotOptions.AimSpeed.ToString(), Array.Empty<GUILayoutOption>());
					AimbotOptions.AimSpeed = (float)((int)GUILayout.HorizontalSlider(AimbotOptions.AimSpeed, 1f, 10f, Array.Empty<GUILayoutOption>()));
				}
				AimbotOptions.OnKey = GUILayout.Toggle(AimbotOptions.OnKey, "On Key (F)", Array.Empty<GUILayoutOption>());
				AimbotOptions.AimUseFOV = GUILayout.Toggle(AimbotOptions.AimUseFOV, "Use Fov Aim", Array.Empty<GUILayoutOption>());
				if (AimbotOptions.AimUseFOV)
				{
					GUILayout.Label("FOV: " + AimbotOptions.SelectedFOV.ToString(), Array.Empty<GUILayoutOption>());
					AimbotOptions.SelectedFOV = (float)((int)GUILayout.HorizontalSlider(AimbotOptions.SelectedFOV, 1f, 180f, Array.Empty<GUILayoutOption>()));
					AimbotOptions.ShowAimUseFOV = GUILayout.Toggle(AimbotOptions.ShowAimUseFOV, "Draw Fov", Array.Empty<GUILayoutOption>());
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x04000006 RID: 6
		private static Vector2 scrollPosition;

		// Token: 0x04000007 RID: 7
		private static Vector2 scrollPosition2;
	}
}
