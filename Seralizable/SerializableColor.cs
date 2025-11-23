using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000077 RID: 119
	public class SerializableColor
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00003AAA File Offset: 0x00001CAA
		public SerializableColor(int nr, int ng, int nb, int na)
		{
			this.r = nr;
			this.g = ng;
			this.b = nb;
			this.a = na;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00003ACF File Offset: 0x00001CCF
		public SerializableColor(int nr, int ng, int nb)
		{
			this.r = nr;
			this.g = ng;
			this.b = nb;
			this.a = 255;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00003AF7 File Offset: 0x00001CF7
		public static implicit operator Color32(SerializableColor color)
		{
			return color.ToColor();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00003AFF File Offset: 0x00001CFF
		public static implicit operator Color(SerializableColor color)
		{
			return color.ToColor();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00003B0C File Offset: 0x00001D0C
		public static implicit operator SerializableColor(Color32 color)
		{
			return color.ToSerializableColor();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00003B14 File Offset: 0x00001D14
		public Color32 ToColor()
		{
			return new Color32((byte)this.r, (byte)this.g, (byte)this.b, (byte)this.a);
		}

		// Token: 0x04000237 RID: 567
		public int r;

		// Token: 0x04000238 RID: 568
		public int g;

		// Token: 0x04000239 RID: 569
		public int b;

		// Token: 0x0400023A RID: 570
		public int a;
	}
}
