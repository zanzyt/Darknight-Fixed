using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using DarkNight;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000007 RID: 7
	public class AssetUtilities
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000523C File Offset: 0x0000343C
		public static void GetAssets()
		{
			if (!Directory.Exists(Loader.appdata2 + "/DarkNight//GUISkins/"))
			{
				Directory.CreateDirectory(Loader.appdata2 + "/DarkNight//GUISkins/");
			}
			if (!Directory.Exists(Loader.appdata2 + "/DarkNight//CustomScreenShot/"))
			{
				Directory.CreateDirectory(Loader.appdata2 + "/DarkNight//CustomScreenShot/");
			}
			AssetBundle assetBundle = AssetBundle.LoadFromMemory(new WebClient().DownloadData(Loader.assett));
			if (assetBundle)
			{
				T.AddNotification("<b>[!]</b>    Assets Loaded");
			}
			foreach (Shader shader in assetBundle.LoadAllAssets<Shader>())
			{
				AssetUtilities.Shaders.Add(shader.name, shader);
			}
			foreach (Font font in assetBundle.LoadAllAssets<Font>())
			{
				AssetUtilities.Fonts.Add(font.name, font);
			}
			foreach (AudioClip audioClip in assetBundle.LoadAllAssets<AudioClip>())
			{
				AssetUtilities.AudioClip.Add(audioClip.name, audioClip);
			}
			foreach (Texture2D texture2D in assetBundle.LoadAllAssets<Texture2D>())
			{
				if (texture2D.name != "Font Texture")
				{
					AssetUtilities.Textures.Add(texture2D.name, texture2D);
				}
			}
			Main._Aimbot = AssetUtilities.Textures["aimbot"];
			Main._Other = AssetUtilities.Textures["misc"];
			Main._Settings = AssetUtilities.Textures["settings"];
			Main._Player = AssetUtilities.Textures["players"];
			Main._Other = AssetUtilities.Textures["misc"];
			Main._Visual = AssetUtilities.Textures["visual"];
			Main._Skin = AssetUtilities.Textures["skins"];
			Main._Keyboard = AssetUtilities.Textures["keyboard"];
			Main._Logo2 = AssetUtilities.Textures["holywarelogo"];
			Main._User = AssetUtilities.Textures["battleye"];
			Main.Player = AssetUtilities.Textures["player"];
			Main.ChPlayer = AssetUtilities.Textures["ChamsPlayer"];
			Main.zombie = AssetUtilities.Textures["zombie"];
			Main.sentiri = AssetUtilities.Textures["sentiri"];
			Main.bed = AssetUtilities.Textures["yatak"];
			Main.vehicle = AssetUtilities.Textures["vehicle"];
			Main.flag = AssetUtilities.Textures["claimflag"];
			Main.locker = AssetUtilities.Textures["locker"];
			Main.ıtem = AssetUtilities.Textures["ıtem"];
			Main.airdrop = AssetUtilities.Textures["airdrop"];
			Main.xray = AssetUtilities.Textures["Xray"];
			Main.Espstyle1 = AssetUtilities.Textures["EspStyle"];
			Main.Espstyle2 = AssetUtilities.Textures["SearchButton"];
			Main.Espstyle3 = AssetUtilities.Textures["sex"];
			Main.Espstyle4 = AssetUtilities.Textures["AktifButonsex"];
			Main.Espstyle5 = AssetUtilities.Textures["ballsackui_notif"];
			AssetUtilities.BonkClip = assetBundle.LoadAllAssets<AudioClip>().First<AudioClip>();
			AssetUtilities.VanillaSkin = assetBundle.LoadAllAssets<GUISkin>().First<GUISkin>();
			if (!string.IsNullOrEmpty(MiscOptions.UISkin))
			{
				AssetUtilities.LoadGUISkinFromName(MiscOptions.UISkin);
				return;
			}
			AssetUtilities.Skin = AssetUtilities.VanillaSkin;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000055DC File Offset: 0x000037DC
		public static void LoadGUISkinFromName(string name)
		{
			if (File.Exists(Loader.appdata2 + "/DarkNight//GUISkins/" + name + ".assets"))
			{
				AssetBundle assetBundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(Loader.appdata2 + "/DarkNight//GUISkins/" + name + ".assets"));
				AssetUtilities.Skin = assetBundle.LoadAllAssets<GUISkin>().First<GUISkin>();
				assetBundle.Unload(false);
				MiscOptions.UISkin = name;
				return;
			}
			AssetUtilities.Skin = AssetUtilities.VanillaSkin;
			MiscOptions.UISkin = "";
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00005658 File Offset: 0x00003858
		public static List<string> GetSkins(bool Extensions = false)
		{
			List<string> list = new List<string>();
			foreach (FileInfo fileInfo in new DirectoryInfo(Loader.appdata2 + "/DarkNight//GUISkins/").GetFiles("*.assets"))
			{
				list.Add(fileInfo.Name.Substring(0, fileInfo.Name.Length));
			}
			return list;
		}

		// Token: 0x04000008 RID: 8
		public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();

		// Token: 0x04000009 RID: 9
		public static GUISkin Skin;

		// Token: 0x0400000A RID: 10
		public static GUISkin VanillaSkin;

		// Token: 0x0400000B RID: 11
		public static AudioClip BonkClip;

		// Token: 0x0400000C RID: 12
		public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

		// Token: 0x0400000D RID: 13
		public static Dictionary<string, Texture2D> sex = new Dictionary<string, Texture2D>();

		// Token: 0x0400000E RID: 14
		public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

		// Token: 0x0400000F RID: 15
		public static Dictionary<string, AudioClip> AudioClip = new Dictionary<string, AudioClip>();
	}
}
