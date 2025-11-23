using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000099 RID: 153
	public class OV_UseableBarricade
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00015330 File Offset: 0x00013530
		[Override(typeof(UseableBarricade), "checkSpace", BindingFlags.Instance | BindingFlags.Public, 0)]
		public bool OV_checkSpace()
		{
			bool flag;
			if (!MiscOptions.BuildObs || G.BeingSpied)
			{
				flag = (bool)OverrideUtilities.CallOriginal(this, Array.Empty<object>());
			}
			else
			{
				OverrideUtilities.CallOriginal(this, Array.Empty<object>());
				RaycastHit raycastHit;
				if ((Vector3)OV_UseableBarricade.pointField.GetValue(this) != Vector3.zero && !MiscOptions.Freecam)
				{
					if (MiscOptions.epos)
					{
						OV_UseableBarricade.pointField.SetValue(this, (Vector3)OV_UseableBarricade.pointField.GetValue(this) + MiscOptions.pos);
					}
					flag = true;
				}
				else if (Physics.Raycast(new Ray(OptimizationVariables.MainCam.transform.position, OptimizationVariables.MainCam.transform.forward), out raycastHit, 20f, RayMasks.DAMAGE_CLIENT))
				{
					Vector3 vector = raycastHit.point;
					if (MiscOptions.epos)
					{
						vector += MiscOptions.pos;
					}
					OV_UseableBarricade.pointField.SetValue(this, vector);
					flag = true;
				}
				else
				{
					Vector3 vector2 = OptimizationVariables.MainCam.transform.position + OptimizationVariables.MainCam.transform.forward * 7f;
					if (MiscOptions.epos)
					{
						vector2 += MiscOptions.pos;
					}
					OV_UseableBarricade.pointField.SetValue(this, vector2);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0400039A RID: 922
		public static FieldInfo pointField = typeof(UseableBarricade).GetField("point", ReflectionVariables.publicInstance);
	}
}
