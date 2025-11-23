using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200003B RID: 59
	public class Drawing
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0000337B File Offset: 0x0000157B
		public static void DrawRect(Rect position, Color color, GUIContent content = null)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUI.Box(position, content ?? GUIContent.none, Drawing.textureStyle);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000033A2 File Offset: 0x000015A2
		public static void LayoutBox(Color color, GUIContent content = null)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUILayout.Box(content ?? GUIContent.none, Drawing.textureStyle, new GUILayoutOption[0]);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x04000123 RID: 291
		public static Texture2D backgroundTexture = Texture2D.whiteTexture;

		// Token: 0x04000124 RID: 292
		public static GUIStyle textureStyle = new GUIStyle
		{
			normal = new GUIStyleState
			{
				background = Drawing.backgroundTexture
			}
		};
	}
}
