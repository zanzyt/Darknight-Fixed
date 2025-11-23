using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000BE RID: 190
	public class ColorVariable
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00004326 File Offset: 0x00002526
		[JsonConstructor]
		public ColorVariable(string identity, string name, Color32 color, Color32 origColor, bool disableAlpha)
		{
			this.identity = identity;
			this.name = name;
			this.color = color;
			this.origColor = origColor;
			this.disableAlpha = disableAlpha;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000435D File Offset: 0x0000255D
		public ColorVariable(string identity, string name, Color32 color, bool disableAlpha = true)
		{
			this.identity = identity;
			this.name = name;
			this.color = color;
			this.origColor = color;
			this.disableAlpha = disableAlpha;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00019C44 File Offset: 0x00017E44
		public ColorVariable(ColorVariable option)
		{
			this.identity = option.identity;
			this.name = option.name;
			this.color = option.color;
			this.origColor = option.origColor;
			this.disableAlpha = option.disableAlpha;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00004393 File Offset: 0x00002593
		public static implicit operator Color(ColorVariable color)
		{
			return color.color.ToColor();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000043A5 File Offset: 0x000025A5
		public static implicit operator Color32(ColorVariable color)
		{
			return color.color;
		}

		// Token: 0x040003DA RID: 986
		public SerializableColor color;

		// Token: 0x040003DB RID: 987
		public SerializableColor origColor;

		// Token: 0x040003DC RID: 988
		public string name;

		// Token: 0x040003DD RID: 989
		public string identity;

		// Token: 0x040003DE RID: 990
		public bool disableAlpha;
	}
}
