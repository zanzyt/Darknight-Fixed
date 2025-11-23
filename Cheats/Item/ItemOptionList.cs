using System;
using System.Collections.Generic;

namespace ZahidAGA
{
	// Token: 0x02000068 RID: 104
	public class ItemOptionList
	{
		// Token: 0x040001D7 RID: 471
		public HashSet<ushort> AddedItems = new HashSet<ushort>();

		// Token: 0x040001D8 RID: 472
		public bool ItemfilterGun = true;

		// Token: 0x040001D9 RID: 473
		public bool ItemfilterGunMeel = true;

		// Token: 0x040001DA RID: 474
		public bool ItemfilterAmmo = true;

		// Token: 0x040001DB RID: 475
		public bool ItemfilterMedical = true;

		// Token: 0x040001DC RID: 476
		public bool ItemfilterBackpack = true;

		// Token: 0x040001DD RID: 477
		public bool ItemfilterCharges = true;

		// Token: 0x040001DE RID: 478
		public bool ItemfilterFuel = true;

		// Token: 0x040001DF RID: 479
		public bool ItemfilterClothing = true;

		// Token: 0x040001E0 RID: 480
		public bool ItemfilterFoodAndWater = true;

		// Token: 0x040001E1 RID: 481
		public bool ItemfilterCustom = true;

		// Token: 0x040001E2 RID: 482
		public string searchstring = "";

		// Token: 0x040001E3 RID: 483
		public SerializableVector2 additemscroll = new SerializableVector2(0f, 0f);

		// Token: 0x040001E4 RID: 484
		public SerializableVector2 removeitemscroll = new SerializableVector2(0f, 0f);
	}
}
