using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x02000096 RID: 150
	public class OV_PlayerQuests
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x00003EB4 File Offset: 0x000020B4
		[Override(typeof(PlayerQuests), "isMemberOfSameGroupAs", BindingFlags.Instance | BindingFlags.Public, 0)]
		public bool OV_isMemberOfSameGroupAs(Player player)
		{
			return ESPOptions.ShowMap;
		}
	}
}
