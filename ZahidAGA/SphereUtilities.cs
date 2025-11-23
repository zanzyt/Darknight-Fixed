using System;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000B5 RID: 181
	public static class SphereUtilities
	{
		// Token: 0x0600031E RID: 798 RVA: 0x000199A8 File Offset: 0x00017BA8
		public static bool GetRaycast(GameObject Target, Vector3 StartPos, out Vector3 Point)
		{
			Point = Vector3.zero;
			bool flag;
			if (Target == null)
			{
				flag = false;
			}
			else
			{
				int layer = Target.layer;
				Target.layer = 24;
				RaycastComponent Component = Target.GetComponent<RaycastComponent>();
				if (VectorUtilities.GetDistance(Target.transform.position, StartPos) <= 15.5)
				{
					Point = OptimizationVariables.MainPlayer.transform.position;
					flag = true;
				}
				else
				{
					IEnumerable<Vector3> vertices = Component.Sphere.GetComponent<MeshCollider>().sharedMesh.vertices;
                        
					Func<Vector3, Vector3> func = v => Component.Sphere.transform.TransformPoint(v);
                    foreach (Vector3 vector in vertices.Select(func).ToArray<Vector3>())
					{
						Vector3 vector2 = VectorUtilities.Normalize(vector - StartPos);
						double distance = VectorUtilities.GetDistance(StartPos, vector);
						if (!Physics.Raycast(StartPos, vector2, (float)distance + 0.5f, RayMasks.DAMAGE_CLIENT)) 
						{
							Target.layer = layer;
							Point = vector;
							return true;
						}
					}
					Target.layer = layer;
					flag = false;
				}
			}
			return flag;
		}
	}
}
