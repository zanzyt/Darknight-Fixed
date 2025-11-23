using System;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000A4 RID: 164
	public static class LocationUtilities
	{
		// Token: 0x060002DF RID: 735 RVA: 0x000176CC File Offset: 0x000158CC
		public static LocationNode GetClosestLocation(Vector3 pos)
		{
			double num = 1337420.0;
			LocationNode locationNode = null;
			foreach (LocationNode locationNode2 in (from n in LevelNodes.nodes
				where n.type == ENodeType.LOCATION
				select (LocationNode)n).ToArray<LocationNode>())
			{
				double distance = VectorUtilities.GetDistance(pos, locationNode2.point);
				if (distance < num)
				{
					num = distance;
					locationNode = locationNode2;
				}
			}
			return locationNode;
		}
	}
}
