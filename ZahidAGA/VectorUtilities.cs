using System;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000B7 RID: 183
	public static class VectorUtilities
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00004242 File Offset: 0x00002442
		public static double GetDistance(Vector3 point)
		{
			return VectorUtilities.GetDistance(OptimizationVariables.MainCam.transform.position, point);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000222D File Offset: 0x0000042D
		public static float GetDistance2(Vector3 endpos)
		{
			return (float)Math.Round((double)Vector3.Distance(Player.player.look.aim.position, endpos));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00019ADC File Offset: 0x00017CDC
		public static double GetDistance(Vector3 start, Vector3 point)
		{
			Vector3 vector;
			vector.x = start.x - point.x;
			vector.y = start.y - point.y;
			vector.z = start.z - point.z;
			return Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00019B54 File Offset: 0x00017D54
		public static float FOVRadius(float fov)
		{
			float num = OptimizationVariables.MainCam.fieldOfView;
			if (GraphicsSettings.scopeQuality != EGraphicQuality.OFF)
			{
				UseableGun useableGun = Player.player.equipment.useable as UseableGun;
				if (useableGun && useableGun.isAiming && Player.player.look.scopeCamera.enabled)
				{
					num = Player.player.look.scopeCamera.fieldOfView;
				}
			}
			return (float)(Math.Tan((double)fov * 0.017453292519943295 / 2.0) / Math.Tan((double)num * 0.017453292519943295 / 2.0) * (double)Screen.height);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00004259 File Offset: 0x00002459
		public static double GetMagnitude(Vector3 vector)
		{
			return Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000428A File Offset: 0x0000248A
		public static Vector3 Normalize(Vector3 vector)
		{
			return vector / (float)VectorUtilities.GetMagnitude(vector);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00019C04 File Offset: 0x00017E04
		public static double GetAngleDelta(Vector3 mainPos, Vector3 forward, Vector3 target)
		{
			Vector3 vector = VectorUtilities.Normalize(target - mainPos);
			return Math.Atan2(VectorUtilities.GetMagnitude(Vector3.Cross(vector, forward)), (double)Vector3.Dot(vector, forward)) * 57.29577951308232;
		}
	}
}
