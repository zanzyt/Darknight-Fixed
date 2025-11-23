using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200009C RID: 156
	public class OV_UseableStructure
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00015C4C File Offset: 0x00013E4C
		[Override(typeof(UseableStructure), "checkSpace", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public bool OV_checkSpace()
		{
			if (!MiscOptions.BuildinObstacles || G.BeingSpied)
			{
				return (bool)OverrideUtilities.CallOriginal(this, new object[0]);
			}
			OverrideUtilities.CallOriginal(this, new object[0]);
			if ((Vector3)OV_UseableStructure.pointField.GetValue(this) != Vector3.zero && !MiscOptions.Freecam)
			{
				if (MiscOptions.epos)
				{
					OV_UseableStructure.pointField.SetValue(this, (Vector3)OV_UseableStructure.pointField.GetValue(this) + MiscOptions.pos);
				}
				return true;
			}
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(OptimizationVariables.MainCam.transform.position, OptimizationVariables.MainCam.transform.forward), out raycastHit, 20f, RayMasks.DAMAGE_CLIENT))
			{
				Vector3 vector = raycastHit.point;
				if (MiscOptions.epos)
				{
					vector += MiscOptions.pos;
				}
				OV_UseableStructure.pointField.SetValue(this, vector);
				return true;
			}
			Vector3 vector2 = OptimizationVariables.MainCam.transform.position + OptimizationVariables.MainCam.transform.forward * 7f;
			if (MiscOptions.epos)
			{
				vector2 += MiscOptions.pos;
			}
			OV_UseableStructure.pointField.SetValue(this, vector2);
			return true;
		}

		// Token: 0x0400039C RID: 924
		public static FieldInfo pointField = typeof(UseableStructure).GetField("point", ReflectionVariables.publicInstance);
	}
}
