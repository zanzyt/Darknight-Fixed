using System;
using DarkNight;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200001D RID: 29
	public class VisualTab
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000096F8 File Offset: 0x000078F8
		public static void Tab()
		{
			GUILayout.Space(0f);
			GUILayout.BeginArea(new Rect(300f, 0f, 940f, 40f));
			VisualTab.SelectedObject = (ESPTarget)GUI.Toolbar(new Rect(25f, 10f, 680f, 40f), (int)VisualTab.SelectedObject, Main.buttons2.ToArray(), "TabBtn");
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(340f, 330f, 350f, 300f), "Options", "box");
			VisualTab.scrollPosition2 = GUILayout.BeginScrollView(VisualTab.scrollPosition2, Array.Empty<GUILayoutOption>());
			switch (VisualTab.SelectedObject)
			{
			case ESPTarget.Player:
				ESPOptions.ShowPlayerWeapon = GUILayout.Toggle(ESPOptions.ShowPlayerWeapon, "Show Weapon", Array.Empty<GUILayoutOption>());
				ESPOptions.showhotbar = GUILayout.Toggle(ESPOptions.showhotbar, "Show Weapon Icon", Array.Empty<GUILayoutOption>());
				ESPOptions.HitboxSize = GUILayout.Toggle(ESPOptions.HitboxSize, "Show Hitbox Size", Array.Empty<GUILayoutOption>());
				ESPOptions.Skeleton = GUILayout.Toggle(ESPOptions.Skeleton, "Skeleton", Array.Empty<GUILayoutOption>());
				if (ESPOptions.showhotbar)
				{
					GUILayout.Label("Pixel Size X: " + ESPOptions.x.ToString(), Array.Empty<GUILayoutOption>());
					ESPOptions.x = (int)GUILayout.HorizontalSlider((float)ESPOptions.x, 10f, 200f, Array.Empty<GUILayoutOption>());
					GUILayout.Label("Pixel Size Y: " + ESPOptions.y.ToString(), Array.Empty<GUILayoutOption>());
					ESPOptions.y = (int)GUILayout.HorizontalSlider((float)ESPOptions.y, 10f, 200f, Array.Empty<GUILayoutOption>());
				}
				//ESPOptions.ShowAmmo = GUILayout.Toggle(ESPOptions.ShowAmmo, "Ammo", Array.Empty<GUILayoutOption>());
				if (MiscOptions.ShaderMethod == 1 && GUILayout.Button("ChamsType: Flat", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					ESPOptions.ChamsEnabled = false;
					ESPOptions.ChamsFlat = false;
					ESPOptions.Ignore = true;
					MiscOptions.ShaderMethod = 2;
				}
				if (MiscOptions.ShaderMethod == 2 && GUILayout.Button("ChamsType: Xray", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					ESPOptions.ChamsEnabled = false;
					ESPOptions.ChamsFlat = false;
					ESPOptions.Ignore = false;
					MiscOptions.ShaderMethod = 0;
				}
				if (MiscOptions.ShaderMethod == 0 && GUILayout.Button("ChamsType: None", "NavBox", Array.Empty<GUILayoutOption>()))
				{
					ESPOptions.ChamsEnabled = true;
					ESPOptions.ChamsFlat = true;
					ESPOptions.Ignore = false;
					MiscOptions.ShaderMethod = 1;
				}
				VisualTab.DrawGlobals2(ESPTarget.Player);
				break;
			case ESPTarget.Zombie:
				VisualTab.DrawGlobals2(ESPTarget.Zombie);
				break;
			case ESPTarget.Item:
				VisualTab.DrawGlobals2(ESPTarget.Item);
				break;
			case ESPTarget.Sentry:
				ESPOptions.ShowSentryItem = GUILayout.Toggle(ESPOptions.ShowSentryItem, "Show Sentry Gun", Array.Empty<GUILayoutOption>());
				VisualTab.DrawGlobals2(ESPTarget.Sentry);
				break;
			case ESPTarget.Bed:
				ESPOptions.ClaimNameBed = GUILayout.Toggle(ESPOptions.ClaimNameBed, "Show Claimed State", Array.Empty<GUILayoutOption>());
				VisualTab.DrawGlobals2(ESPTarget.Bed);
				break;
			case ESPTarget.Flag:
				VisualTab.DrawGlobals2(ESPTarget.Flag);
				break;
			case ESPTarget.Vehicle:
				ESPOptions.ShowVehicleLocked = GUILayout.Toggle(ESPOptions.ShowVehicleLocked, "Show Lock State", Array.Empty<GUILayoutOption>());
				ESPOptions.FilterVehicleLocked = GUILayout.Toggle(ESPOptions.FilterVehicleLocked, "Only Display Unlocked Vehicles", Array.Empty<GUILayoutOption>());
				VisualTab.DrawGlobals2(ESPTarget.Vehicle);
				break;
			case ESPTarget.Storage:
				VisualTab.DrawGlobals2(ESPTarget.Storage);
				break;
			case ESPTarget.Airdrop:
				VisualTab.DrawGlobals2(ESPTarget.Airdrop);
				break;
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(700f, 430f, 290f, 200f), "Other", "box");
			GUILayout.Space(5f);
			VisualTab.scrollPosition3 = GUILayout.BeginScrollView(VisualTab.scrollPosition3, Array.Empty<GUILayoutOption>());
			MiscOptions.NoGrass = GUILayout.Toggle(MiscOptions.NoGrass, "No Grass", Array.Empty<GUILayoutOption>());
			MiscOptions.NoCloud = GUILayout.Toggle(MiscOptions.NoCloud, "No Cloud", Array.Empty<GUILayoutOption>());
			MiscOptions.NoRain = GUILayout.Toggle(MiscOptions.NoRain, "No Rain", Array.Empty<GUILayoutOption>());
			MiscOptions.GPS = GUILayout.Toggle(MiscOptions.GPS, "Force GPS", Array.Empty<GUILayoutOption>());
			ESPOptions.ShowMap = GUILayout.Toggle(ESPOptions.ShowMap, "Show Player On Map", Array.Empty<GUILayoutOption>());
			MiscOptions.NightVision = GUILayout.Toggle(MiscOptions.NightVision, "Night Mode", Array.Empty<GUILayoutOption>());
			MiscOptions.NightVision2 = GUILayout.Toggle(MiscOptions.NightVision2, "NightVision", Array.Empty<GUILayoutOption>());
			MiscOptions.Compass = GUILayout.Toggle(MiscOptions.Compass, "Force Compass", Array.Empty<GUILayoutOption>());
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			Rect rect = new Rect(340f, 65f, 350f, 250f);
			GUIStyle guistyle = "box";
			GUILayout.BeginArea(rect, Enum.GetName(typeof(ESPTarget), VisualTab.SelectedObject), guistyle);
			GUILayout.Space(5f);
			VisualTab.scrollPosition = GUILayout.BeginScrollView(VisualTab.scrollPosition, Array.Empty<GUILayoutOption>());
			switch (VisualTab.SelectedObject)
			{
			case ESPTarget.Player:
				VisualTab.DrawGlobals(ESPTarget.Player, "Players");
				break;
			case ESPTarget.Zombie:
				VisualTab.DrawGlobals(ESPTarget.Zombie, "Zombies");
				break;
			case ESPTarget.Item:
				VisualTab.DrawGlobals(ESPTarget.Item, "Items");
				break;
			case ESPTarget.Sentry:
				VisualTab.DrawGlobals(ESPTarget.Sentry, "Sentry");
				break;
			case ESPTarget.Bed:
				VisualTab.DrawGlobals(ESPTarget.Bed, "Beds");
				break;
			case ESPTarget.Flag:
				VisualTab.DrawGlobals(ESPTarget.Flag, "Claim Flags");
				break;
			case ESPTarget.Vehicle:
				VisualTab.DrawGlobals(ESPTarget.Vehicle, "Vehicles");
				break;
			case ESPTarget.Storage:
				VisualTab.DrawGlobals(ESPTarget.Storage, "Storages");
				break;
			case ESPTarget.Airdrop:
				VisualTab.DrawGlobals(ESPTarget.Airdrop, "Airdrop");
				break;
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(700f, 65f, 290f, 350f), "box");
			switch (VisualTab.SelectedObject)
			{
			case ESPTarget.Player:
				VisualTab.DrawPreview(ESPTarget.Player);
				break;
			case ESPTarget.Zombie:
				VisualTab.DrawPreviewZombie(ESPTarget.Zombie);
				break;
			case ESPTarget.Item:
				VisualTab.DrawPreviewİtem(ESPTarget.Item);
				break;
			case ESPTarget.Sentry:
				VisualTab.DrawPreviewSentiri(ESPTarget.Sentry);
				break;
			case ESPTarget.Bed:
				VisualTab.DrawPreviewBed(ESPTarget.Bed);
				break;
			case ESPTarget.Flag:
				VisualTab.DrawPreviewFlag(ESPTarget.Flag);
				break;
			case ESPTarget.Vehicle:
				VisualTab.DrawPreviewVehicle(ESPTarget.Vehicle);
				break;
			case ESPTarget.Storage:
				VisualTab.DrawPreviewLocker(ESPTarget.Storage);
				break;
			case ESPTarget.Airdrop:
				VisualTab.DrawPreviewAIRDROP(ESPTarget.Airdrop);
				break;
			}
			GUILayout.EndArea();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00009D24 File Offset: 0x00007F24
		private static void DrawPreview(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			if (ESPOptions.ChamsEnabled)
			{
				GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.ChPlayer);
			}
			else
			{
				GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.Player);
			}
			if (ESPOptions.Ignore)
			{
				GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.xray);
			}
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 265f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName || ESPOptions.ShowPlayerWeapon)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? (" " + Loader.Name) : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? " - Maplestrike" : "Maplestrike") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00009EAC File Offset: 0x000080AC
		private static void DrawPreviewZombie(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.zombie);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " Zombie" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00009FD0 File Offset: 0x000081D0
		private static void DrawPreviewSentiri(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.sentiri);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " Sentry" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000A0F4 File Offset: 0x000082F4
		private static void DrawPreviewİtem(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.ıtem);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " Item" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000A218 File Offset: 0x00008418
		private static void DrawPreviewBed(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.bed);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " Bed" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000A33C File Offset: 0x0000853C
		private static void DrawPreviewFlag(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.flag);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " Claim Flag" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000A460 File Offset: 0x00008660
		private static void DrawPreviewVehicle(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.vehicle);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " Vehicle" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000A584 File Offset: 0x00008784
		private static void DrawPreviewLocker(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.locker);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " Locker" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000A6A8 File Offset: 0x000088A8
		private static void DrawPreviewAIRDROP(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUI.Label(new Rect(25f, 20f, 250f, 400f), Main.airdrop);
			if (espvisual.Boxes)
			{
				T.DrawColorBox(Color.cyan, new Rect(15f, 10f, 260f, 280f), 1);
			}
			if (espvisual.ShowDistance || espvisual.ShowName)
			{
				GUI.Label(new Rect(110f, 300f, 335f, 22f), string.Concat(new string[]
				{
					"<size=",
					espvisual.FixedTextSize.ToString(),
					">",
					espvisual.ShowDistance ? "[50]" : "",
					espvisual.ShowName ? " AIRDROP" : "",
					ESPOptions.ShowPlayerWeapon ? ((espvisual.ShowDistance || espvisual.ShowName) ? "" : "") : "",
					"</size>"
				}));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000A7CC File Offset: 0x000089CC
		private static void DrawGlobals2(ESPTarget esptarget)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUILayout.Space(2f);
			GUILayout.Label("Max Render Distance: " + espvisual.Distance.ToString(), Array.Empty<GUILayoutOption>());
			espvisual.Distance = (float)((int)GUILayout.HorizontalSlider(espvisual.Distance, 0f, 3000f, Array.Empty<GUILayoutOption>()));
			GUILayout.Space(2f);
			GUILayout.Label("Font Size: " + espvisual.FixedTextSize.ToString(), Array.Empty<GUILayoutOption>());
			espvisual.FixedTextSize = (int)GUILayout.HorizontalSlider((float)espvisual.FixedTextSize, 0f, 24f, Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000A878 File Offset: 0x00008A78
		private static void DrawGlobals(ESPTarget esptarget, string objname)
		{
			ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
			GUILayout.Space(2f);
			espvisual.Enabled = GUILayout.Toggle(espvisual.Enabled, objname + " ESP Enabled", Array.Empty<GUILayoutOption>());
			espvisual.Boxes = GUILayout.Toggle(espvisual.Boxes, "Box", Array.Empty<GUILayoutOption>());
			espvisual.LineToObject = GUILayout.Toggle(espvisual.LineToObject, "Snaplines", Array.Empty<GUILayoutOption>());
			espvisual.ShowName = GUILayout.Toggle(espvisual.ShowName, "Name", Array.Empty<GUILayoutOption>());
			espvisual.ShowDistance = GUILayout.Toggle(espvisual.ShowDistance, "Distance", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x040000BC RID: 188
		public static ESPTarget SelectedObject = ESPTarget.Player;

		// Token: 0x040000BD RID: 189
		private static Vector2 scrollPosition;

		// Token: 0x040000BE RID: 190
		private static Vector2 scrollPosition3 = new Vector2(0f, 0f);

		// Token: 0x040000BF RID: 191
		private static Vector2 scrollPosition2 = new Vector2(0f, 0f);
	}
}
