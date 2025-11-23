using System;
using UnityEngine;
using ZahidAGA;

namespace DarkNight
{
	// Token: 0x020000C3 RID: 195
	public static class Loader
	{
		// Token: 0x06000352 RID: 850 RVA: 0x00019E00 File Offset: 0x00018000
		public static void Load()
		{
			Loader.oko = new GameObject();
			global::UnityEngine.Object.DontDestroyOnLoad(Loader.oko);
			Loader.oko.AddComponent<Manager>();
		}

		// Token: 0x040003F1 RID: 1009
		public static GameObject oko;

		// Token: 0x040003F2 RID: 1010
		public static string Name = "Exploithub";

		// Token: 0x040003F3 RID: 1011
		public static int Gün = -1;

		// Token: 0x040003F4 RID: 1012
		public static string assett = "https://github.com/Kovalsky1243/Cheat/blob/main/DarkNight.bundle?raw=true";

		// Token: 0x040003F5 RID: 1013
		public static string appdata2 = Environment.ExpandEnvironmentVariables("%appdata%"); //you want this dll?
	}
}
