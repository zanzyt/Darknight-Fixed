using System;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200003F RID: 63
	[Component]
	[SpyComponent]
	public class TargetComponent : MonoBehaviour
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public void OnGUI()
		{
			GUI.skin = AssetUtilities.Skin;
			if (DrawUtilities.ShouldRun() && !G.BeingSpied)
			{
				ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
				float num = ((itemGunAsset != null) ? itemGunAsset.range : 15.5f);
				GameObject gameObject;
				Vector3 vector;
				RaycastUtilities.GetTargetObject(RaycastUtilities.Objects, out gameObject, out vector, num);
				if (RaycastOptions.Enabled && gameObject != null)
				{
					this.TargetInfoWin = GUILayout.Window(313316, this.TargetInfoWin, new GUI.WindowFunction(this.TargetInfoWindow), "", "SelectedButtonDropdown", Array.Empty<GUILayoutOption>());
					T.DrawSnapline(Color.cyan);
				}
				if (WeaponOptions.ShowWeaponInfo)
				{
					this.WeapontInfoWin = GUILayout.Window(326274, this.WeapontInfoWin, new GUI.WindowFunction(this.WeaponInfoWindow), "", "SelectedButtonDropdown", Array.Empty<GUILayoutOption>());
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000FD78 File Offset: 0x0000DF78
		private void TargetInfoWindow(int winid)
		{
			ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
			float num = ((itemGunAsset != null) ? itemGunAsset.range : 15.5f);
			GameObject gameObject;
			Vector3 vector;
			RaycastUtilities.GetTargetObject(RaycastUtilities.Objects, out gameObject, out vector, num);
			GUILayout.Label("", Array.Empty<GUILayoutOption>());
			GUILayout.Label(string.Format("       Target: [{0}] {1}", VectorUtilities.GetDistance2(gameObject.transform.position), gameObject.name), Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000FDFC File Offset: 0x0000DFFC
		private void WeaponInfoWindow(int winid)
		{
			GUILayout.Label("", Array.Empty<GUILayoutOption>());
			GUILayout.Label("<size=17>       Weapon Range: </size><size=17>" + T.GetGunDistance().ToString() + "</size>", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x0400014F RID: 335
		public Rect WeapontInfoWin = new Rect(0f, (float)(Screen.height / 2 - 90), 220f, 0f);

		// Token: 0x04000150 RID: 336
		public Rect TargetInfoWin = new Rect(0f, (float)(Screen.height / 2 - 20), 220f, 0f);

		// Token: 0x04000151 RID: 337
		public static Camera MainCamera;
	}
}
