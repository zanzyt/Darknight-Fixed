using System;
using System.Collections.Generic;

namespace ZahidAGA
{
	// Token: 0x0200006C RID: 108
	public class SkinOptionList
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00003A17 File Offset: 0x00001C17
		public SkinOptionList(SkinType Type)
		{
			this.Type = Type;
		}

		// Token: 0x040001F1 RID: 497
		public SkinType Type;

		// Token: 0x040001F2 RID: 498
		public HashSet<Skin> Skins = new HashSet<Skin>();
	}
}
