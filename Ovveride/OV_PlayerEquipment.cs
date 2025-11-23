using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x02000091 RID: 145
	public class OV_PlayerEquipment
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00003CD8 File Offset: 0x00001ED8
		[Override(typeof(PlayerEquipment), "punch", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public void OV_punch(EPlayerPunch p)
		{
			if (MiscOptions.PunchSilentAim)
			{
				OV_DamageTool.OVType = OverrideType.PlayerHit;
			}
			OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment, new object[] { p });
			OV_DamageTool.OVType = OverrideType.None;
		}

		// Token: 0x0400037C RID: 892
		public static bool WasPunching;

		// Token: 0x0400037D RID: 893
		public static uint CurrSim;
	}
}
