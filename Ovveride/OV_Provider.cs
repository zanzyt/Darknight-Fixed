using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x02000097 RID: 151
	public static class OV_Provider
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00003EBB File Offset: 0x000020BB
		[Override(typeof(Provider), "legacyReceiveClient", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_legacyReceiveClient(byte[] packet, int offset, int size)
		{
			if (!OV_Provider.IsConnected)
			{
				OV_Provider.IsConnected = true;
			}
		}

		// Token: 0x04000399 RID: 921
		public static bool IsConnected;
	}
}
