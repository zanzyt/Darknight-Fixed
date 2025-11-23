using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x0200009B RID: 155
	public class OV_UseableMelee
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00015BE0 File Offset: 0x00013DE0
		[Override(typeof(UseableMelee), "fire", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public static void OV_fire()
		{
			OV_DamageTool.OVType = OverrideType.None;
			if (RaycastOptions.Enabled && MiscOptions.ExtendMeleeRange)
			{
				OV_DamageTool.OVType = OverrideType.SilentAimMelee;
			}
			else if (RaycastOptions.Enabled)
			{
				OV_DamageTool.OVType = OverrideType.SilentAim;
			}
			else if (MiscOptions.ExtendMeleeRange)
			{
				OV_DamageTool.OVType = OverrideType.Extended;
			}
			OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment.useable, new object[0]);
			OV_DamageTool.OVType = OverrideType.None;
		}
	}
}
