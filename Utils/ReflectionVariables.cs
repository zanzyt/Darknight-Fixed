using System;
using System.Reflection;

namespace ZahidAGA
{
	// Token: 0x020000BC RID: 188
	public static class ReflectionVariables
	{
		// Token: 0x040003D1 RID: 977
		public static BindingFlags PublicInstance = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x040003D2 RID: 978
		public static BindingFlags publicInstance = BindingFlags.Instance | BindingFlags.NonPublic;

		// Token: 0x040003D3 RID: 979
		public static BindingFlags PublicStatic = BindingFlags.Static | BindingFlags.Public;

		// Token: 0x040003D4 RID: 980
		public static BindingFlags publicStatic = BindingFlags.Static | BindingFlags.NonPublic;

		// Token: 0x040003D5 RID: 981
		public static BindingFlags Everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
