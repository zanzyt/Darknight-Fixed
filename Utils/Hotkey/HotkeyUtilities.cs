using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000A1 RID: 161
	public static class HotkeyUtilities
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x000172A0 File Offset: 0x000154A0
		public static void AddHotkey(string Group, string Name, string Identifier, params KeyCode[] DefaultKeys)
		{
			if (!HotkeyOptions.HotkeyDict.ContainsKey(Group))
			{
				HotkeyOptions.HotkeyDict.Add(Group, new Dictionary<string, Hotkey>());
			}
			Dictionary<string, Hotkey> dictionary = HotkeyOptions.HotkeyDict[Group];
			if (dictionary.ContainsKey(Identifier))
			{
				return;
			}
			Hotkey hotkey = new Hotkey
			{
				Name = Name,
				Keys = DefaultKeys
			};
			dictionary.Add(Identifier, hotkey);
			HotkeyOptions.UnorganizedHotkeys.Add(Identifier, hotkey);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00017308 File Offset: 0x00015508
		public static bool IsHotkeyDown(string Identifier)
		{
			return HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Any(new Func<KeyCode, bool>(Input.GetKeyDown)) && HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.All(new Func<KeyCode, bool>(Input.GetKey));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00004012 File Offset: 0x00002212
		public static bool IsHotkeyHeld(string Identifier)
		{
			return HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.All(new Func<KeyCode, bool>(Input.GetKey));
		}
	}
}
