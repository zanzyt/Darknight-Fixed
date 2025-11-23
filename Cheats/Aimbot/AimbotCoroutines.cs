using System;
using System.Collections;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000043 RID: 67
	public static class AimbotCoroutines
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00003531 File Offset: 0x00001731
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00003542 File Offset: 0x00001742
		public static float Pitch
		{
			get
			{
				return OptimizationVariables.MainPlayer.look.pitch;
			}
			set
			{
				AimbotCoroutines.PitchInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000355E File Offset: 0x0000175E
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000356F File Offset: 0x0000176F
		public static float Yaw
		{
			get
			{
				return OptimizationVariables.MainPlayer.look.yaw;
			}
			set
			{
				AimbotCoroutines.YawInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000358B File Offset: 0x0000178B
		[Initializer]
		public static void Init()
		{
			AimbotCoroutines.PitchInfo = typeof(PlayerLook).GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic);
			AimbotCoroutines.YawInfo = typeof(PlayerLook).GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000035C3 File Offset: 0x000017C3
		public static IEnumerator SetLockedObject()
		{
			for (;;)
			{
				if (!DrawUtilities.ShouldRun() || !AimbotOptions.Enabled)
				{
					yield return new WaitForSeconds(0.1f);
				}
				else
				{
					Player player = null;
					Vector3 vector = OptimizationVariables.MainPlayer.look.aim.position;
					Vector3 vector2 = OptimizationVariables.MainPlayer.look.aim.forward;
					SteamPlayer[] array = Provider.clients.ToArray();
					int num;
					for (int i = 0; i < array.Length; i = num + 1)
					{
						TargetMode targetMode = AimbotOptions.TargetMode;
						SteamPlayer steamPlayer = array[i];
						if (steamPlayer != null && !(steamPlayer.player == OptimizationVariables.MainPlayer) && !(steamPlayer.player.life == null) && !steamPlayer.player.life.isDead && !FriendUtilities.IsFriendly(steamPlayer.player))
						{
							if (targetMode == TargetMode.FOV && VectorUtilities.GetAngleDelta(vector, vector2, array[i].player.transform.position) < (double)AimbotOptions.SelectedFOV)
							{
								if (player == null)
								{
									player = array[i].player;
								}
								else if (VectorUtilities.GetAngleDelta(vector, vector2, array[i].player.transform.position) < VectorUtilities.GetAngleDelta(vector, vector2, player.transform.position))
								{
									player = array[i].player;
								}
							}
						}
						num = i;
					}
					if (!AimbotCoroutines.IsAiming)
					{
						AimbotCoroutines.LockedObject = ((player != null) ? player.gameObject : null);
					}
					yield return new WaitForEndOfFrame();
					vector = default(Vector3);
					vector2 = default(Vector3);
				}
			}
			yield break;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000035CB File Offset: 0x000017CB
		public static IEnumerator AimToObject()
		{
			for (;;)
			{
				if (!DrawUtilities.ShouldRun() || !AimbotOptions.Enabled)
				{
					yield return new WaitForSeconds(0.1f);
				}
				else
				{
					if (AimbotCoroutines.LockedObject != null && AimbotCoroutines.LockedObject.transform != null && ESPComponent.MainCamera != null)
					{
						if (HotkeyUtilities.IsHotkeyHeld("_AimbotKey") || !AimbotOptions.OnKey)
						{
							AimbotCoroutines.IsAiming = true;
							if (AimbotOptions.Smooth)
							{
								AimbotCoroutines.SmoothAim(AimbotCoroutines.LockedObject);
							}
							else
							{
								AimbotCoroutines.Aim(AimbotCoroutines.LockedObject);
							}
						}
						else
						{
							AimbotCoroutines.IsAiming = false;
						}
					}
					else
					{
						AimbotCoroutines.IsAiming = false;
					}
					yield return new WaitForEndOfFrame();
				}
			}
			yield break;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00010960 File Offset: 0x0000EB60
		public static void Aim(GameObject obj)
		{
			Camera mainCam = OptimizationVariables.MainCam;
			Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
			if (!(aimPosition == AimbotCoroutines.PiVector))
			{
				OptimizationVariables.MainPlayer.transform.LookAt(aimPosition);
				OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
				mainCam.transform.rotation = Quaternion.LookRotation(aimPosition - OptimizationVariables.MainPlayer.look.aim.position); //3rd person? 
				float num = mainCam.transform.localRotation.eulerAngles.x;
				if (num <= 90f && num <= 270f)
				{
					num = mainCam.transform.localRotation.eulerAngles.x + 90f;
				}
				else if (num >= 270f && num <= 360f)
				{
					num = mainCam.transform.localRotation.eulerAngles.x - 270f;
				}
				AimbotCoroutines.Pitch = num;
				AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public static void SmoothAim(GameObject obj)
		{
			Camera mainCam = OptimizationVariables.MainCam;
			Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
			if (!(aimPosition == AimbotCoroutines.PiVector))
			{
				OptimizationVariables.MainPlayer.transform.rotation = Quaternion.Slerp(OptimizationVariables.MainPlayer.transform.rotation, Quaternion.LookRotation(aimPosition - OptimizationVariables.MainPlayer.transform.position), Time.deltaTime * AimbotOptions.AimSpeed);
				OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
				mainCam.transform.rotation = Quaternion.Slerp(mainCam.transform.rotation, Quaternion.LookRotation(aimPosition - OptimizationVariables.MainPlayer.look.aim.position), Time.deltaTime * AimbotOptions.AimSpeed);
				float num = mainCam.transform.localRotation.eulerAngles.x;
				if (num <= 90f && num <= 270f)
				{
					num = mainCam.transform.localRotation.eulerAngles.x + 90f;
				}
				else if (num >= 270f && num <= 360f)
				{
					num = mainCam.transform.localRotation.eulerAngles.x - 270f;
				}
				AimbotCoroutines.Pitch = num;
				AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000035D3 File Offset: 0x000017D3
		public static Vector2 CalcAngle(GameObject obj)
		{
			ESPComponent.MainCamera.WorldToScreenPoint(AimbotCoroutines.GetAimPosition(obj.transform, "Skull"));
			return Vector2.zero;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000035F5 File Offset: 0x000017F5
		public static void AimMouseTo(float x, float y)
		{
			AimbotCoroutines.Yaw = x;
			AimbotCoroutines.Pitch = y;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00010C40 File Offset: 0x0000EE40
		public static Vector3 GetAimPosition(Transform parent, string name)
		{
			Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
			Vector3 vector;
			if (componentsInChildren == null)
			{
				vector = AimbotCoroutines.PiVector;
			}
			else
			{
				foreach (Transform transform in componentsInChildren)
				{
					if (transform.name.Trim() == name)
					{
						return transform.position + new Vector3(0f, 0.4f, 0f);
					}
				}
				vector = AimbotCoroutines.PiVector;
			}
			return vector;
		}

		// Token: 0x0400016F RID: 367
		public static Vector3 PiVector = new Vector3(0f, 3.1415927f, 0f);

		// Token: 0x04000170 RID: 368
		public static GameObject LockedObject;

		// Token: 0x04000171 RID: 369
		public static bool IsAiming = false;

		// Token: 0x04000172 RID: 370
		public static FieldInfo PitchInfo;

		// Token: 0x04000173 RID: 371
		public static FieldInfo YawInfo;
	}
}
