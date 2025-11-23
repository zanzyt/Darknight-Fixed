using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000084 RID: 132
	public static class ColorOptions
	{
		// Token: 0x04000316 RID: 790
		[Save]
		public static Dictionary<string, ColorVariable> ColorDict = new Dictionary<string, ColorVariable>();

		// Token: 0x04000317 RID: 791
		public static Dictionary<string, ColorVariable> DefaultColorDict = new Dictionary<string, ColorVariable>();

		// Token: 0x04000318 RID: 792
		public static ColorVariable errorColor = new ColorVariable("errorColor", "#ERROR.NOTFOUND", Color.magenta, true);

		// Token: 0x04000319 RID: 793
		public static ColorVariable preview = new ColorVariable("preview", "Renk Seçilmedi", Color.white, true);

		// Token: 0x0400031A RID: 794
		public static ColorVariable previewselected;

		// Token: 0x0400031B RID: 795
		public static string selectedOption;
	}
}
