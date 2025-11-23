using System;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200002F RID: 47
	[Component]
	public class RadarComponent : MonoBehaviour
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000268D File Offset: 0x0000088D
		[OnSpy]
		public static void Disable()
		{
			RadarComponent.WasEnabled = RadarOptions.Enabled;
			RadarOptions.Enabled = false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000269F File Offset: 0x0000089F
		[OffSpy]
		public static void Enable()
		{
			RadarOptions.Enabled = RadarComponent.WasEnabled;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		private void OnGUI()
		{
			if (RadarOptions.Enabled && DrawUtilities.ShouldRun())
			{
				RadarOptions.vew.width = (RadarOptions.vew.height = RadarOptions.RadarSize + 10f);
				GUI.color = new Color(1f, 1f, 1f, 0f);
				RadarComponent.veww = GUILayout.Window(345, RadarOptions.vew, new GUI.WindowFunction(this.RadarMenu), "Radar", Array.Empty<GUILayoutOption>());
				RadarOptions.vew.x = RadarComponent.veww.x;
				RadarOptions.vew.y = RadarComponent.veww.y;
				GUI.color = Color.white;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000BF7C File Offset: 0x0000A17C
		private void RadarMenu(int windowID)
		{
			Drawing.DrawRect(new Rect(0f, 0f, RadarOptions.vew.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 20f, RadarOptions.vew.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
			Drawing.DrawRect(new Rect(0f, 25f, RadarOptions.vew.width, RadarOptions.vew.height + 25f), new Color32(64, 64, 64, byte.MaxValue), null);
			GUILayout.Space(-19f);
			GUILayout.Label("Radar", Array.Empty<GUILayoutOption>());
			Vector2 vector = new Vector2(RadarOptions.vew.width / 2f, (RadarOptions.vew.height + 25f) / 2f);
			RadarComponent.radarcenter = new Vector2(RadarOptions.vew.width / 2f, (RadarOptions.vew.height + 25f) / 2f);
			Vector2 vector2 = this.GameToRadarPosition(OptimizationVariables.MainPlayer.transform.position);
			if (RadarOptions.type == 2 || RadarOptions.type == 3)
			{
				RadarComponent.radarcenter.x = RadarComponent.radarcenter.x - vector2.x;
				RadarComponent.radarcenter.y = RadarComponent.radarcenter.y + vector2.y;
			}
			Drawing.DrawRect(new Rect(vector.x, 25f, 1f, RadarOptions.vew.height), Color.gray, null);
			Drawing.DrawRect(new Rect(0f, vector.y, RadarOptions.vew.width, 1f), Color.gray, null);
			Vector2 vector3 = new Vector2(vector.x, vector.y - 10f);
			Vector2 vector4 = new Vector2(vector.x + 5f, vector.y + 5f);
			Vector2 vector5 = new Vector2(vector.x - 5f, vector.y + 5f);
			if (RadarOptions.type == 2)
			{
				vector3 = RadarComponent.RotatePoint(vector3, vector, Math.Round((double)MainCamera.instance.transform.eulerAngles.y, 2));
				vector4 = RadarComponent.RotatePoint(vector4, vector, Math.Round((double)MainCamera.instance.transform.eulerAngles.y, 2));
				vector5 = RadarComponent.RotatePoint(vector5, vector, Math.Round((double)MainCamera.instance.transform.eulerAngles.y, 2));
				RadarComponent.DrawLine(vector3, vector4, Color.white, 1f);
				RadarComponent.DrawLine(vector4, vector5, Color.white, 1f);
				RadarComponent.DrawLine(vector5, vector3, Color.white, 1f);
			}
			if (RadarOptions.type == 3)
			{
				RadarComponent.DrawLine(vector3, vector4, Color.white, 1f);
				RadarComponent.DrawLine(vector4, vector5, Color.white, 1f);
				RadarComponent.DrawLine(vector5, vector3, Color.white, 1f);
			}
			if (RadarOptions.type == 1)
			{
				Vector2 vector6 = new Vector2(RadarComponent.radarcenter.x + vector2.x, RadarComponent.radarcenter.y - vector2.y);
				Vector2 vector7 = new Vector2(vector6.x, vector6.y - 10f);
				Vector2 vector8 = new Vector2(vector6.x + 5f, vector6.y + 5f);
				Vector2 vector9 = new Vector2(vector6.x - 5f, vector6.y + 5f);
				vector7 = RadarComponent.RotatePoint(vector7, vector6, Math.Round((double)MainCamera.instance.transform.eulerAngles.y, 2));
				vector8 = RadarComponent.RotatePoint(vector8, vector6, Math.Round((double)MainCamera.instance.transform.eulerAngles.y, 2));
				vector9 = RadarComponent.RotatePoint(vector9, vector6, Math.Round((double)MainCamera.instance.transform.eulerAngles.y, 2));
				RadarComponent.DrawLine(vector7, vector8, Color.white, 1f);
				RadarComponent.DrawLine(vector8, vector9, Color.white, 1f);
				RadarComponent.DrawLine(vector9, vector7, Color.white, 1f);
			}
			if (RadarOptions.ShowVehicles)
			{
				foreach (InteractableVehicle interactableVehicle in VehicleManager.vehicles)
				{
					bool flag;
					if (interactableVehicle == null)
					{
						flag = true;
					}
					else
					{
						Transform transform = interactableVehicle.transform;
						Vector3? vector10 = ((transform != null) ? new Vector3?(transform.position) : null);
						flag = vector10 == null;
					}
					if (!flag)
					{
						if (RadarOptions.ShowVehiclesUnlocked)
						{
							if (!interactableVehicle.isLocked)
							{
								Vector2 vector11 = this.GameToRadarPosition(interactableVehicle.transform.position);
								this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector11.x, RadarComponent.radarcenter.y - vector11.y), Color.black, 3f);
								this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector11.x, RadarComponent.radarcenter.y - vector11.y), ColorUtilities.getColor("_Vehicles"), 2f);
							}
						}
						else
						{
							Vector2 vector12 = this.GameToRadarPosition(interactableVehicle.transform.position);
							this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector12.x, RadarComponent.radarcenter.y - vector12.y), Color.black, 3f);
							this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector12.x, RadarComponent.radarcenter.y - vector12.y), ColorUtilities.getColor("_Vehicles"), 2f);
						}
					}
				}
			}
			if (RadarOptions.ShowPlayers)
			{
				foreach (SteamPlayer steamPlayer in Provider.clients)
				{
					if (((steamPlayer != null) ? steamPlayer.player : null) != OptimizationVariables.MainPlayer)
					{
						bool flag2;
						if (steamPlayer == null)
						{
							flag2 = true;
						}
						else
						{
							Player player = steamPlayer.player;
							Vector3? vector13;
							if (player == null)
							{
								vector13 = null;
							}
							else
							{
								Transform transform2 = player.transform;
								vector13 = ((transform2 != null) ? new Vector3?(transform2.position) : null);
							}
							Vector3? vector10 = vector13;
							flag2 = vector10 == null;
						}
						if (!flag2)
						{
							bool flag3;
							if (steamPlayer == null)
							{
								flag3 = true;
							}
							else
							{
								Player player2 = steamPlayer.player;
								Vector3? vector14;
								if (player2 == null)
								{
									vector14 = null;
								}
								else
								{
									PlayerLook look = player2.look;
									if (look == null)
									{
										vector14 = null;
									}
									else
									{
										Transform aim = look.aim;
										vector14 = ((aim != null) ? new Vector3?(aim.eulerAngles) : null);
									}
								}
								Vector3? vector10 = vector14;
								flag3 = vector10 == null;
							}
							if (!flag3)
							{
								Vector2 vector15 = this.GameToRadarPosition(steamPlayer.player.transform.position);
								Vector2 vector16 = new Vector2(RadarComponent.radarcenter.x + vector15.x, RadarComponent.radarcenter.y - vector15.y);
								Color color = ColorUtilities.getColor("_Players");
								if (FriendUtilities.IsFriendly(steamPlayer.player) && ESPOptions.UsePlayerGroup)
								{
									color = ColorUtilities.getColor("_ESPFriendly");
								}
								if (RadarOptions.DetialedPlayers)
								{
									if (vector16.y > 30f)
									{
										Vector2 vector17 = new Vector2(vector16.x, vector16.y - 10f);
										Vector2 vector18 = new Vector2(vector16.x + 5f, vector16.y + 5f);
										Vector2 vector19 = new Vector2(vector16.x - 5f, vector16.y + 5f);
										vector17 = RadarComponent.RotatePoint(vector17, vector16, Math.Round((double)steamPlayer.player.look.aim.eulerAngles.y, 2));
										vector18 = RadarComponent.RotatePoint(vector18, vector16, Math.Round((double)steamPlayer.player.look.aim.eulerAngles.y, 2));
										vector19 = RadarComponent.RotatePoint(vector19, vector16, Math.Round((double)steamPlayer.player.look.aim.eulerAngles.y, 2));
										RadarComponent.DrawLine(vector17, vector18, color, 1f);
										RadarComponent.DrawLine(vector18, vector19, color, 1f);
										RadarComponent.DrawLine(vector19, vector17, color, 1f);
									}
								}
								else
								{
									this.DrawRadarDot(vector16, Color.black, 3f);
									this.DrawRadarDot(vector16, color, 2f);
								}
							}
						}
					}
				}
			}
			if (MiscComponent.LastDeath != new Vector3(0f, 0f, 0f))
			{
				Vector2 vector20 = this.GameToRadarPosition(MiscComponent.LastDeath);
				this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector20.x, RadarComponent.radarcenter.y - vector20.y), Color.black, 4f);
				this.DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector20.x, RadarComponent.radarcenter.y - vector20.y), Color.grey, 3f);
			}
			GUI.DragWindow();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000026AB File Offset: 0x000008AB
		private void DrawRadarDot(Vector2 pos, Color color, float size = 2f)
		{
			if (pos.y > 28f)
			{
				Drawing.DrawRect(new Rect(pos.x - size, pos.y - size, size * 2f, size * 2f), color, null);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public Vector2 GameToRadarPosition(Vector3 pos)
		{
			Vector2 vector;
			vector.x = pos.x / ((float)Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
			vector.y = pos.z / ((float)Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
			if (RadarOptions.type == 3)
			{
				return RadarComponent.RotatePoint(vector, new Vector2(RadarOptions.vew.width / 2f, (RadarOptions.vew.height + 25f) / 2f), Math.Round((double)MainCamera.instance.transform.eulerAngles.y, 2));
			}
			return vector;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			if (!RadarComponent.lineTex)
			{
				RadarComponent.lineTex = new Texture2D(1, 1);
				RadarComponent.lineTex.SetPixel(0, 0, Color.white);
				RadarComponent.lineTex.Apply();
			}
			Color color2 = GUI.color;
			GUI.color = color;
			float num = Vector2.Angle(pointB - pointA, Vector2.right);
			if (pointA.y > pointB.y)
			{
				num = -num;
			}
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, (pointB - pointA).magnitude, width), RadarComponent.lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public static Vector2 RotatePoint(Vector2 pointToRotate, Vector2 centerPoint, double angleInDegrees)
		{
			double num = angleInDegrees * 0.017453292519943295;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			return new Vector2((float)((int)(num2 * (double)(pointToRotate.x - centerPoint.x) - num3 * (double)(pointToRotate.y - centerPoint.y) + (double)centerPoint.x)), (float)((int)(num3 * (double)(pointToRotate.x - centerPoint.x) + num2 * (double)(pointToRotate.y - centerPoint.y) + (double)centerPoint.y)));
		}

		// Token: 0x04000102 RID: 258
		public static Rect veww;

		// Token: 0x04000103 RID: 259
		public static Vector2 radarcenter;

		// Token: 0x04000104 RID: 260
		public static bool WasEnabled;

		// Token: 0x04000105 RID: 261
		private static Texture2D lineTex;
	}
}
