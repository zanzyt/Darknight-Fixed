using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000B8 RID: 184
	public static class AssetVariables
	{
		// Token: 0x040003C2 RID: 962
		public static AssetBundle ABundle;

		// Token: 0x040003C3 RID: 963
		public static Dictionary<string, Material> Materials = new Dictionary<string, Material>();

		// Token: 0x040003C4 RID: 964
		public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

		// Token: 0x040003C5 RID: 965
		public static Dictionary<string, Cursor> Cursor = new Dictionary<string, Cursor>();

		// Token: 0x040003C6 RID: 966
		public static Dictionary<string, AudioClip> Audio = new Dictionary<string, AudioClip>();

		// Token: 0x040003C7 RID: 967
		public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

		// Token: 0x040003C8 RID: 968
		public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();
	}
}
