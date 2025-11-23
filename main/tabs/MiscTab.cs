using System;
using System.Diagnostics;
using DarkNight;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000016 RID: 22
	public class MiscTab
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00006530 File Offset: 0x00004730
		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(340f, 20f, 350f, 335f), "Other 1", "box");
			GUILayout.Space(5f);
			MiscTab.scrollPosition5 = GUILayout.BeginScrollView(MiscTab.scrollPosition5, Array.Empty<GUILayoutOption>());
			MiscOptions.Freecam = GUILayout.Toggle(MiscOptions.Freecam, "Free Cam", Array.Empty<GUILayoutOption>());
			MiscOptions.DeleteCharacterAnimation = GUILayout.Toggle(MiscOptions.DeleteCharacterAnimation, "Delete Character Animation", Array.Empty<GUILayoutOption>());
			MiscOptions.ShowVanish = GUILayout.Toggle(MiscOptions.ShowVanish, "Show Vanished Players", Array.Empty<GUILayoutOption>());
			MiscOptions.ShowAdmin = GUILayout.Toggle(MiscOptions.ShowAdmin, "Show Admin Players", Array.Empty<GUILayoutOption>());
			MiscOptions.KillSpam = GUILayout.Toggle(MiscOptions.KillSpam, "Message On kill", Array.Empty<GUILayoutOption>());
			MiscOptions.KillSpamText = GUILayout.TextField(MiscOptions.KillSpamText, Array.Empty<GUILayoutOption>());
			MiscOptions.SpammerEnabled = GUILayout.Toggle(MiscOptions.SpammerEnabled, "Enable Spam", Array.Empty<GUILayoutOption>());
			MiscOptions.SpamText = GUILayout.TextField(MiscOptions.SpamText, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Spam Delay: " + MiscOptions.SpammerDelay.ToString() + " MilliSecond", Array.Empty<GUILayoutOption>());
			MiscOptions.SpammerDelay = (int)GUILayout.HorizontalSlider((float)MiscOptions.SpammerDelay, 10f, 9999f, Array.Empty<GUILayoutOption>());
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(700f, 20f, 290f, 335f), "WeaponOptions", "box");
			GUILayout.Space(5f);
			WeaponOptions.NoRecoil = GUILayout.Toggle(WeaponOptions.NoRecoil, "Remove Recoil", Array.Empty<GUILayoutOption>());
			WeaponOptions.NoSpread = GUILayout.Toggle(WeaponOptions.NoSpread, "Remove Spread", Array.Empty<GUILayoutOption>());
			WeaponOptions.NoSway = GUILayout.Toggle(WeaponOptions.NoSway, "Remove Sway", Array.Empty<GUILayoutOption>());
			WeaponOptions.RemoveBurstDelay = GUILayout.Toggle(WeaponOptions.RemoveBurstDelay, "Remove Burst Delay", Array.Empty<GUILayoutOption>());
			WeaponOptions.RemoveHammerDelay = GUILayout.Toggle(WeaponOptions.RemoveHammerDelay, "Remove Hammer Delay", Array.Empty<GUILayoutOption>());
			WeaponOptions.InstantReload = GUILayout.Toggle(WeaponOptions.InstantReload, "Remove Reload Delay", Array.Empty<GUILayoutOption>());
			WeaponOptions.HitmarkerSoundStatus = GUILayout.Toggle(WeaponOptions.HitmarkerSoundStatus, "Hitmarker", Array.Empty<GUILayoutOption>());
			WeaponOptions.ShowWeaponInfo = GUILayout.Toggle(WeaponOptions.ShowWeaponInfo, "Show Weapon Info", Array.Empty<GUILayoutOption>());
			WeaponOptions.EnableBulletDropPrediction = GUILayout.Toggle(WeaponOptions.EnableBulletDropPrediction, "Showing Bullet Drops", Array.Empty<GUILayoutOption>());
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(340f, 365f, 350f, 265f), "Other 2", "box");
			GUILayout.Space(5f);
			MiscTab.scrollPosition3 = GUILayout.BeginScrollView(MiscTab.scrollPosition3, Array.Empty<GUILayoutOption>());
			MiscOptions.BuildObs = GUILayout.Toggle(MiscOptions.BuildObs, "Build OBS", Array.Empty<GUILayoutOption>());
			MiscOptions.CustomSalvageTime = GUILayout.Toggle(MiscOptions.CustomSalvageTime, "Custom Salvage Time", Array.Empty<GUILayoutOption>());
			if (MiscOptions.CustomSalvageTime)
			{
				GUILayout.Label("Time: " + MiscOptions.SalvageTime.ToString() + " Seconds", Array.Empty<GUILayoutOption>());
				MiscOptions.SalvageTime = GUILayout.HorizontalSlider(MiscOptions.SalvageTime, 0.2f, 10f, Array.Empty<GUILayoutOption>());
			}
			ItemOptions.AutoItemPickupFiltresiz = GUILayout.Toggle(ItemOptions.AutoItemPickupFiltresiz, "Auto Pickup (No Filter)", Array.Empty<GUILayoutOption>());
			if (ItemOptions.AutoItemPickupFiltresiz)
			{
				GUILayout.Label("Delay: " + ItemOptions.ItemPickupDelayFiltresizDelay.ToString() + " Second", Array.Empty<GUILayoutOption>());
				GUILayout.Space(2f);
				ItemOptions.ItemPickupDelayFiltresizDelay = GUILayout.HorizontalSlider(ItemOptions.ItemPickupDelayFiltresizDelay, 0.1f, 10f, Array.Empty<GUILayoutOption>());
				GUILayout.Space(2f);
				GUILayout.Label("Range: " + ItemOptions.ItemPickupDelayFiltresizRange.ToString() + "m", Array.Empty<GUILayoutOption>());
				GUILayout.Space(2f);
				ItemOptions.ItemPickupDelayFiltresizRange = (int)GUILayout.HorizontalSlider((float)ItemOptions.ItemPickupDelayFiltresizRange, 0f, 20f, Array.Empty<GUILayoutOption>());
			}
			MiscOptions.HwidChanger = GUILayout.Toggle(MiscOptions.HwidChanger, "HWID Changer", Array.Empty<GUILayoutOption>());
			MiscOptions.FToplama = GUILayout.Toggle(MiscOptions.FToplama, "Remote Collection(F)", Array.Empty<GUILayoutOption>());
			InteractionOptions.InteractThroughWalls = GUILayout.Toggle(InteractionOptions.InteractThroughWalls, "Interact Through Walls", Array.Empty<GUILayoutOption>());
			MiscOptions.SetTimeEnabled = GUILayout.Toggle(MiscOptions.SetTimeEnabled, "Set Time", Array.Empty<GUILayoutOption>());
			if (MiscOptions.SetTimeEnabled)
			{
				GUILayout.Label("Time: " + MiscOptions.Time.ToString(), Array.Empty<GUILayoutOption>());
				MiscOptions.Time = GUILayout.HorizontalSlider(MiscOptions.Time, 0f, 0.9f, Array.Empty<GUILayoutOption>());
			}
			GUILayout.Space(5f);
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(700f, 365f, 290f, 265f), "Other 3", "box");
			GUILayout.Space(5f);
			MiscTab.scrollPosition2 = GUILayout.BeginScrollView(MiscTab.scrollPosition2, Array.Empty<GUILayoutOption>());
			MiscOptions.VehicleFly = GUILayout.Toggle(MiscOptions.VehicleFly, "Vehicle Fly", Array.Empty<GUILayoutOption>());
			if (MiscOptions.VehicleFly)
			{
				MiscOptions.VehicleRigibody = GUILayout.Toggle(MiscOptions.VehicleRigibody, "Vehicle Rigibody", Array.Empty<GUILayoutOption>());
				MiscOptions.VehicleUseMaxSpeed = GUILayout.Toggle(MiscOptions.VehicleUseMaxSpeed, "Vehicle Max Speed", Array.Empty<GUILayoutOption>());
				if (!MiscOptions.VehicleUseMaxSpeed)
				{
					GUILayout.Label("Speed Multiplier: " + MiscOptions.SpeedMultiplier.ToString() + "x", Array.Empty<GUILayoutOption>());
					MiscOptions.SpeedMultiplier = GUILayout.HorizontalSlider(MiscOptions.SpeedMultiplier, 0f, 50f, Array.Empty<GUILayoutOption>());
				}
			}
			MiscOptions.Adam = GUILayout.Toggle(MiscOptions.Adam, "Change Player Scale", Array.Empty<GUILayoutOption>());
			if (MiscOptions.Adam)
			{
				MiscTab.zort.x = GUILayout.HorizontalSlider(MiscTab.zort.x, 0.5f, 14f, Array.Empty<GUILayoutOption>());
				MiscTab.zort.y = GUILayout.HorizontalSlider(MiscTab.zort.y, 0.5f, 14f, Array.Empty<GUILayoutOption>());
				MiscTab.zort.z = GUILayout.HorizontalSlider(MiscTab.zort.z, 0.5f, 14f, Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Format("Active Player Size: X: {0} | Y: {1} | {2}", MiscTab.zort.x, MiscTab.zort.y, MiscTab.zort.z), Array.Empty<GUILayoutOption>());
				foreach (SteamPlayer steamPlayer in Provider.clients)
				{
					if (steamPlayer.playerID != OptimizationVariables.MainPlayer.channel.owner.playerID)
					{
						steamPlayer.player.gameObject.transform.localScale = MiscTab.zort;
					}
				}
				if (GUILayout.Button("Reset Player Size", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					MiscTab.zort = new Vector3(1f, 1f, 1f);
				}
			}
			else
			{
				foreach (SteamPlayer steamPlayer2 in Provider.clients)
				{
					if (steamPlayer2.playerID != OptimizationVariables.MainPlayer.channel.owner.playerID)
					{
						steamPlayer2.player.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					}
				}
			}
			MiscOptions.Adam2 = GUILayout.Toggle(MiscOptions.Adam2, "Change Local Player Scale", Array.Empty<GUILayoutOption>());
			if (MiscOptions.Adam2)
			{
				MiscTab.zort2.x = GUILayout.HorizontalSlider(MiscTab.zort2.x, 0.5f, 14f, Array.Empty<GUILayoutOption>());
				MiscTab.zort2.y = GUILayout.HorizontalSlider(MiscTab.zort2.y, 0.5f, 14f, Array.Empty<GUILayoutOption>());
				MiscTab.zort2.z = GUILayout.HorizontalSlider(MiscTab.zort2.z, 0.5f, 14f, Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Format("Active Player Size: X: {0} | Y: {1} | {2}", MiscTab.zort2.x, MiscTab.zort2.y, MiscTab.zort2.z), Array.Empty<GUILayoutOption>());
				OptimizationVariables.MainPlayer.gameObject.transform.localScale = MiscTab.zort2;
				if (GUILayout.Button("Reset Player Size", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					MiscTab.zort2 = new Vector3(1f, 1f, 1f);
				}
			}
			else if (OptimizationVariables.MainPlayer != null)
			{
				OptimizationVariables.MainPlayer.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			MiscOptions.Fovv = GUILayout.Toggle(MiscOptions.Fovv, "Change FOV", Array.Empty<GUILayoutOption>());
			if (MiscOptions.Fovv)
			{
				GUILayout.Label(string.Format("Hip: {0}", Provider.preferenceData.Viewmodel.Field_Of_View_Hip), Array.Empty<GUILayoutOption>());
				Provider.preferenceData.Viewmodel.Field_Of_View_Hip = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Field_Of_View_Hip, -5f, 300f, Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Format("Fov Aim: {0}", Provider.preferenceData.Viewmodel.Field_Of_View_Aim), Array.Empty<GUILayoutOption>());
				Provider.preferenceData.Viewmodel.Field_Of_View_Aim = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Field_Of_View_Aim, -5f, 300f, Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Format("Horizontal: {0}", Provider.preferenceData.Viewmodel.Offset_Horizontal), Array.Empty<GUILayoutOption>());
				Provider.preferenceData.Viewmodel.Offset_Horizontal = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Offset_Horizontal, -3f, 150f, Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Format("Vertical:  {0}", Provider.preferenceData.Viewmodel.Offset_Vertical), Array.Empty<GUILayoutOption>());
				Provider.preferenceData.Viewmodel.Offset_Vertical = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Offset_Vertical, -2f, 6f, Array.Empty<GUILayoutOption>());
				GUILayout.Label(string.Format("Depth: {0}", Provider.preferenceData.Viewmodel.Offset_Depth), Array.Empty<GUILayoutOption>());
				Provider.preferenceData.Viewmodel.Offset_Depth = GUILayout.HorizontalSlider(Provider.preferenceData.Viewmodel.Offset_Depth, 0f, 150f, Array.Empty<GUILayoutOption>());
			}
			else
			{
				Provider.preferenceData.Viewmodel.Field_Of_View_Aim = 60f;
				Provider.preferenceData.Viewmodel.Field_Of_View_Hip = 60f;
				Provider.preferenceData.Viewmodel.Offset_Horizontal = 0f;
				Provider.preferenceData.Viewmodel.Offset_Vertical = 0f;
				Provider.preferenceData.Viewmodel.Offset_Depth = 0f;
			}
			GUILayout.Space(5f);
			GUILayout.Label("Anti Spy Method:", Array.Empty<GUILayoutOption>());
			if (MiscOptions.AntiSpyMethod == 0 && GUILayout.Button("Hide Hack", "NavBox", Array.Empty<GUILayoutOption>()))
			{
				MiscOptions.AntiSpyMethod = 1;
			}
			if (MiscOptions.AntiSpyMethod == 1)
			{
				if (GUILayout.Button("Random Image in Folder", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					MiscOptions.AntiSpyMethod = 2;
				}
				GUILayout.Space(10f);
				if (GUILayout.Button("Open Directory The Image Folder", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					Process.Start((Loader.appdata2 + "\\DarkNight\\").Replace("/", "\\") + "\\CustomScreenShot");
				}
			}
			if (MiscOptions.AntiSpyMethod == 2 && GUILayout.Button("Send No Image", "NavBox", Array.Empty<GUILayoutOption>()))
			{
				MiscOptions.AntiSpyMethod = 3;
			}
			if (MiscOptions.AntiSpyMethod == 3 && GUILayout.Button("No Anti-Spy", "NavBox", Array.Empty<GUILayoutOption>()))
			{
				MiscOptions.AntiSpyMethod = 0;
			}
			GUILayout.Space(10f);
			if (GUILayout.Button("Delete Water", "NavBox", Array.Empty<GUILayoutOption>()))
			{
				if (MiscOptions.Altitude == 0f)
				{
					MiscOptions.Altitude = LevelLighting.seaLevel;
				}
				LevelLighting.seaLevel = ((LevelLighting.seaLevel == 0f) ? MiscOptions.Altitude : 0f);
			}
			GUILayout.Space(5f);
			if (GUILayout.Button("Learn All Achievements", "NavBox", Array.Empty<GUILayoutOption>()))
			{
				Provider.provider.achievementsService.setAchievement("Educated");
				Provider.provider.achievementsService.setAchievement("Zweihander");
				Provider.provider.achievementsService.setAchievement("Boss_Magma");
				Provider.provider.achievementsService.setAchievement("Quest");
				Provider.provider.achievementsService.setAchievement("pei");
				Provider.provider.achievementsService.setAchievement("bridge");
				Provider.provider.achievementsService.setAchievement("mastermind");
				Provider.provider.achievementsService.setAchievement("offense");
				Provider.provider.achievementsService.setAchievement("defense");
				Provider.provider.achievementsService.setAchievement("support");
				Provider.provider.achievementsService.setAchievement("experienced");
				Provider.provider.achievementsService.setAchievement("hoarder");
				Provider.provider.achievementsService.setAchievement("outdoors");
				Provider.provider.achievementsService.setAchievement("psychopath");
				Provider.provider.achievementsService.setAchievement("survivor");
				Provider.provider.achievementsService.setAchievement("berries");
				Provider.provider.achievementsService.setAchievement("accident_prone");
				Provider.provider.achievementsService.setAchievement("wheel");
				Provider.provider.achievementsService.setAchievement("yukon");
				Provider.provider.achievementsService.setAchievement("fishing");
				Provider.provider.achievementsService.setAchievement("washington");
				Provider.provider.achievementsService.setAchievement("crafting");
				Provider.provider.achievementsService.setAchievement("farming");
				Provider.provider.achievementsService.setAchievement("headshot");
				Provider.provider.achievementsService.setAchievement("sharpshooter");
				Provider.provider.achievementsService.setAchievement("hiking");
				Provider.provider.achievementsService.setAchievement("roadtrip");
				Provider.provider.achievementsService.setAchievement("champion");
				Provider.provider.achievementsService.setAchievement("fortified");
				Provider.provider.achievementsService.setAchievement("russia");
				Provider.provider.achievementsService.setAchievement("villain");
				Provider.provider.achievementsService.setAchievement("unturned");
				Provider.provider.achievementsService.setAchievement("forged");
				Provider.provider.achievementsService.setAchievement("soulcrystal");
				Provider.provider.achievementsService.setAchievement("paragon");
				Provider.provider.achievementsService.setAchievement("mk2");
				Provider.provider.achievementsService.setAchievement("ensign");
				Provider.provider.achievementsService.setAchievement("major");
				Provider.provider.achievementsService.setAchievement("lieutenant");
				Provider.provider.achievementsService.setAchievement("hawaii");
				Provider.provider.achievementsService.setAchievement("Yukon");
				Provider.provider.achievementsService.setAchievement("Elver_Visited");
				Provider.provider.achievementsService.setAchievement("Kuwait_Visited");
				Provider.provider.achievementsService.setAchievement("PEI");
				Provider.provider.achievementsService.setAchievement("Washington");
				Provider.provider.achievementsService.setAchievement("Hawaii");
				Provider.provider.achievementsService.setAchievement("Arid_Visited");
				Provider.provider.achievementsService.setAchievement("Ireland_Visited");
				Provider.provider.achievementsService.setAchievement("Russia");
				Provider.provider.achievementsService.setAchievement("Peaks");
				Provider.provider.achievementsService.setAchievement("Wheel");
				Provider.provider.achievementsService.setAchievement("Ensign");
				Provider.provider.achievementsService.setAchievement("Lieutenant");
				Provider.provider.achievementsService.setAchievement("Major");
				Provider.provider.achievementsService.setAchievement("Quest");
				Provider.provider.achievementsService.setAchievement("Villain");
				Provider.provider.achievementsService.setAchievement("Paragon");
				Provider.provider.achievementsService.setAchievement("Offense");
				Provider.provider.achievementsService.setAchievement("Defense");
				Provider.provider.achievementsService.setAchievement("Support");
				Provider.provider.achievementsService.setAchievement("Mastermind");
				Provider.provider.achievementsService.setAchievement("Berries");
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x0400009F RID: 159
		public static Vector3 zort = new Vector3(1f, 1f, 1f);

		// Token: 0x040000A0 RID: 160
		public static Vector3 zort2 = new Vector3(1f, 1f, 1f);

		// Token: 0x040000A1 RID: 161
		private static Vector2 scrollPosition2 = new Vector2(0f, 0f);

		// Token: 0x040000A2 RID: 162
		private static Vector2 scrollPosition3 = new Vector2(0f, 0f);

		// Token: 0x040000A3 RID: 163
		private static Vector2 scrollPosition5 = new Vector2(0f, 0f);
	}
}
