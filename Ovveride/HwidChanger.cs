using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200004B RID: 75
	[Component]
	public class HwidChanger : MonoBehaviour
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000036F0 File Offset: 0x000018F0
		public void Start()
		{
			HwidChanger.methodhwid = (byte[][])HwidChanger.method.Invoke(null, null);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00011ADC File Offset: 0x0000FCDC
		[Override(typeof(LocalHwid), "GetHwids", BindingFlags.Static | BindingFlags.Public, 0)]
		public static byte[][] OV_GetHwids()
		{
			byte[][] array = (byte[][])HwidChanger.methodhwid;
			if (MiscOptions.HwidChanger) 
			{
				HwidChanger.CreateRandomHwid();
				array.SetValue(HwidChanger.Hwid1, 0);
				array.SetValue(HwidChanger.Hwid2, 1);
				array.SetValue(HwidChanger.Hwid3, 2);
			}
			return array;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00011B28 File Offset: 0x0000FD28
		public static byte[] CreateRandomHwid2() 
		{
			byte[] array = new byte[20];
			for (int i = 0; i < 20; i++)
			{
				array[i] = (byte)global::UnityEngine.Random.Range(0, 256);
			}
			return array;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public static void CreateRandomHwid()
		{
			HwidChanger.Hwid1 = new byte[20];
			for (int i = 0; i < 20; i++)
			{
				HwidChanger.Hwid1[i] = (byte)global::UnityEngine.Random.Range(0, 256);
			}
			HwidChanger.Hwid2 = new byte[20];
			for (int j = 0; j < 20; j++)
			{
				HwidChanger.Hwid2[j] = (byte)global::UnityEngine.Random.Range(0, 256);
			}
			HwidChanger.Hwid3 = new byte[20];
			for (int k = 0; k < 20; k++)
			{
				HwidChanger.Hwid3[k] = (byte)global::UnityEngine.Random.Range(0, 256);
			}
		}

		// Token: 0x04000180 RID: 384
		public static object methodhwid;

		// Token: 0x04000181 RID: 385
		public static byte[] Hwid1;

		// Token: 0x04000182 RID: 386
		public static byte[] Hwid2;

		// Token: 0x04000183 RID: 387
		public static byte[] Hwid3;

		// Token: 0x04000184 RID: 388
		public static MethodBase method = typeof(LocalHwid).GetMethod("InitHwids", BindingFlags.Static | BindingFlags.NonPublic);
	}
}
