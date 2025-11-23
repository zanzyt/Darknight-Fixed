using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200001C RID: 28
	public class T
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00008C00 File Offset: 0x00006E00
		public static void DrawColorBox(Color color, Rect Pos, int thinkness = 1)
		{
			GUI.skin = AssetUtilities.Skin;
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUI.Button(new Rect(Pos.x, Pos.y, Pos.width, (float)thinkness), " ", T.textureStyle);
			GUI.Button(new Rect(Pos.x + Pos.width, Pos.y, (float)thinkness, Pos.height), " ", T.textureStyle);
			GUI.Button(new Rect(Pos.x, Pos.y, (float)thinkness, Pos.height), " ", T.textureStyle);
			GUI.Button(new Rect(Pos.x, Pos.y + Pos.height, Pos.width, (float)thinkness), " ", T.textureStyle);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002200 File Offset: 0x00000400
		public static void DrawColorLayout(Color color, GUILayoutOption[] options = null)
		{
			GUI.skin = AssetUtilities.Skin;
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUILayout.Button(" ", T.textureStyle, options);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000222D File Offset: 0x0000042D
		public static float GetDistance(Vector3 endpos)
		{
			return (float)Math.Round((double)Vector3.Distance(Player.player.look.aim.position, endpos));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public static void AddNotification(string text)
		{
			NotificationWindow notificationWindow = new NotificationWindow();
			notificationWindow.info = text;
			T.Notifications.Add(notificationWindow);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00008D10 File Offset: 0x00006F10
		public static void DrawSnapline(Color color)
		{
			ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
			float num = ((itemGunAsset != null) ? itemGunAsset.range : 15.5f);
			GameObject gameObject;
			Vector3 vector;
			RaycastUtilities.GetTargetObject(RaycastUtilities.Objects, out gameObject, out vector, num);
			Vector3 vector2 = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.9f, gameObject.transform.position.z);
			Vector3 vector3 = OptimizationVariables.MainCam.WorldToScreenPoint(vector2);
			if (vector3.z > 0f)
			{
				vector3.y = (float)Screen.height - vector3.y;
				GL.PushMatrix();
				GL.Begin(1);
				T.DrawMaterial.SetPass(0);
				GL.Color(color);
				GL.Vertex3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f);
				GL.Vertex3(vector3.x, vector3.y, 0f);
				GL.End();
				GL.PopMatrix();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00008E1C File Offset: 0x0000701C
		public static void DrawSkeleton(Transform transform, Color color)
		{
			Vector3 vector = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Skull") - new Vector3(0f, 0.4f, 0f));
			vector.y = (float)Screen.height - vector.y;
			Vector3 vector2 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Spine") - new Vector3(0f, 0.4f, 0f));
			vector2.y = (float)Screen.height - vector2.y;
			Vector3 vector3 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Right_Shoulder") - new Vector3(0f, 0.4f, 0f));
			vector3.y = (float)Screen.height - vector3.y;
			Vector3 vector4 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Left_Shoulder") - new Vector3(0f, 0.4f, 0f));
			vector4.y = (float)Screen.height - vector4.y;
			Vector3 vector5 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Right_Arm") - new Vector3(0f, 0.4f, 0f));
			vector5.y = (float)Screen.height - vector5.y;
			Vector3 vector6 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Left_Arm") - new Vector3(0f, 0.4f, 0f));
			vector6.y = (float)Screen.height - vector6.y;
			Vector3 vector7 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Right_Hand") - new Vector3(0f, 0.4f, 0f));
			vector7.y = (float)Screen.height - vector7.y;
			Vector3 vector8 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Left_Hand") - new Vector3(0f, 0.4f, 0f));
			vector8.y = (float)Screen.height - vector8.y;
			Vector3 vector9 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Left_Hip") - new Vector3(0f, 0.4f, 0f));
			vector9.y = (float)Screen.height - vector9.y;
			Vector3 vector10 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Right_Hip") - new Vector3(0f, 0.4f, 0f));
			vector10.y = (float)Screen.height - vector10.y;
			Vector3 vector11 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Left_Foot") - new Vector3(0f, 0.4f, 0f));
			vector11.y = (float)Screen.height - vector11.y;
			Vector3 vector12 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Right_Foot") - new Vector3(0f, 0.4f, 0f));
			vector12.y = (float)Screen.height - vector12.y;
			Vector3 vector13 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Left_Leg") - new Vector3(0f, 0.4f, 0f));
			vector13.y = (float)Screen.height - vector13.y;
			Vector3 vector14 = OptimizationVariables.MainCam.WorldToScreenPoint(T.GetLimbPosition(transform, "Right_Leg") - new Vector3(0f, 0.4f, 0f));
			vector14.y = (float)Screen.height - vector14.y;
			GL.PushMatrix();
			GL.Begin(1);
			T.DrawMaterial.SetPass(0);
			GL.Color(color);
			GL.Vertex3(vector.x, vector.y, 0f);
			GL.Vertex3(vector2.x, vector2.y, 0f);
			GL.Vertex3(vector2.x, vector2.y, 0f);
			GL.Vertex3(vector4.x, vector4.y, 0f);
			GL.Vertex3(vector4.x, vector4.y, 0f);
			GL.Vertex3(vector6.x, vector6.y, 0f);
			GL.Vertex3(vector6.x, vector6.y, 0f);
			GL.Vertex3(vector8.x, vector8.y, 0f);
			GL.Vertex3(vector2.x, vector2.y, 0f);
			GL.Vertex3(vector3.x, vector3.y, 0f);
			GL.Vertex3(vector3.x, vector3.y, 0f);
			GL.Vertex3(vector5.x, vector5.y, 0f);
			GL.Vertex3(vector5.x, vector5.y, 0f);
			GL.Vertex3(vector7.x, vector7.y, 0f);
			GL.Vertex3(vector2.x, vector2.y, 0f);
			GL.Vertex3(vector9.x, vector9.y, 0f);
			GL.Vertex3(vector9.x, vector9.y, 0f);
			GL.Vertex3(vector13.x, vector13.y, 0f);
			GL.Vertex3(vector13.x, vector13.y, 0f);
			GL.Vertex3(vector11.x, vector11.y, 0f);
			GL.Vertex3(vector2.x, vector2.y, 0f);
			GL.Vertex3(vector10.x, vector10.y, 0f);
			GL.Vertex3(vector10.x, vector10.y, 0f);
			GL.Vertex3(vector14.x, vector14.y, 0f);
			GL.Vertex3(vector14.x, vector14.y, 0f);
			GL.Vertex3(vector12.x, vector12.y, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000946C File Offset: 0x0000766C
		public static Vector3 WorldToScreen(Vector3 worldpos)
		{
			Vector3 vector = OptimizationVariables.MainCam.WorldToScreenPoint(worldpos);
			vector.y = (float)Screen.height - vector.y;
			return new Vector3(vector.x, vector.y);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000094AC File Offset: 0x000076AC
		public static void DrawOutlineLabel(Vector2 rect, Color textcolor, Color outlinecolor, string text, string outlinetext = null)
		{
			GUIContent guicontent = new GUIContent(text);
			if (outlinetext == null)
			{
				outlinetext = text;
			}
			GUIContent guicontent2 = new GUIContent(outlinetext);
			Vector2 vector = GUI.skin.label.CalcSize(guicontent);
			GUI.color = Color.black;
			GUI.Label(new Rect(rect.x + 1f, rect.y + 1f, vector.x, vector.y), guicontent2);
			GUI.Label(new Rect(rect.x - 1f, rect.y - 1f, vector.x, vector.y), guicontent2);
			GUI.Label(new Rect(rect.x + 1f, rect.y - 1f, vector.x, vector.y), guicontent2);
			GUI.Label(new Rect(rect.x - 1f, rect.y + 1f, vector.x, vector.y), guicontent2);
			GUI.color = textcolor;
			GUI.Label(new Rect(rect.x, rect.y, vector.x, vector.y), guicontent);
			GUI.color = Main.GUIColor;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002250 File Offset: 0x00000450
		public static void DrawColor(Rect position, Color color)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUI.Box(position, GUIContent.none, T.textureStyle);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000095DC File Offset: 0x000077DC
		public static Vector3 GetLimbPosition(Transform target, string objName)
		{
			Transform[] componentsInChildren = target.transform.GetComponentsInChildren<Transform>();
			Vector3 vector = Vector3.zero;
			if (componentsInChildren == null)
			{
				return vector;
			}
			foreach (Transform transform in componentsInChildren)
			{
				if (!(transform.name.Trim() != objName))
				{
					vector = transform.position + new Vector3(0f, 0.4f, 0f);
					break;
				}
			}
			return vector;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002272 File Offset: 0x00000472
		public static float? GetGunDistance()
		{
			Player player = Player.LocalPlayer;
			object obj;
			if (player == null)
			{
				obj = null;
			}
			else
			{
				PlayerEquipment equipment = player.equipment;
				obj = ((equipment != null) ? equipment.asset : null);
			}
			ItemGunAsset itemGunAsset = obj as ItemGunAsset;
			return new float?((itemGunAsset != null) ? itemGunAsset.range : 15.5f);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00009650 File Offset: 0x00007850
		public static void DrawCircle(Color Col, Vector2 Center, float Radius)
		{
			GL.PushMatrix();
			T.DrawMaterial.SetPass(0);
			GL.Begin(1);
			GL.Color(Col);
			for (float num = 0f; num < 6.2831855f; num += 0.05f)
			{
				GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
				GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * Radius + Center.x, Mathf.Sin(num + 0.05f) * Radius + Center.y));
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x040000B8 RID: 184
		public static List<NotificationWindow> Notifications = new List<NotificationWindow>();

		// Token: 0x040000B9 RID: 185
		public static Material DrawMaterial;

		// Token: 0x040000BA RID: 186
		private static readonly Texture2D backgroundTexture = Texture2D.whiteTexture;

		// Token: 0x040000BB RID: 187
		private static readonly GUIStyle textureStyle = new GUIStyle
		{
			normal = new GUIStyleState
			{
				background = T.backgroundTexture
			}
		};
	}
}
