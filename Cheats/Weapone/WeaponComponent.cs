using System;
using System.Collections.Generic;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000040 RID: 64
	[Component]
	[SpyComponent]
	public class WeaponComponent : MonoBehaviour
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		public void Update()
		{
			ItemGunAsset itemGunAsset;
			if (DrawUtilities.ShouldRun() && !G.BeingSpied && (itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset) != null)
			{
				if (!WeaponComponent.AssetBackups.ContainsKey(itemGunAsset.id))
				{
					float[] array = new float[]
					{
					 itemGunAsset.recoilMax_x, itemGunAsset.recoilMax_y, itemGunAsset.recoilMin_x, itemGunAsset.recoilMin_y, itemGunAsset.recoilCrouch, itemGunAsset.recoilProne, itemGunAsset.recoilSprint, itemGunAsset.aimingRecoilMultiplier, itemGunAsset.spreadAim,
						itemGunAsset.spreadCrouch, itemGunAsset.recoilProne, itemGunAsset.spreadSprint
					};
					WeaponComponent.AssetBackups.Add(itemGunAsset.id, array);
				}
				if (WeaponOptions.NoRecoil)
				{
					itemGunAsset.recoilMax_x = WeaponOptions.NoRecoil1;
					itemGunAsset.recoilMax_y = WeaponOptions.NoRecoil1;
					itemGunAsset.recoilMin_x = WeaponOptions.NoRecoil1;
					itemGunAsset.recoilMin_y = WeaponOptions.NoRecoil1;
					itemGunAsset.recoilCrouch = WeaponOptions.NoRecoil1;
					itemGunAsset.recoilProne = WeaponOptions.NoRecoil1;
					itemGunAsset.recoilSprint = WeaponOptions.NoRecoil1;
					itemGunAsset.aimingRecoilMultiplier = WeaponOptions.NoRecoil1;
				}
				else
				{
					itemGunAsset.recoilMax_x = WeaponComponent.AssetBackups[itemGunAsset.id][1];
					itemGunAsset.recoilMax_y = WeaponComponent.AssetBackups[itemGunAsset.id][2];
					itemGunAsset.recoilMin_x = WeaponComponent.AssetBackups[itemGunAsset.id][3];
					itemGunAsset.recoilMin_y = WeaponComponent.AssetBackups[itemGunAsset.id][4];
					itemGunAsset.recoilCrouch = WeaponComponent.AssetBackups[itemGunAsset.id][5];
					itemGunAsset.recoilProne = WeaponComponent.AssetBackups[itemGunAsset.id][6];
					itemGunAsset.recoilSprint = WeaponComponent.AssetBackups[itemGunAsset.id][7];
					itemGunAsset.aimingRecoilMultiplier = WeaponComponent.AssetBackups[itemGunAsset.id][8];
				}
				if (WeaponOptions.NoSpread)
				{
					itemGunAsset.spreadAim = WeaponOptions.NoSpread1;
					itemGunAsset.spreadCrouch = WeaponOptions.NoSpread1;
					itemGunAsset.spreadProne = WeaponOptions.NoSpread1;
					itemGunAsset.spreadSprint = WeaponOptions.NoSpread1;
					OptionsSettings.useStaticCrosshair = true;
					OptionsSettings.staticCrosshairSize = 0f;
				}
				else
				{
					itemGunAsset.spreadAim = WeaponComponent.AssetBackups[itemGunAsset.id][9];
					itemGunAsset.spreadCrouch = WeaponComponent.AssetBackups[itemGunAsset.id][10];
					itemGunAsset.spreadProne = WeaponComponent.AssetBackups[itemGunAsset.id][11];
					itemGunAsset.spreadSprint = WeaponComponent.AssetBackups[itemGunAsset.id][12];
				}
				if (WeaponOptions.NoSway)
				{
					OptimizationVariables.MainPlayer.animator.viewmodelSwayMultiplier = 0f;
					OptimizationVariables.MainPlayer.animator.scopeSway = new Vector3(0f, 0f, 0f);
				}
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000101C4 File Offset: 0x0000E3C4
		private void FixedUpdate()
		{
			if (this.r > 0 && this.b == 0)
			{
				this.r--;
				this.g++;
			}
			if (this.g > 0 && this.r == 0)
			{
				this.g--;
				this.b++;
			}
			if (this.b > 0 && this.g == 0)
			{
				this.b--;
				this.r++;
			}
			WeaponComponent.RGBRenk = new Color((float)this.r, (float)this.g, (float)this.b, 255f);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0001027C File Offset: 0x0000E47C
		public void OnGUI()
		{
			if (!G.BeingSpied && Event.current.type == EventType.Repaint && DrawUtilities.ShouldRun())
			{
				if (RaycastOptions.SilentAimUseFOV && RaycastOptions.ShowSilentAimUseFOV && RaycastOptions.Enabled)
				{
					DrawUtilities.DrawCircle(Color.red, new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), WeaponComponent.FOVRadius(RaycastOptions.SilentAimFOV));
				}
				if (WeaponOptions.Tracers)
				{
					T.DrawMaterial.SetPass(0);
					GL.PushMatrix();
					GL.LoadProjectionMatrix(OptimizationVariables.MainCam.projectionMatrix);
					GL.modelview = OptimizationVariables.MainCam.worldToCameraMatrix;
					GL.Begin(1);
					for (int i = WeaponComponent.TracerLines.Count - 1; i > -1; i--)
					{
						WeaponComponent.TracerObject tracerObject = WeaponComponent.TracerLines[i];
						if (DateTime.Now - tracerObject.ShotTime > TimeSpan.FromSeconds(3.0))
						{
							WeaponComponent.TracerLines.Remove(tracerObject);
						}
						else
						{
							GL.Color(WeaponComponent.RGBRenk);
							GL.Vertex(tracerObject.PlayerPos);
							GL.Vertex(tracerObject.HitPos);
						}
					}
					GL.End();
					GL.PopMatrix();
				}
				if (WeaponOptions.DamageIndicators && WeaponComponent.DamageIndicators.Count > 0)
				{
					T.DrawMaterial.SetPass(0);
					for (int j = WeaponComponent.DamageIndicators.Count - 1; j > -1; j--)
					{
						WeaponComponent.IndicatorObject indicatorObject = WeaponComponent.DamageIndicators[j];
						if (DateTime.Now - indicatorObject.ShotTime > TimeSpan.FromSeconds(3.0))
						{
							WeaponComponent.DamageIndicators.Remove(indicatorObject);
						}
						else
						{
							GUI.color = Color.red;
							Vector3 vector = OptimizationVariables.MainCam.WorldToScreenPoint(indicatorObject.HitPos + new Vector3(0f, 1f, 0f));
							vector.y = (float)Screen.height - vector.y;
							if (vector.z >= 0f)
							{
								GUIStyle label = GUI.skin.label;
								label.alignment = TextAnchor.MiddleCenter;
								Vector2 vector2 = label.CalcSize(new GUIContent(string.Format("<b>{0}</b>", indicatorObject.Damage)));
								T.DrawOutlineLabel(new Vector2(vector.x - vector2.x / 2f, vector.y - (float)(DateTime.Now - indicatorObject.ShotTime).TotalSeconds * 10f), WeaponComponent.RGBRenk, Color.black, string.Format("<b>{0}</b>", indicatorObject.Damage), null);
								label.alignment = TextAnchor.MiddleLeft;
							}
							GUI.color = Main.GUIColor;
						}
					}
				}
				if (AimbotOptions.ShowAimUseFOV && AimbotOptions.AimUseFOV && AimbotOptions.Enabled)
				{
					DrawUtilities.DrawCircle(Color.green, new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), WeaponComponent.FOVRadius(AimbotOptions.SelectedFOV));
				}
				if (ESPOptions.ShowCoordinates)
				{
					float x = OptimizationVariables.MainPlayer.transform.position.x;
					float y = OptimizationVariables.MainPlayer.transform.position.y;
					float z = OptimizationVariables.MainPlayer.transform.position.z;
					string text = string.Format("", Math.Round((double)x, 2).ToString(), Math.Round((double)y, 2).ToString(), Math.Round((double)z, 2).ToString());
					DrawUtilities.DrawLabel(Font.CreateDynamicFontFromOSFont("Arial", 11), LabelLocation.MiddleLeft, new Vector2((float)(Screen.width - 20), (float)(Screen.height / 2 + 50)), text, ColorUtilities.getColor("_Coordinates"), ColorUtilities.getColor("_CoordinatesTick"), 1, null, 12);
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00010654 File Offset: 0x0000E854
		public static void AddTracer(RaycastInfo ri)
		{
			if (WeaponOptions.Tracers && ri.point != new Vector3(0f, 0f, 0f))
			{
				WeaponComponent.TracerObject tracerObject = new WeaponComponent.TracerObject
				{
					HitPos = ri.point,
					PlayerPos = Player.player.look.aim.transform.position,
					ShotTime = DateTime.Now
				};
				WeaponComponent.TracerLines.Add(tracerObject);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000106D0 File Offset: 0x0000E8D0
		public static void AddDamage(RaycastInfo ri)
		{
			if (WeaponOptions.DamageIndicators && ri.point != new Vector3(0f, 0f, 0f))
			{
				ItemGunAsset itemGunAsset = Player.player.equipment.asset as ItemGunAsset;
				if (itemGunAsset != null && ri.player != null)
				{
					WeaponComponent.IndicatorObject indicatorObject = new WeaponComponent.IndicatorObject
					{
						HitPos = ri.point,
						Damage = Mathf.FloorToInt(DamageTool.getPlayerArmor(ri.limb, ri.player) * itemGunAsset.playerDamageMultiplier.multiply(ri.limb)),
						ShotTime = DateTime.Now
					};
					WeaponComponent.DamageIndicators.Add(indicatorObject);
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00010784 File Offset: 0x0000E984
		internal static float FOVRadius(float fov)
		{
			float fieldOfView = OptimizationVariables.MainCam.fieldOfView;
			return (float)(Math.Tan((double)fov * 0.017453292519943295 / 2.0) / Math.Tan((double)fieldOfView * 0.017453292519943295 / 2.0) * (double)Screen.height);
		}

		// Token: 0x04000152 RID: 338
		public static Dictionary<ushort, float> SpreadBackup = new Dictionary<ushort, float>();

		// Token: 0x04000153 RID: 339
		public static List<WeaponComponent.TracerObject> TracerLines = new List<WeaponComponent.TracerObject>();

		// Token: 0x04000154 RID: 340
		public static List<WeaponComponent.IndicatorObject> DamageIndicators = new List<WeaponComponent.IndicatorObject>();

		// Token: 0x04000155 RID: 341
		private int r = 255;

		// Token: 0x04000156 RID: 342
		private int g;

		// Token: 0x04000157 RID: 343
		private int b;

		// Token: 0x04000158 RID: 344
		public static Color RGBRenk;

		// Token: 0x04000159 RID: 345
		private Color originalColor;

		// Token: 0x0400015A RID: 346
		public Color targetColor;

		// Token: 0x0400015B RID: 347
		public static Dictionary<ushort, float[]> AssetBackups = new Dictionary<ushort, float[]>();

		// Token: 0x0400015C RID: 348
		public static List<TracerLine> Tracers = new List<TracerLine>();

		// Token: 0x0400015D RID: 349
		public static FieldInfo look1 = typeof(PlayerLook).GetField("fov", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400015E RID: 350
		public static FieldInfo ZoomInfo = typeof(UseableGun).GetField("zoom", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400015F RID: 351
		public static FieldInfo AmmoInfo = typeof(UseableGun).GetField("ammo", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000160 RID: 352
		public static MethodInfo UpdateCrosshair = typeof(UseableGun).GetMethod("updateCrosshair", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000161 RID: 353
		public static FieldInfo attachments1 = typeof(UseableGun).GetField("firstAttachments", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000162 RID: 354
		public static FieldInfo fov = typeof(PlayerLook).GetField("fov", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000163 RID: 355
		public static FieldInfo oryaw = typeof(PlayerLook).GetField("_orbitPitch", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000164 RID: 356
		public static FieldInfo orpitch = typeof(PlayerLook).GetField("_orbitYaw", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000165 RID: 357
		public static FieldInfo yaw = typeof(PlayerLook).GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000166 RID: 358
		public static FieldInfo pitch = typeof(PlayerLook).GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000167 RID: 359
		public static FieldInfo lookx = typeof(PlayerLook).GetField("_look_x", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000168 RID: 360
		public static FieldInfo looky = typeof(PlayerLook).GetField("_look_y", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x02000041 RID: 65
		public class TracerObject
		{
			// Token: 0x04000169 RID: 361
			public DateTime ShotTime;

			// Token: 0x0400016A RID: 362
			public Vector3 PlayerPos;

			// Token: 0x0400016B RID: 363
			public Vector3 HitPos;
		}

		// Token: 0x02000042 RID: 66
		public class IndicatorObject
		{
			// Token: 0x0400016C RID: 364
			public DateTime ShotTime;

			// Token: 0x0400016D RID: 365
			public Vector3 HitPos;

			// Token: 0x0400016E RID: 366
			public int Damage;
		}
	}
}
