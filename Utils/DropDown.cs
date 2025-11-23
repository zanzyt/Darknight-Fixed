using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000BF RID: 191
	public class DropDown
	{
		// Token: 0x06000333 RID: 819 RVA: 0x000043B2 File Offset: 0x000025B2
		public DropDown()
		{
			this.IsEnabled = false;
			this.ListIndex = 0;
			this.ScrollView = Vector2.zero;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00019C94 File Offset: 0x00017E94
		public static DropDown Get(string identifier)
		{
			DropDown dropDown;
			DropDown dropDown2;
			if (DropDown.DropDownManager.TryGetValue(identifier, out dropDown))
			{
				dropDown2 = dropDown;
			}
			else
			{
				dropDown = new DropDown();
				DropDown.DropDownManager.Add(identifier, dropDown);
				dropDown2 = dropDown;
			}
			return dropDown2;
		}

		// Token: 0x040003DF RID: 991
		public static Dictionary<string, DropDown> DropDownManager = new Dictionary<string, DropDown>();

		// Token: 0x040003E0 RID: 992
		public bool IsEnabled;

		// Token: 0x040003E1 RID: 993
		public int ListIndex;

		// Token: 0x040003E2 RID: 994
		public Vector2 ScrollView;
	}
}
