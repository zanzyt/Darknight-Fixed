using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000AA RID: 170
	public static class NearestPointTest
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x00018108 File Offset: 0x00016308
		public static Vector3 NearestPointOnMesh(Vector3 pt, Vector3[] verts, int[] tri, VertTriList vt)
		{
			int num = -1;
			float num2 = 100000000f;
			for (int i = 0; i < verts.Length; i++)
			{
				float sqrMagnitude = (verts[i] - pt).sqrMagnitude;
				if (sqrMagnitude < num2)
				{
					num = i;
					num2 = sqrMagnitude;
				}
			}
			Vector3 vector;
			if (num == -1)
			{
				vector = Vector3.zero;
			}
			else
			{
				int[] array = vt[num];
				Vector3 vector2 = Vector3.zero;
				num2 = 100000000f;
				for (int j = 0; j < array.Length; j++)
				{
					int num3 = array[j] * 3;
					Vector3 vector3 = verts[tri[num3]];
					Vector3 vector4 = verts[tri[num3 + 1]];
					Vector3 vector5 = verts[tri[num3 + 2]];
					Vector3 vector6 = NearestPointTest.NearestPointOnTri(pt, vector3, vector4, vector5);
					float sqrMagnitude2 = (pt - vector6).sqrMagnitude;
					if (sqrMagnitude2 < num2)
					{
						vector2 = vector6;
						num2 = sqrMagnitude2;
					}
				}
				vector = vector2;
			}
			return vector;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000181F4 File Offset: 0x000163F4
		public static Vector3 NearestPointOnTri(Vector3 pt, Vector3 a, Vector3 b, Vector3 c)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			Vector3 vector3 = c - b;
			float magnitude = vector.magnitude;
			float magnitude2 = vector2.magnitude;
			float magnitude3 = vector3.magnitude;
			Vector3 vector4 = pt - a;
			Vector3 vector5 = pt - b;
			Vector3 vector6 = pt - c;
			Vector3 vector7 = vector / magnitude;
			Vector3 normalized = Vector3.Cross(vector, vector2).normalized;
			Vector3 vector8 = Vector3.Cross(normalized, vector7);
			Vector3 vector9 = Vector3.Cross(vector, vector4);
			Vector3 vector10 = Vector3.Cross(vector2, -vector6);
			Vector3 vector11 = Vector3.Cross(vector3, vector5);
			bool flag = Vector3.Dot(vector9, normalized) > 0f;
			bool flag2 = Vector3.Dot(vector10, normalized) > 0f;
			bool flag3 = Vector3.Dot(vector11, normalized) > 0f;
			Vector3 vector12;
			if (flag && flag2 && flag3)
			{
				float num = Vector3.Dot(vector4, vector7);
				float num2 = Vector3.Dot(vector4, vector8);
				vector12 = a + vector7 * num + vector8 * num2;
			}
			else
			{
				Vector3 vector13 = vector7;
				Vector3 normalized2 = vector2.normalized;
				Vector3 normalized3 = vector3.normalized;
				float num3 = Mathf.Clamp(Vector3.Dot(vector13, vector4), 0f, magnitude);
				float num4 = Mathf.Clamp(Vector3.Dot(normalized2, vector4), 0f, magnitude2);
				float num5 = Mathf.Clamp(Vector3.Dot(normalized3, vector5), 0f, magnitude3);
				Vector3 vector14 = a + num3 * vector13;
				Vector3 vector15 = a + num4 * normalized2;
				Vector3 vector16 = b + num5 * normalized3;
				float sqrMagnitude = (pt - vector14).sqrMagnitude;
				float sqrMagnitude2 = (pt - vector15).sqrMagnitude;
				float sqrMagnitude3 = (pt - vector16).sqrMagnitude;
				vector12 = ((sqrMagnitude < sqrMagnitude2) ? ((sqrMagnitude < sqrMagnitude3) ? vector14 : vector16) : ((sqrMagnitude2 < sqrMagnitude3) ? vector15 : vector16));
			}
			return vector12;
		}

		// Token: 0x040003A7 RID: 935
		public static Vector3 a;

		// Token: 0x040003A8 RID: 936
		public static Vector3 b;

		// Token: 0x040003A9 RID: 937
		public static Vector3 c;

		// Token: 0x040003AA RID: 938
		public static Vector3 pt;
	}
}
