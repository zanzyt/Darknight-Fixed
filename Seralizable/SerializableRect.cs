using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000089 RID: 137
	public class SerializableRect
	{
		// Token: 0x06000276 RID: 630 RVA: 0x00003BFF File Offset: 0x00001DFF
		public static implicit operator Rect(SerializableRect rect)
		{
			return new Rect(rect.x, rect.y, rect.width, rect.height);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00003C1E File Offset: 0x00001E1E
		public static implicit operator SerializableRect(Rect rect)
		{
			return new SerializableRect
			{
				x = rect.x,
				y = rect.y,
				width = rect.width,
				height = rect.height
			};
		}

		// Token: 0x04000365 RID: 869
		public float x;

		// Token: 0x04000366 RID: 870
		public float y;

		// Token: 0x04000367 RID: 871
		public float width;

		// Token: 0x04000368 RID: 872
		public float height;
	}
}
