using System;
using System.Collections.Generic;

namespace ZahidAGA
{
	// Token: 0x0200007F RID: 127
	public static class HotkeyOptions
	{
		// Token: 0x0400029B RID: 667
		[Save]
		public static Dictionary<string, Dictionary<string, Hotkey>> HotkeyDict = new Dictionary<string, Dictionary<string, Hotkey>>();

		// Token: 0x0400029C RID: 668
		[Save]
		public static Dictionary<string, Hotkey> UnorganizedHotkeys = new Dictionary<string, Hotkey>();
	}
}
