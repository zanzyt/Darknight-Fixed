using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000079 RID: 121
	public class SerializableVector2
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00003B75 File Offset: 0x00001D75
		public SerializableVector2(float nx, float ny)
		{
			this.x = nx;
			this.y = ny;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00003B8B File Offset: 0x00001D8B
		public Vector2 ToVector2()
		{
			return new Vector2(this.x, this.y);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00003B9E File Offset: 0x00001D9E
		public static implicit operator Vector2(SerializableVector2 vector)
		{
			return vector.ToVector2();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00003BA6 File Offset: 0x00001DA6
		public static implicit operator SerializableVector2(Vector2 vector)
		{
			return new SerializableVector2(vector.x, vector.y);
		}

		// Token: 0x0400023E RID: 574
		public float x;

		// Token: 0x0400023F RID: 575
		public float y;
	}
}
