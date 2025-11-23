using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000A6 RID: 166
	public static class MathUtilities
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00004054 File Offset: 0x00002254
		[Initializer]
		public static void GenerateRandom()
		{
			MathUtilities.Random = new global::System.Random();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00017768 File Offset: 0x00015968
		public static bool Intersect(Vector3 p0, Vector3 p1, Vector3 oVector, Vector3 bCenter, out Vector3 intersection)
		{
			intersection = Vector3.zero;
			Vector3 vector = p1 - p0;
			float num = Vector3.Dot(vector, oVector);
			bool flag;
			if (num == 0f)
			{
				flag = false;
			}
			else
			{
				float num2 = -(Vector3.Dot(p0 - bCenter, oVector) / num);
				intersection = p0 + num2 * vector;
				flag = true;
			}
			return flag;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000177C8 File Offset: 0x000159C8
		public static Vector3 GetOrthogonalVector(Vector3 vCenter, Vector3 vPoint)
		{
			Vector3 vector = vCenter - vPoint;
			double distance = VectorUtilities.GetDistance(vCenter, vPoint);
			return vector / (float)distance;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000177EC File Offset: 0x000159EC
		public static Vector3[] GetRectanglePoints(Vector3 playerPos, Vector3[] bCorners, Bounds bound)
		{
			Vector3 orthogonalVector = MathUtilities.GetOrthogonalVector(bound.center, playerPos);
			List<Vector3> list = new List<Vector3>();
			Vector3[] array = new Vector3[]
			{
				bCorners[0],
				bCorners[1],
				bCorners[1],
				bCorners[3],
				bCorners[3],
				bCorners[2],
				bCorners[2],
				bCorners[0],
				bCorners[4],
				bCorners[5],
				bCorners[5],
				bCorners[7],
				bCorners[7],
				bCorners[6],
				bCorners[6],
				bCorners[4],
				bCorners[0],
				bCorners[4],
				bCorners[1],
				bCorners[5],
				bCorners[2],
				bCorners[6],
				bCorners[3],
				bCorners[7]
			};
			for (int i = 0; i < 24; i += 2)
			{
				Vector3 vector = array[i];
				Vector3 vector2 = array[i + 1];
				Vector3 vector3;
				if (MathUtilities.Intersect(vector, vector2, orthogonalVector, bound.center, out vector3))
				{
					list.Add(vector3);
				}
			}
			Bounds bounds = new Bounds(bound.center, bound.size * 1.2f);
			for (int j = list.Count - 1; j > -1; j--)
			{
				if (!bounds.Contains(list[j]))
				{
					list.RemoveAt(j);
				}
			}
			return list.ToArray();
		}

		// Token: 0x040003A1 RID: 929
		private static readonly WebClient web = new WebClient();

		// Token: 0x040003A2 RID: 930
		public static global::System.Random Random;
	}
}
