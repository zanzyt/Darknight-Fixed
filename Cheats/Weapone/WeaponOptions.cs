using System;

namespace ZahidAGA
{
	// Token: 0x0200007E RID: 126
	public static class WeaponOptions
	{
		// Token: 0x04000274 RID: 628
		[Save]
		public static bool ShowWeaponInfo = false;

		// Token: 0x04000275 RID: 629
		[Save]
		public static bool HeatShot = false;

		// Token: 0x04000276 RID: 630
		[Save]
		public static float HeatShotVolume = 1f;

		// Token: 0x04000277 RID: 631
		[Save]
		public static float HitmarkerSoundVolume = 1f;

		// Token: 0x04000278 RID: 632
		public static bool HitmarkerBonk = true;

		// Token: 0x04000279 RID: 633
		public static bool HitmarkerSoundStatus = true;

		// Token: 0x0400027A RID: 634
		public static bool csgo = false;

		// Token: 0x0400027B RID: 635
		public static bool rust = false;

		// Token: 0x0400027C RID: 636
		public static bool skeet = false;

		// Token: 0x0400027D RID: 637
		public static string HitmarkerName = "defualt";

		// Token: 0x0400027E RID: 638
		[Save]
		public static int TracerTime = 2;

		// Token: 0x0400027F RID: 639
		[Save]
		public static bool Tracers = false;

		// Token: 0x04000280 RID: 640
		public static bool RemoveHammerDelay = false;

		// Token: 0x04000281 RID: 641
		public static bool RemoveBurstDelay = false;

		// Token: 0x04000282 RID: 642
		public static bool InstantReload = false;

		// Token: 0x04000283 RID: 643
		[Save]
		public static bool SafeZone = false;

		// Token: 0x04000284 RID: 644
		[Save]
		public static bool Admin = false;

		// Token: 0x04000285 RID: 645
		[Save]
		public static bool DamageIndicators = false;

		// Token: 0x04000286 RID: 646
		[Save]
		public static bool CustomCrosshair = false;

		// Token: 0x04000287 RID: 647
		[Save]
		public static SerializableColor CrosshairColor = new SerializableColor(255, 0, 0);

		// Token: 0x04000288 RID: 648
		[Save]
		public static bool NoRecoil = false;

		// Token: 0x04000289 RID: 649
		[Save]
		public static float NoRecoil1 = 0f;

		// Token: 0x0400028A RID: 650
		[Save]
		public static float NoSpread1 = 0f;

		// Token: 0x0400028B RID: 651
		[Save]
		public static bool NoSpread = false;

		// Token: 0x0400028C RID: 652
		[Save]
		public static bool NoSway = false;

		// Token: 0x0400028D RID: 653
		[Save]
		public static bool NoDrop = false;

		// Token: 0x0400028E RID: 654
		[Save]
		public static bool RemoveReloadDelay = false;

		// Token: 0x0400028F RID: 655
		[Save]
		public static bool OofOnDeath = false;

		// Token: 0x04000290 RID: 656
		[Save]
		public static bool Cod = false;

		// Token: 0x04000291 RID: 657
		[Save]
		public static bool coin = false;

		// Token: 0x04000292 RID: 658
		[Save]
		public static bool hypixe = false;

		// Token: 0x04000293 RID: 659
		[Save]
		public static bool Ez4ence = false;

		// Token: 0x04000294 RID: 660
		[Save]
		public static bool sigma = true;

		// Token: 0x04000295 RID: 661
		[Save]
		public static float KillSoundVolume = 2f;

		// Token: 0x04000296 RID: 662
		[Save]
		public static bool AutoReload = false;

		// Token: 0x04000297 RID: 663
		[Save]
		public static bool EnableBulletDropPrediction = true;

		// Token: 0x04000298 RID: 664
		[Save]
		public static bool HighlightBulletDropPredictionTarget = true;

		// Token: 0x04000299 RID: 665
		[Save]
		public static bool Zoom;

		// Token: 0x0400029A RID: 666
		[Save]
		public static float ZoomValue = 16f;
	}
}
