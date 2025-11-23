using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000069 RID: 105
	public class LocalBounds
	{
		// Token: 0x06000250 RID: 592 RVA: 0x000039D8 File Offset: 0x00001BD8
		public LocalBounds(Vector3 po, Vector3 e)
		{
			this.PosOffset = po;
			this.Extents = e;
		}

		// Token: 0x040001E5 RID: 485
		public Vector3 PosOffset;

		// Token: 0x040001E6 RID: 486
		public Vector3 Extents;
	}
}
