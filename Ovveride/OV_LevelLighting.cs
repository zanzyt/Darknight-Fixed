using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x02000090 RID: 144
	public static class OV_LevelLighting
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00003CAA File Offset: 0x00001EAA
		[OnSpy]
		public static void Disable()
		{
			if (DrawUtilities.ShouldRun())
			{
				OV_LevelLighting.OV_updateLighting();
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00003CAA File Offset: 0x00001EAA
		[OffSpy]
		public static void Enable()
		{
			if (DrawUtilities.ShouldRun())
			{
				OV_LevelLighting.OV_updateLighting();
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00003CBB File Offset: 0x00001EBB
		[Initializer]
		public static void Init()
		{
			OV_LevelLighting.Time = typeof(LevelLighting).GetField("_time", BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000147C4 File Offset: 0x000129C4
		[Override(typeof(LevelLighting), "updateLighting", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_updateLighting()
		{
			float time = LevelLighting.time;
			if (!DrawUtilities.ShouldRun() || !MiscOptions.SetTimeEnabled || G.BeingSpied)
			{
				OverrideUtilities.CallOriginal(null, new object[0]);
				return;
			}
			OV_LevelLighting.Time.SetValue(null, MiscOptions.Time);
			OverrideUtilities.CallOriginal(null, new object[0]);
			OV_LevelLighting.Time.SetValue(null, time);
		}

		// Token: 0x0400037A RID: 890
		public static FieldInfo Time;

		// Token: 0x0400037B RID: 891
		public static bool WasEnabled;
	}
}
