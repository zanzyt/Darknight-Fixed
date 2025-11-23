using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000013 RID: 19
	public static class HotkeyTab
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000056BC File Offset: 0x000038BC
		public static void Tab()
		{
			GUILayout.BeginArea(new Rect(340f, 20f, 650f, 610f), "Hotkeys", "box");
			HotkeyTab.scrollPosition1 = GUILayout.BeginScrollView(HotkeyTab.scrollPosition1, Array.Empty<GUILayoutOption>());
			foreach (KeyValuePair<string, Dictionary<string, Hotkey>> keyValuePair in HotkeyOptions.HotkeyDict)
			{
				if (HotkeyTab.IsFirst)
				{
					HotkeyTab.IsFirst = false;
					HotkeyTab.DrawSpacer(keyValuePair.Key, true);
				}
				else
				{
					HotkeyTab.DrawSpacer(keyValuePair.Key, false);
				}
				foreach (KeyValuePair<string, Hotkey> keyValuePair2 in keyValuePair.Value)
				{
					HotkeyTab.DrawButton(keyValuePair2.Value.Name, keyValuePair2.Key);
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002120 File Offset: 0x00000320
		public static void DrawSpacer(string Text, bool First)
		{
			if (!First)
			{
				GUILayout.Space(10f);
			}
			GUILayout.Label(Text, new GUILayoutOption[0]);
			GUILayout.Space(8f);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000057D4 File Offset: 0x000039D4
		public static void DrawButton(string Option, string Identifier)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(Option, new GUILayoutOption[0]);
			if (HotkeyTab.ClickedOption == Identifier)
			{
				if (GUILayout.Button("Put away", "SelectedButton", Array.Empty<GUILayoutOption>()))
				{
					HotkeyComponent.Clear();
					HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = new KeyCode[0];
					HotkeyTab.ClickedOption = "";
				}
				if (!HotkeyComponent.StopKeys)
				{
					string text;
					if (HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0)
					{
						text = string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
						{
							KeyCode keyCode = k;
							return keyCode.ToString();
						}).ToArray<string>());
					}
					else
					{
						text = "Not assigned";
					}
					GUILayout.Button(text, "SelectedButton", Array.Empty<GUILayoutOption>());
				}
				else
				{
					HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = HotkeyComponent.CurrentKeys.ToArray();
					HotkeyComponent.Clear();
					GUILayout.Button(string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
					{
						KeyCode keyCode2 = k;
						return keyCode2.ToString();
					}).ToArray<string>()), new GUILayoutOption[0]);
					HotkeyTab.ClickedOption = "";
				}
			}
			else
			{
				string text2;
				if (HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0)
				{
					text2 = string.Join(" + ", HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Select(delegate(KeyCode k)
					{
						KeyCode keyCode3 = k;
						return keyCode3.ToString();
					}).ToArray<string>());
				}
				else
				{
					text2 = "Not assigned";
				}
				if (GUILayout.Button(text2, "SelectedButton", Array.Empty<GUILayoutOption>()))
				{
					HotkeyComponent.Clear();
					HotkeyTab.ClickedOption = Identifier;
					HotkeyComponent.NeedsKeys = true;
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(2f);
		}

		// Token: 0x04000068 RID: 104
		private static Vector2 scrollPosition1 = new Vector2(0f, 0f);

		// Token: 0x04000069 RID: 105
		public static Vector2 HotkeyScroll;

		// Token: 0x0400006A RID: 106
		public static string ClickedOption;

		// Token: 0x0400006B RID: 107
		public static bool IsFirst = true;
	}
}
