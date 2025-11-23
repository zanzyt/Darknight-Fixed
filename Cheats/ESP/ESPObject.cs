using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000066 RID: 102
	public class ESPObject
	{
		// Token: 0x0600024D RID: 589 RVA: 0x000039BB File Offset: 0x00001BBB
		public ESPObject(ESPTarget t, object o, GameObject go)
		{
			this.Target = t;
			this.Object = o;
			this.GObject = go;
		}

		// Token: 0x040001BE RID: 446
		public ESPTarget Target;

		// Token: 0x040001BF RID: 447
		public object Object;

		// Token: 0x040001C0 RID: 448
		public GameObject GObject;
	}
}
