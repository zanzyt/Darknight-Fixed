using System;
using System.Collections.Generic;

namespace ZahidAGA
{
	// Token: 0x020000C0 RID: 192
	public class MenuTabOption
	{
		// Token: 0x06000336 RID: 822 RVA: 0x000043DF File Offset: 0x000025DF
		public MenuTabOption(string name, MenuTabOption.MenuTab tab)
		{
			this.tab = tab;
			this.name = name;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000043F5 File Offset: 0x000025F5
		public static void Add(MenuTabOption tab)
		{
			MenuTabOption.tabs.Add(tab);
		}

		// Token: 0x040003E3 RID: 995
		public static MenuTabOption CurrentTab;

		// Token: 0x040003E4 RID: 996
		public static List<MenuTabOption> tabs = new List<MenuTabOption>();

		// Token: 0x040003E5 RID: 997
		public bool enabled;

		// Token: 0x040003E6 RID: 998
		public string name;

		// Token: 0x040003E7 RID: 999
		public MenuTabOption.MenuTab tab;

		// Token: 0x020000C1 RID: 193
		// (Invoke) Token: 0x0600033A RID: 826
		public delegate void MenuTab();
	}
}
