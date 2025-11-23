using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000029 RID: 41
	[Component]
	public class HotkeyComponent : MonoBehaviour
	{
		// Token: 0x0600006F RID: 111 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public void Update()
		{
			if (HotkeyComponent.NeedsKeys)
			{
				List<KeyCode> list = HotkeyComponent.CurrentKeys.ToList<KeyCode>();
				HotkeyComponent.CurrentKeys.Clear();
				foreach (KeyCode keyCode in HotkeyComponent.Keys)
				{
					if (Input.GetKey(keyCode))
					{
						HotkeyComponent.CurrentKeys.Add(keyCode);
					}
				}
				if (HotkeyComponent.CurrentKeys.Count < HotkeyComponent.CurrentKeyCount && HotkeyComponent.CurrentKeyCount > 0)
				{
					HotkeyComponent.CurrentKeys = list;
					HotkeyComponent.StopKeys = true;
				}
				HotkeyComponent.CurrentKeyCount = HotkeyComponent.CurrentKeys.Count;
			}
			if (!MenuComponent.IsInMenu)
			{
				foreach (KeyValuePair<string, System.Action> keyValuePair in HotkeyComponent.ActionDict)
				{
					if ((!MiscOptions.PanicMode || keyValuePair.Key == "_PanicButton") && HotkeyUtilities.IsHotkeyDown(keyValuePair.Key))
					{
						keyValuePair.Value();
					}
				}
				return;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000023AF File Offset: 0x000005AF
		public static void Clear()
		{
			HotkeyComponent.NeedsKeys = false;
			HotkeyComponent.StopKeys = false;
			HotkeyComponent.CurrentKeyCount = 0;
			HotkeyComponent.CurrentKeys = new List<KeyCode>();
		}

		// Token: 0x040000CD RID: 205
		public static bool NeedsKeys;

		// Token: 0x040000CE RID: 206
		public static bool StopKeys;

		// Token: 0x040000CF RID: 207
		public static int CurrentKeyCount;

		// Token: 0x040000D0 RID: 208
		public static List<KeyCode> CurrentKeys;

		// Token: 0x040000D1 RID: 209
		public static Dictionary<string, System.Action> ActionDict = new Dictionary<string, System.Action>();

		// Token: 0x040000D2 RID: 210
		public static KeyCode[] Keys = (KeyCode[])Enum.GetValues(typeof(KeyCode));
	}
}
