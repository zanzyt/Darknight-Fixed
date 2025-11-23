using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200003C RID: 60
	public class MenuComponent : MonoBehaviour
	{
		// Token: 0x04000125 RID: 293
		public static Texture2D[] Icons;

		// Token: 0x04000126 RID: 294
		public static Font _TabFont;

		// Token: 0x04000127 RID: 295
		public static Font _TextFont;

		// Token: 0x04000128 RID: 296
		public static Texture2D _Logo;

		// Token: 0x04000129 RID: 297
		public static bool IsInMenu;

		// Token: 0x0400012A RID: 298
		public static KeyCode MenuKey = KeyCode.F1;

		// Token: 0x0400012B RID: 299
		public static Rect MenuRect = new Rect(400f, 200f, 640f, 500f);

		// Token: 0x0400012C RID: 300
		public static Color32 _OutlineBorderBlack;

		// Token: 0x0400012D RID: 301
		public static Color32 _OutlineBorderLightGray;

		// Token: 0x0400012E RID: 302
		public static Color32 _OutlineBorderDarkGray;

		// Token: 0x0400012F RID: 303
		public static Color32 _FillLightBlack;

		// Token: 0x04000130 RID: 304
		public static Color32 _Accent1;

		// Token: 0x04000131 RID: 305
		public static Color32 _Accent2;

		// Token: 0x04000132 RID: 306
		public static Rect _cursor = new Rect(0f, 0f, 23f, 31f);

		// Token: 0x04000133 RID: 307
		private Texture _cursorTexture;

		// Token: 0x04000134 RID: 308
		public static int _pIndex = 0;
	}
}
