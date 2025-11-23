using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000076 RID: 118
	public static class Extensions
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00003A47 File Offset: 0x00001C47
		public static Color Invert(this Color32 color)
		{
			return new Color((float)(byte.MaxValue - color.r), (float)(byte.MaxValue - color.g), (float)(byte.MaxValue - color.b));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000139A8 File Offset: 0x00011BA8
		public static T Next<T>(this T src) where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));
			}
			T[] array = (T[])Enum.GetValues(src.GetType());
			int num = Array.IndexOf<T>(array, src) + 1;
			if (array.Length != num)
			{
				return array[num];
			}
			return array[0];
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00003A75 File Offset: 0x00001C75
		public static SerializableColor ToSerializableColor(this Color32 c)
		{
			return new SerializableColor((int)c.r, (int)c.g, (int)c.b, (int)c.a);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00003A94 File Offset: 0x00001C94
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
		{
			return source.Skip(Math.Max(0, source.Count<T>() - N));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00013A1C File Offset: 0x00011C1C
		public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> target)
		{
			foreach (T t in target)
			{
				source.Add(t);
			}
		}
	}
}
