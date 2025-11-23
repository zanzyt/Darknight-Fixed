using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x02000094 RID: 148
	public static class OV_PlayerLifeUI
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00003E29 File Offset: 0x00002029
		[Override(typeof(PlayerLifeUI), "hasCompassInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static bool OV_hasCompassInInventory()
		{
			return MiscOptions.Compass || (bool)OverrideUtilities.CallOriginal(null, new object[0]);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00003E45 File Offset: 0x00002045
		[Override(typeof(PlayerLifeUI), "updateGrayscale", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_updateGrayscale()
		{
			if (!MiscOptions.NoGrayscale)
			{
				OverrideUtilities.CallOriginal(null, new object[0]);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00003E5B File Offset: 0x0000205B
		[OnSpy]
		public static void Disable()
		{
			if (DrawUtilities.ShouldRun())
			{
				OV_PlayerLifeUI.WasCompassEnabled = MiscOptions.Compass;
				MiscOptions.Compass = false;
				PlayerLifeUI.updateCompass();
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00003E7C File Offset: 0x0000207C
		[OffSpy]
		public static void Enable()
		{
			if (DrawUtilities.ShouldRun())
			{
				MiscOptions.Compass = OV_PlayerLifeUI.WasCompassEnabled;
				PlayerLifeUI.updateCompass();
			}
		}

		// Token: 0x04000398 RID: 920
		public static bool WasCompassEnabled;
	}
}
