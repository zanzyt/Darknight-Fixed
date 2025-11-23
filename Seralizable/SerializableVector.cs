using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000078 RID: 120
	public class SerializableVector
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00003B37 File Offset: 0x00001D37
		public SerializableVector(float nx, float ny, float nz)
		{
			this.x = nx;
			this.y = ny;
			this.z = nz;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00003B54 File Offset: 0x00001D54
		public Vector3 ToVector()
		{
			return new Vector3(this.x, this.y, this.z);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00003B6D File Offset: 0x00001D6D
		public static implicit operator Vector3(SerializableVector vector)
		{
			return vector.ToVector();
		}

		// Token: 0x0400023B RID: 571
		public float x;

		// Token: 0x0400023C RID: 572
		public float y;

		// Token: 0x0400023D RID: 573
		public float z;
	}
}
