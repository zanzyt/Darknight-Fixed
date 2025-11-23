using System;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x0200007A RID: 122
	public static class AimbotOptions
	{
		// Token: 0x04000240 RID: 576
		[Save]
		public static bool Enabled = false;

		// Token: 0x04000241 RID: 577
		[Save]
		public static float SelectedFOV = 15f;

		// Token: 0x04000242 RID: 578
		[Save]
		public static bool ShowAimUseFOV = true;

		// Token: 0x04000243 RID: 579
		[Save]
		public static bool AimUseFOV = false;

		// Token: 0x04000244 RID: 580
		[Save]
		public static bool UseGunDistance = false;

		// Token: 0x04000245 RID: 581
		[Save]
		public static int HitChance = 100;

		// Token: 0x04000246 RID: 582
		[Save]
		public static bool Smooth = false;

		// Token: 0x04000247 RID: 583
		[Save]
		public static bool OnKey = true;

		// Token: 0x04000248 RID: 584
		[Save]
		public static int HitboxSize = 15;

		// Token: 0x04000249 RID: 585
		public static float MaxSpeed = 20f;

		// Token: 0x0400024A RID: 586
		[Save]
		public static float AimSpeed = 5f;

		// Token: 0x0400024B RID: 587
		[Save]
		public static float Distance = 300f;

		// Token: 0x0400024C RID: 588
		[Save]
		public static ELimb TargetLimb = ELimb.SKULL;

		// Token: 0x0400024D RID: 589
		[Save]
		public static TargetMode TargetMode = TargetMode.FOV;

		// Token: 0x0400024E RID: 590
		[Save]
		public static bool NoAimbotDrop = false;
	}
}
