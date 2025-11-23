using System;
using System.Collections.Generic;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x0200007B RID: 123
	public static class RaycastOptions
	{
		// Token: 0x0400024F RID: 591
		[Save]
		public static bool Enabled = false;

		// Token: 0x04000250 RID: 592
		[Save]
		public static int FovMethod = 0;

		// Token: 0x04000251 RID: 593
		[Save]
		public static bool SlientInfo = false;

		// Token: 0x04000252 RID: 594
		[Save]
		public static bool SlientCizgi = false;

		// Token: 0x04000253 RID: 595
		[Save]
		public static bool TargetPlayers = true;

		// Token: 0x04000254 RID: 596
		[Save]
		public static bool TargetZombies = false;

		// Token: 0x04000255 RID: 597
		[Save]
		public static bool TargetSentries = false;

		// Token: 0x04000256 RID: 598
		[Save]
		public static bool TargetBeds = false;

		// Token: 0x04000257 RID: 599
		[Save]
		public static bool TargetAnimal = false;

		// Token: 0x04000258 RID: 600
		[Save]
		public static bool TargetClaimFlags = false;

		// Token: 0x04000259 RID: 601
		[Save]
		public static bool TargetVehicles = false;

		// Token: 0x0400025A RID: 602
		[Save]
		public static bool TargetStorage = false;

		// Token: 0x0400025B RID: 603
		[Save]
		public static bool ExpandHitboxes = false;

		// Token: 0x0400025C RID: 604
		[Save]
		public static bool Players = false;

		// Token: 0x0400025D RID: 605
		[Save]
		public static bool NoShootthroughthewalls = false;

		// Token: 0x0400025E RID: 606
		[Save]
		public static bool AlwaysHitHead = false;

		// Token: 0x0400025F RID: 607
		[Save]
		public static bool UseRandomLimb = false;

		// Token: 0x04000260 RID: 608
		[Save]
		public static bool UseCustomLimb = false;

		// Token: 0x04000261 RID: 609
		[Save]
		public static bool UseTargetMaterial = false;

		// Token: 0x04000262 RID: 610
		[Save]
		public static bool UseModifiedVector = false;

		// Token: 0x04000263 RID: 611
		[Save]
		public static bool Targetİnfo = false;

		// Token: 0x04000264 RID: 612
		[Save]
		public static bool EnablePlayerSelection = false;

		// Token: 0x04000265 RID: 613
		[Save]
		public static bool OnlyShootAtSelectedPlayer = false;

		// Token: 0x04000266 RID: 614
		[Save]
		public static bool SilentAimUseFOV = false;

		// Token: 0x04000267 RID: 615
		[Save]
		public static bool ShowSilentAimUseFOV = true;

		// Token: 0x04000268 RID: 616
		[Save]
		public static float FovKalınlık = 0.01f;

		// Token: 0x04000269 RID: 617
		[Save]
		public static float SilentAimFOV = 20f;

		// Token: 0x0400026A RID: 618
		[Save]
		public static HashSet<TargetPriority> Targets = new HashSet<TargetPriority>();

		// Token: 0x0400026B RID: 619
		[Save]
		public static TargetPriority Target = TargetPriority.Players;

		// Token: 0x0400026C RID: 620
		[Save]
		public static EPhysicsMaterial TargetMaterial = EPhysicsMaterial.ALIEN_DYNAMIC;

		// Token: 0x0400026D RID: 621
		[Save]
		public static ELimb TargetLimb = ELimb.SKULL;

		// Token: 0x0400026E RID: 622
		[Save]
		public static SerializableVector TargetRagdoll = new SerializableVector(0f, 10f, 0f);
	}
}
