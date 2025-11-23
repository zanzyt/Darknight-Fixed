using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200002B RID: 43
	[Component]
	public class MirrorCameraComponent : MonoBehaviour
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002420 File Offset: 0x00000620
		[OnSpy]
		public static void Disable()
		{
			MirrorCameraComponent.WasEnabled = MirrorCameraOptions.Enabled;
			MirrorCameraOptions.Enabled = false;
			global::UnityEngine.Object.Destroy(MirrorCameraComponent.cam_obj);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000243C File Offset: 0x0000063C
		[OffSpy]
		public static void Enable()
		{
			MirrorCameraOptions.Enabled = MirrorCameraComponent.WasEnabled;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002448 File Offset: 0x00000648
		public void Update()
		{
			if (!MirrorCameraComponent.cam_obj || !MirrorCameraComponent.subCam)
			{
				return;
			}
			if (MirrorCameraOptions.Enabled)
			{
				MirrorCameraComponent.subCam.enabled = true;
				return;
			}
			MirrorCameraComponent.subCam.enabled = false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002481 File Offset: 0x00000681
		private void Start()
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000AB18 File Offset: 0x00008D18
		private void OnGUI()
		{
			if (MirrorCameraOptions.Enabled)
			{
				GUI.color = new Color(1f, 1f, 1f, 0f);
				MirrorCameraComponent.viewport = GUILayout.Window(99, MirrorCameraComponent.viewport, new GUI.WindowFunction(this.DoMenu), "Mirror Camera", Array.Empty<GUILayoutOption>());
				GUI.color = Color.white;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000AB7C File Offset: 0x00008D7C
		private void DoMenu(int windowID)
		{
			if (MirrorCameraComponent.cam_obj == null || MirrorCameraComponent.subCam == null)
			{
				MirrorCameraComponent.cam_obj = new GameObject();
				if (MirrorCameraComponent.subCam != null)
				{
					global::UnityEngine.Object.Destroy(MirrorCameraComponent.subCam);
				}
				MirrorCameraComponent.subCam = MirrorCameraComponent.cam_obj.AddComponent<Camera>();
				MirrorCameraComponent.subCam.CopyFrom(OptimizationVariables.MainCam);
				MirrorCameraComponent.cam_obj.transform.position = OptimizationVariables.MainCam.gameObject.transform.position;
				MirrorCameraComponent.cam_obj.transform.rotation = OptimizationVariables.MainCam.gameObject.transform.rotation;
				MirrorCameraComponent.cam_obj.transform.Rotate(0f, 0f, 0f);
				MirrorCameraComponent.subCam.transform.SetParent(OptimizationVariables.MainCam.transform, false);
				MirrorCameraComponent.subCam.enabled = true;
				MirrorCameraComponent.subCam.rect = new Rect(0.6f, 0.6f, 0.4f, 0.4f);
				MirrorCameraComponent.subCam.depth = 99f;
				global::UnityEngine.Object.DontDestroyOnLoad(MirrorCameraComponent.cam_obj);
			}
			float num = MirrorCameraComponent.viewport.x / (float)Screen.width;
			float num2 = (MirrorCameraComponent.viewport.y + 25f) / (float)Screen.height;
			float num3 = MirrorCameraComponent.viewport.width / (float)Screen.width;
			float num4 = MirrorCameraComponent.viewport.height / (float)Screen.height;
			num2 = 1f - num2;
			num2 -= num4;
			MirrorCameraComponent.subCam.rect = new Rect(num, num2, num3, num4);
			Drawing.DrawRect(new Rect(0f, 0f, MirrorCameraComponent.viewport.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 20f, MirrorCameraComponent.viewport.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
			GUILayout.Space(-19f);
			GUILayout.Label("Mirror Camera", Array.Empty<GUILayoutOption>());
			GUI.DragWindow();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public static void FixCam()
		{
			if (MirrorCameraComponent.cam_obj != null && MirrorCameraComponent.subCam != null)
			{
				MirrorCameraComponent.cam_obj.transform.position = OptimizationVariables.MainCam.gameObject.transform.position;
				MirrorCameraComponent.cam_obj.transform.rotation = OptimizationVariables.MainCam.gameObject.transform.rotation;
				MirrorCameraComponent.cam_obj.transform.Rotate(0f, 180f, 0f);
				MirrorCameraComponent.subCam.transform.SetParent(OptimizationVariables.MainCam.transform, true);
				MirrorCameraComponent.subCam.depth = 99f;
				MirrorCameraComponent.subCam.enabled = true;
			}
		}

		// Token: 0x040000D4 RID: 212
		public static Rect viewport = new Rect(1075f, 10f, (float)(Screen.width / 4), (float)(Screen.height / 4));

		// Token: 0x040000D5 RID: 213
		public static GameObject cam_obj;

		// Token: 0x040000D6 RID: 214
		public static Camera subCam;

		// Token: 0x040000D7 RID: 215
		public static bool WasEnabled;
	}
}
