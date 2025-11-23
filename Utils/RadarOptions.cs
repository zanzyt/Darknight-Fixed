using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000088 RID: 136
	public static class RadarOptions
	{
		// Token: 0x04000359 RID: 857
		[Save]
		public static bool Enabled = false;

		// Token: 0x0400035A RID: 858
		[Save]
		public static int type = 3;

		// Token: 0x0400035B RID: 859
		[Save]
		public static bool DetialedPlayers = false;

		// Token: 0x0400035C RID: 860
		[Save]
		public static bool ShowPlayers = false;

		// Token: 0x0400035D RID: 861
		[Save]
		public static bool ShowVehicles = false;

		// Token: 0x0400035E RID: 862
		[Save]
		public static bool ShowVehiclesUnlocked = false;

		// Token: 0x0400035F RID: 863
		[Save]
		public static bool ShowDeathPosition = false;

		// Token: 0x04000360 RID: 864
		[Save]
		public static float RadarZoom = 1f;

		// Token: 0x04000361 RID: 865
		[Save]
		public static float RadarSize = 300f;

		// Token: 0x04000362 RID: 866
		[Save]
		public static SerializableRect vew = new Rect((float)Screen.width - RadarOptions.RadarSize - 20f, 10f, RadarOptions.RadarSize + 10f, RadarOptions.RadarSize + 10f);

		// Token: 0x04000363 RID: 867
		public static float cumbo = 90f;

		// Token: 0x04000364 RID: 868
		public static float cumbo2 = 90f;
	}
}
