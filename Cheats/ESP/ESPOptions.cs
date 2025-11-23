using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000085 RID: 133
	public static class ESPOptions
	{
		// Token: 0x0400031C RID: 796
		public static ShaderType ChamType = ShaderType.None;

		// Token: 0x0400031D RID: 797
		[Save]
		public static bool Enabled = true;

		// Token: 0x0400031E RID: 798
		[Save]
		public static bool ChamsEnabled = false;

		// Token: 0x0400031F RID: 799
		[Save]
		public static bool Ignore = false;

		// Token: 0x04000320 RID: 800
		[Save]
		public static bool Skeleton = false;

		// Token: 0x04000321 RID: 801
		[Save]
		public static bool Tran = false;

		// Token: 0x04000322 RID: 802
		public static bool EspStyle = true;

		// Token: 0x04000323 RID: 803
		public static bool EspStyle1 = false;

		// Token: 0x04000324 RID: 804
		public static bool EspStyle2 = false;

		// Token: 0x04000325 RID: 805
		public static bool EspStyle3 = false;

		// Token: 0x04000326 RID: 806
		public static bool EspStyle4 = false;

		// Token: 0x04000327 RID: 807
		public static bool EspStyle5 = false;

		// Token: 0x04000328 RID: 808
		[Save]
		public static float xxx = 50f;

		// Token: 0x04000329 RID: 809
		[Save]
		public static float yyy = 50f;

		// Token: 0x0400032A RID: 810
		[Save]
		public static float zzz = 50f;

		// Token: 0x0400032B RID: 811
		[Save]
		public static float bnmk = 50f;

		// Token: 0x0400032C RID: 812
		[Save]
		public static bool SelfChams = false;

		// Token: 0x0400032D RID: 813
		[Save]
		public static float ChamsTime = 2f;

		// Token: 0x0400032E RID: 814
		[Save]
		public static bool ChamsFlat = false;

		// Token: 0x0400032F RID: 815
		[Save]
		public static bool RGBChams = false;

		// Token: 0x04000330 RID: 816
		[Save]
		public static bool SpinBotOyuncu = false;

		// Token: 0x04000331 RID: 817
		[Save]
		public static bool SpinBotAraç = false;

		// Token: 0x04000332 RID: 818
		[Save]
		public static bool ShowVanishPlayers = false;

		// Token: 0x04000333 RID: 819
		[Save]
		public static bool ShowVehiclePlayers = false;

		// Token: 0x04000334 RID: 820
		[Save]
		public static bool ShowToolTipWindow = false;

		// Token: 0x04000335 RID: 821
		[Save]
		public static bool ShowCoordinates = false;

		// Token: 0x04000336 RID: 822
		[Save]
		public static bool ShowMap = false;

		// Token: 0x04000337 RID: 823
		public static bool ShowMap2 = false;

		// Token: 0x04000338 RID: 824
		[Save]
		public static ESPVisual[] VisualOptions = Enumerable.Repeat<ESPVisual>(new ESPVisual
		{
			Enabled = false,
			Boxes = false,
			ShowName = true,
			ShowDistance = true,
			ShowAngle = false,
			TwoDimensional = false,
			Glow = false,
			InfiniteDistance = false,
			LineToObject = false,
			TextScaling = false,
			UseObjectCap = false,
			CustomTextColor = false,
			Distance = 500f,
			Location = LabelLocation.BottomMiddle,
			FixedTextSize = 11,
			MinTextSize = 8,
			MaxTextSize = 11,
			MinTextSizeDistance = 800f,
			BorderStrength = 0,
			ObjectCap = 24
		}, Enum.GetValues(typeof(ESPTarget)).Length).ToArray<ESPVisual>();

		// Token: 0x04000339 RID: 825
		[Save]
		public static Dictionary<ESPTarget, int> PriorityTable = Enum.GetValues(typeof(ESPTarget)).Cast<ESPTarget>().ToDictionary((ESPTarget x) => x, (ESPTarget x) => (int)x);

		// Token: 0x0400033A RID: 826
		[Save]
		public static bool ShowPlayerWeapon = false;

		// Token: 0x0400033B RID: 827
		[Save]
		public static bool showhotbar = false;

		// Token: 0x0400033C RID: 828
		[Save]
		public static bool HitboxSize = false;

		// Token: 0x0400033D RID: 829
		public static int x = 50;

		// Token: 0x0400033E RID: 830
		public static int y = 50;

		// Token: 0x0400033F RID: 831
		public static int cumbox = 50;

		// Token: 0x04000340 RID: 832
		public static int cumboy = 50;

		// Token: 0x04000341 RID: 833
		public static bool ShowAmmo = false;

		// Token: 0x04000342 RID: 834
		[Save]
		public static bool ShowPlayerVehicle = false;

		// Token: 0x04000343 RID: 835
		[Save]
		public static bool ArkadaşRengi = true;

		// Token: 0x04000344 RID: 836
		[Save]
		public static bool AdminRengi = true;

		// Token: 0x04000345 RID: 837
		[Save]
		public static bool UsePlayerGroup = true;

        [Save]
        public static SerializableColor SameGroupColor = ((Color32)Color.green).ToSerializableColor();

        // Token: 0x04000347 RID: 839
        [Save]
		public static bool FilterItems = false;

		// Token: 0x04000348 RID: 840
		[Save]
		public static bool ShowVehicleFuel;

		// Token: 0x04000349 RID: 841
		[Save]
		public static bool ShowVehicleHealth;

		// Token: 0x0400034A RID: 842
		[Save]
		public static bool ShowVehicleLocked;

		// Token: 0x0400034B RID: 843
		[Save]
		public static bool FilterVehicleLocked;

		// Token: 0x0400034C RID: 844
		[Save]
		public static bool ShowStorageItem;

		// Token: 0x0400034D RID: 845
		[Save]
		public static bool ShowSentryItem;

		// Token: 0x0400034E RID: 846
		[Save]
		public static bool ShowClaimed;

		// Token: 0x0400034F RID: 847
		[Save]
		public static bool ClaimİDStorage;

		// Token: 0x04000350 RID: 848
		[Save]
		public static bool ClaimNameStorage;

		// Token: 0x04000351 RID: 849
		[Save]
		public static bool ClaimİDCar;

		// Token: 0x04000352 RID: 850
		[Save]
		public static bool ClaimNameCar;

		// Token: 0x04000353 RID: 851
		[Save]
		public static bool ClaimİDBed;

		// Token: 0x04000354 RID: 852
		[Save]
		public static bool ClaimNameBed;

		// Token: 0x04000355 RID: 853
		[Save]
		public static bool ShowGeneratorFuel;

		// Token: 0x04000356 RID: 854
		[Save]
		public static bool ShowGeneratorPowered;
	}
}
