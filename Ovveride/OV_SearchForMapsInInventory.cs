using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x02000098 RID: 152
	public static class OV_SearchForMapsInInventory
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00003ECD File Offset: 0x000020CD
		[Override(typeof(PlayerDashboardInformationUI), "searchForMapsInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_searchForMapsInInventory(ref bool enableChart, ref bool enableMap)
		{
			if (MiscOptions.GPS)
			{
				enableMap = true;
				enableChart = true;
				return;
			}
			OverrideUtilities.CallOriginal(null, new object[] { true, true });
		}
	}
}
