using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000A7 RID: 167
	public class MenuUtilities
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000406C File Offset: 0x0000226C
		static MenuUtilities()
		{
			MenuUtilities.TexClear.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			MenuUtilities.TexClear.Apply();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00017A10 File Offset: 0x00015C10
		public static void FixGUIStyleColor(GUIStyle style)
		{
			style.normal.background = MenuUtilities.TexClear;
			style.onNormal.background = MenuUtilities.TexClear;
			style.active.background = MenuUtilities.TexClear;
			style.onActive.background = MenuUtilities.TexClear;
			style.focused.background = MenuUtilities.TexClear;
			style.onFocused.background = MenuUtilities.TexClear;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000040A9 File Offset: 0x000022A9
		public static Rect Inline(Rect rect, float border = 1f)
		{
			return new Rect(rect.x + border, rect.y + border, rect.width - border * 2f, rect.height - border * 2f);
		}

		// Token: 0x040003A3 RID: 931
		public static Texture2D TexClear = new Texture2D(1, 1);
	}
}
