using System;
using System.Collections.Generic;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200008F RID: 143
	public static class OV_ItemManager
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00014750 File Offset: 0x00012950
		[Override(typeof(ItemManager), "getItemsInRadius", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_getItemsInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<InteractableItem> result)
		{
			if (MiscOptions.IncreaseNearbyItemDistance)
			{
				OverrideUtilities.CallOriginal(null, new object[]
				{
					center,
					Mathf.Pow(MiscOptions.NearbyItemDistance, 2f),
					search,
					result
				});
				return;
			}
			OverrideUtilities.CallOriginal(null, new object[] { center, sqrRadius, search, result });
		}
	}
}
