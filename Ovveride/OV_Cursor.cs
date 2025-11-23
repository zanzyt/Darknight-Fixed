using System;
using System.Reflection;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200008B RID: 139
	public static class OV_Cursor
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00003C59 File Offset: 0x00001E59
		[Override(typeof(Cursor), "set_lockState", BindingFlags.Static | BindingFlags.Public, 0)]
		public static void OV_set_lockState(CursorLockMode rMode)
		{
			if (!MenuComponent.IsInMenu || G.BeingSpied || (rMode != CursorLockMode.Confined && rMode != CursorLockMode.Locked))
			{
				OverrideUtilities.CallOriginal(null, new object[] { rMode });
			}
		}
	}
}
