using System;

namespace ZahidAGA
{
	// Token: 0x0200008A RID: 138
	public static class SkinOptions
	{
		// Token: 0x04000369 RID: 873
		[Save]
		public static SkinConfig SkinConfig = new SkinConfig();

		// Token: 0x0400036A RID: 874
		public static SkinOptionList SkinWeapons = new SkinOptionList(SkinType.Weapons);

		// Token: 0x0400036B RID: 875
		public static SkinOptionList SkinClothesShirts = new SkinOptionList(SkinType.Shirts);

		// Token: 0x0400036C RID: 876
		public static SkinOptionList SkinClothesPants = new SkinOptionList(SkinType.Pants);

		// Token: 0x0400036D RID: 877
		public static SkinOptionList SkinClothesBackpack = new SkinOptionList(SkinType.Bags);

		// Token: 0x0400036E RID: 878
		public static SkinOptionList SkinClothesVest = new SkinOptionList(SkinType.Vests);

		// Token: 0x0400036F RID: 879
		public static SkinOptionList SkinClothesHats = new SkinOptionList(SkinType.Hats);

		// Token: 0x04000370 RID: 880
		public static SkinOptionList SkinClothesMask = new SkinOptionList(SkinType.Masks);

		// Token: 0x04000371 RID: 881
		public static SkinOptionList SkinClothesGlasses = new SkinOptionList(SkinType.Glasses);
	}
}
