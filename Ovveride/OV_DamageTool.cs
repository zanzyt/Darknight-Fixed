using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200008C RID: 140
	public static class OV_DamageTool
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00003C8F File Offset: 0x00001E8F
		[Override(typeof(DamageTool), "raycast", BindingFlags.Static | BindingFlags.Public, 1)]
		public static RaycastInfo OV_raycast(Ray ray, float range, int mask, Player ignorePlayer = null)
		{
			return OV_DamageTool.SetupRaycast(ray, range, mask, ignorePlayer);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00014348 File Offset: 0x00012548
		public static RaycastInfo SetupRaycast(Ray ray, float range, int mask, Player ignorePlayer = null)
		{
			switch (OV_DamageTool.OVType)
			{
			case OverrideType.Extended:
				return RaycastUtilities.GenerateOriginalRaycast(ray, range, mask, null);
			case OverrideType.PlayerHit:
			{
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (VectorUtilities.GetDistance(Player.player.transform.position, Provider.clients[i].player.transform.position) <= 15.5)
					{
						RaycastInfo raycastInfo;
						RaycastUtilities.GenerateRaycast(out raycastInfo);
						return raycastInfo;
					}
				}
				break;
			}
			case OverrideType.SilentAim:
			{
				RaycastInfo raycastInfo2;
				if (!RaycastUtilities.GenerateRaycast(out raycastInfo2))
				{
					return RaycastUtilities.GenerateOriginalRaycast(ray, range, mask, null);
				}
				return raycastInfo2;
			}
			case OverrideType.SilentAimMelee:
			{
				RaycastInfo raycastInfo3;
				if (!RaycastUtilities.GenerateRaycast(out raycastInfo3))
				{
					return RaycastUtilities.GenerateOriginalRaycast(ray, Mathf.Max(5.5f, range), mask, null);
				}
				return raycastInfo3;
			}
			}
			return RaycastUtilities.GenerateOriginalRaycast(ray, range, mask, ignorePlayer);
		}

		// Token: 0x04000372 RID: 882
		public static OverrideType OVType;
	}
}
