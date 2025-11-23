using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200008E RID: 142
	public static class OV_Input
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00003C9A File Offset: 0x00001E9A
		[OnSpy]
		public static void OnSpied()
		{
			OV_Input.InputEnabled = false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00003CA2 File Offset: 0x00001EA2
		[OffSpy]
		public static void OnEndSpy()
		{
			OV_Input.InputEnabled = true;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000146AC File Offset: 0x000128AC
		[Override(typeof(Input), "GetKey", BindingFlags.Static | BindingFlags.Public, 0)]
		public static bool OV_GetKey(KeyCode key)
		{
			bool flag;
			if (!DrawUtilities.ShouldRun() || !OV_Input.InputEnabled)
			{
				flag = (bool)OverrideUtilities.CallOriginal(null, new object[] { key });
			}
			else
			{
				flag = (key == ControlsSettings.primary && TriggerbotOptions.IsFiring) || (((key != ControlsSettings.left && key != ControlsSettings.right && key != ControlsSettings.up && key != ControlsSettings.down) || !(MiscOptions.SpectatedPlayer != null)) && (bool)OverrideUtilities.CallOriginal(null, new object[] { key }));
			}
			return flag;
		}

		// Token: 0x04000379 RID: 889
		public static bool InputEnabled = true;
	}
}
