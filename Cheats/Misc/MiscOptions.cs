using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000082 RID: 130
	public static class MiscOptions
	{
		// Token: 0x040002AC RID: 684
		[Save]
		public static bool VehicleRigibody;

		// Token: 0x040002AD RID: 685
		[Save]
		public static bool ShowVanish;

		// Token: 0x040002AE RID: 686
		public static bool ShowAdmin;

		// Token: 0x040002AF RID: 687
		[Save]
		public static bool Adam;

		// Token: 0x040002B0 RID: 688
		public static Dictionary<string, Color32> GlobalColors = new Dictionary<string, Color32>();

		// Token: 0x040002B1 RID: 689
		[Save]
		public static bool Adam2;

		// Token: 0x040002B2 RID: 690
		[Save]
		public static bool Fovv;

		// Token: 0x040002B3 RID: 691
		[Save]
		public static bool HwidChanger = true;

		// Token: 0x040002B4 RID: 692
		public static bool spynofity;

		// Token: 0x040002B5 RID: 693
		public static bool Skingöster;

		// Token: 0x040002B6 RID: 694
		[Save]
		public static bool fovoç1 = false;

		// Token: 0x040002B7 RID: 695
		[Save]
		public static float fovoç2 = 90f;

		// Token: 0x040002B8 RID: 696
		[Save]
		public static bool AllOnMap = false;

		// Token: 0x040002B9 RID: 697
		[Save]
		public static bool BuildObs;

		// Token: 0x040002BA RID: 698
		public static bool water;

		// Token: 0x040002BB RID: 699
		[Save]
		public static bool ChatGlobal = true;

		// Token: 0x040002BC RID: 700
		[Save]
		public static bool ChatArea = false;

		// Token: 0x040002BD RID: 701
		[Save]
		public static bool ChatGroup = false;

		// Token: 0x040002BE RID: 702
		[Save]
		public static bool ChatSay = false;

		// Token: 0x040002BF RID: 703
		public static Vector3 pos;

		// Token: 0x040002C0 RID: 704
		public static bool PanicMode = false;

		// Token: 0x040002C1 RID: 705
		[Save]
		public static bool hang = false;

		// Token: 0x040002C2 RID: 706
		[Save]
		public static bool PunchSilentAim = false;

		// Token: 0x040002C3 RID: 707
		public static float RunspeedMult = 5f;

		// Token: 0x040002C4 RID: 708
		public static float JumpMult = 10f;

		// Token: 0x040002C5 RID: 709
		public static string UISkin = "";

		// Token: 0x040002C6 RID: 710
		public static string FontName = "";

		// Token: 0x040002C7 RID: 711
		[Save]
		public static bool FastSell;

		// Token: 0x040002C8 RID: 712
		[Save]
		public static bool FastBuy;

		// Token: 0x040002C9 RID: 713
		[Save]
		public static int FastSellCount = 5;

		// Token: 0x040002CA RID: 714
		[Save]
		public static bool EnVehicle = false;

		// Token: 0x040002CB RID: 715
		[Save]
		public static bool BuildinObstacles = false;

		// Token: 0x040002CC RID: 716
		[Save]
		public static bool FToplama = false;

		// Token: 0x040002CD RID: 717
		[Save]
		public static bool NoFlash = false;

		// Token: 0x040002CE RID: 718
		[Save]
		public static bool PunchAura = false;

		// Token: 0x040002CF RID: 719
		[Save]
		public static bool NoCloud = false;

		// Token: 0x040002D0 RID: 720
		[Save]
		public static bool NoGrass = true;

		// Token: 0x040002D1 RID: 721
		[Save]
		public static bool GPS = false;

		// Token: 0x040002D2 RID: 722
		[Save]
		public static bool NoSnow = false;

		// Token: 0x040002D3 RID: 723
		[Save]
		public static bool IsRussian = false;

		// Token: 0x040002D4 RID: 724
		[Save]
		public static bool IsEnglish = true;

		// Token: 0x040002D5 RID: 725
		[Save]
		public static bool NoRain = false;

		// Token: 0x040002D6 RID: 726
		[Save]
		public static bool banbypass = false;

		// Token: 0x040002D7 RID: 727
		[Save]
		public static bool NoFlinch = false;

		// Token: 0x040002D8 RID: 728
		[Save]
		public static bool NoGrayscale = false;

		// Token: 0x040002D9 RID: 729
		[Save]
		public static bool CustomSalvageTime = false;

		// Token: 0x040002DA RID: 730
		[Save]
		public static float SalvageTime = 0.2f;

		// Token: 0x040002DB RID: 731
		[Save]
		public static bool SetTimeEnabled = false;

		// Token: 0x040002DC RID: 732
		[Save]
		public static float Time = 0.4f;

		// Token: 0x040002DD RID: 733
		[Save]
		public static bool SlowFall = false;

		// Token: 0x040002DE RID: 734
		[Save]
		public static bool AirStick = false;

		// Token: 0x040002DF RID: 735
		[Save]
		public static bool Compass = false;

		// Token: 0x040002E0 RID: 736
		[Save]
		public static bool Bones = false;

		// Token: 0x040002E1 RID: 737
		[Save]
		public static bool ShowPlayersOnMap = false;

		// Token: 0x040002E2 RID: 738
		[Save]
		public static bool NightVision = false;

		// Token: 0x040002E3 RID: 739
		[Save]
		public static bool NightVision2 = false;

		// Token: 0x040002E4 RID: 740
		[Save]
		public static bool NightVision3 = false;

		// Token: 0x040002E5 RID: 741
		[Save]
		public static bool CIVILIAN = false;

		// Token: 0x040002E6 RID: 742
		[Save]
		public static bool HEADLAMP = false;

		// Token: 0x040002E7 RID: 743
		public static bool WasNightVision = false;

		// Token: 0x040002E8 RID: 744
		public static bool WasNightVision2 = false;

		// Token: 0x040002E9 RID: 745
		public static bool WasNightVision3 = false;

		// Token: 0x040002EA RID: 746
		[Save]
		public static string KillSpamText = "https://discord.gg/ucGP5WUs";

		// Token: 0x040002EB RID: 747
		[Save]
		public static bool KillSpam = false;

		// Token: 0x040002EC RID: 748
		[Save]
		public static string SpamText = "https://fakecrime.bio/zanzyt";

		// Token: 0x040002ED RID: 749
		[Save]
		public static bool Spam = false;
		 //afk
		// Token: 0x040002EE RID: 750
		[Save]
		public static string NickName = "";

		// Token: 0x040002EF RID: 751
		[Save]
		public static string Password = "";

		// Token: 0x040002F0 RID: 752
		[Save]
		public static bool SpammerEnabled = false;

		// Token: 0x040002F1 RID: 753
		[Save]
		public static int SpammerDelay = 3000;

		// Token: 0x040002F2 RID: 754
		[Save]
		public static bool VehicleFly = false;

		// Token: 0x040002F3 RID: 755
		[Save]
		public static bool VehicleUseMaxSpeed = false;

		// Token: 0x040002F4 RID: 756
		[Save]
		public static bool VehicleInfo = false;

		// Token: 0x040002F5 RID: 757
		[Save]
		public static float SpeedMultiplier = 0.85f;

		// Token: 0x040002F6 RID: 758
		[Save]
		public static bool ExtendMeleeRange;

		// Token: 0x040002F7 RID: 759
		[Save]
		public static float MeleeRangeExtension = 15.5f;

		// Token: 0x040002F8 RID: 760
		public static bool NoMovementVerification = false;

		// Token: 0x040002F9 RID: 761
		[Save]
		public static bool AlwaysCheckMovementVerification = false;

		// Token: 0x040002FA RID: 762
		public static Player SpectatedPlayer;

		// Token: 0x040002FB RID: 763
		[Save]
		public static bool PlayerFlight = false;

		// Token: 0x040002FC RID: 764
		[Save]
		public static bool RunspeedMult2 = false;

		// Token: 0x040002FD RID: 765
		[Save]
		public static bool JumpMult2 = false;

		// Token: 0x040002FE RID: 766
		[Save]
		public static float FlightSpeedMultiplier = 1f;

		// Token: 0x040002FF RID: 767
		public static bool Freecam = false;

		// Token: 0x04000300 RID: 768
		public static bool DeleteCharacterAnimation = false;

		// Token: 0x04000301 RID: 769
		public static bool shake = false;

		// Token: 0x04000302 RID: 770
		[Save]
		public static HashSet<ulong> Friends = new HashSet<ulong>();

		// Token: 0x04000303 RID: 771
		[Save]
		public static int SCrashMethod = 1;

		// Token: 0x04000304 RID: 772
		[Save]
		public static int AntiSpyMethod = 0;

		// Token: 0x04000305 RID: 773
		[Save]
		public static int ShaderMethod = 0;

		// Token: 0x04000306 RID: 774
		[Save]
		public static string AntiSpyPath = "";

		// Token: 0x04000307 RID: 775
		public static string AntiSpyPath2 = "";

		// Token: 0x04000308 RID: 776
		[Save]
		public static bool AlertOnSpy = true;

		// Token: 0x04000309 RID: 777
		[Save]
		public static bool EnableDistanceCrash = false;

		// Token: 0x0400030A RID: 778
		[Save]
		public static float CrashDistance = 100f;

		// Token: 0x0400030B RID: 779
		[Save]
		public static bool CrashByName = false;

		// Token: 0x0400030C RID: 780
		[Save]
		public static List<string> CrashWords = new List<string>();

		// Token: 0x0400030D RID: 781
		[Save]
		public static List<string> CrashIDs = new List<string>();

		// Token: 0x0400030E RID: 782
		[Save]
		public static bool NearbyItemRaycast = true;

		// Token: 0x0400030F RID: 783
		[Save]
		public static bool IncreaseNearbyItemDistance = true;

		// Token: 0x04000310 RID: 784
		[Save]
		public static float NearbyItemDistance = 15f;

		// Token: 0x04000311 RID: 785
		[Save]
		public static bool ConnectKick = false;

		// Token: 0x04000312 RID: 786
		[Save]
		public static bool epos;

		// Token: 0x04000313 RID: 787
		public static float Altitude;
	}
}
