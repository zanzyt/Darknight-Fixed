using System;

namespace ZahidAGA
{
	// Token: 0x02000081 RID: 129
	public static class ItemOptions
	{
		// Token: 0x040002A4 RID: 676
		[Save]
		public static bool AutoItemPickup = false;

		// Token: 0x040002A5 RID: 677
		[Save]
		public static bool AutoItemPickupFiltresiz = false;

		// Token: 0x040002A6 RID: 678
		[Save]
		public static float ItemPickupDelayFiltresizDelay = 1f;

		// Token: 0x040002A7 RID: 679
		[Save]
		public static int ItemPickupDelayFiltresizRange = 20;

		// Token: 0x040002A8 RID: 680
		[Save]
		public static float ItemPickupDelay = 1f;

		// Token: 0x040002A9 RID: 681
		[Save]
		public static int ItemPickupRange = 20;

		// Token: 0x040002AA RID: 682
		[Save]
		public static ItemOptionList ItemFilterOptions = new ItemOptionList();

		// Token: 0x040002AB RID: 683
		[Save]
		public static ItemOptionList ItemESPOptions = new ItemOptionList();
	}
}
