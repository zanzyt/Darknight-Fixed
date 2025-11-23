using System;
using System.Collections.Generic;

namespace ZahidAGA
{
	// Token: 0x020000B9 RID: 185
	public class ESPVariables
	{
		// Token: 0x040003C9 RID: 969
		public static List<ESPObject> Objects = new List<ESPObject>();

		// Token: 0x040003CA RID: 970
		public static Queue<ESPBox> DrawBuffer = new Queue<ESPBox>();

		// Token: 0x040003CB RID: 971
		public static Queue<ESPBox2> DrawBuffer2 = new Queue<ESPBox2>();

		// Token: 0x040003CC RID: 972
		public static Queue<TracerLine> TracerLine = new Queue<TracerLine>();
	}
}
