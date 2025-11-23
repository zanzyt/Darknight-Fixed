using System;
using System.Collections.Generic;
using System.Reflection;
using DarkNight;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000063 RID: 99
	public class SpyManager
	{
		// Token: 0x06000246 RID: 582 RVA: 0x000137C8 File Offset: 0x000119C8
		public static void InvokePre()
		{
			foreach (MethodInfo methodInfo in SpyManager.PreSpy)
			{
				methodInfo.Invoke(null, null);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00013814 File Offset: 0x00011A14
		public static void InvokePost()
		{
			foreach (MethodInfo methodInfo in SpyManager.PostSpy)
			{
				methodInfo.Invoke(null, null);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00013860 File Offset: 0x00011A60
		public static void DestroyComponents()
		{
			foreach (Type type in SpyManager.Components)
			{
				global::UnityEngine.Object.Destroy(Loader.oko.GetComponent(type));
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000138B8 File Offset: 0x00011AB8
		public static void AddComponents()
		{
			foreach (Type type in SpyManager.Components)
			{
				Loader.oko.AddComponent(type);
			}
		}

		// Token: 0x040001B7 RID: 439
		public static IEnumerable<MethodInfo> PreSpy;

		// Token: 0x040001B8 RID: 440
		public static IEnumerable<Type> Components;

		// Token: 0x040001B9 RID: 441
		public static IEnumerable<MethodInfo> PostSpy;
	}
}
