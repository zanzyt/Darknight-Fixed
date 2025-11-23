using System;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200009F RID: 159
	public static class DrawUtilities
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00003FD4 File Offset: 0x000021D4
		public static bool ShouldRun()
		{
			return Provider.isConnected && !Provider.isLoading && !LoadingUI.isBlocked && OptimizationVariables.MainPlayer != null;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00015EEC File Offset: 0x000140EC
		public static void DrawESPLabel(Vector3 worldpos, Color textcolor, string text, string outlinetext = null)
		{
			GUIContent guicontent = new GUIContent(text);
			if (outlinetext == null)
			{
				outlinetext = text;
			}
			GUIContent guicontent2 = new GUIContent(outlinetext);
			GUIStyle label = GUI.skin.label;
			label.alignment = TextAnchor.MiddleCenter;
			Vector2 vector = label.CalcSize(guicontent);
			Vector3 vector2 = G.MainCamera.WorldToScreenPoint(worldpos);
			vector2.y = (float)Screen.height - vector2.y;
			if (vector2.z >= 0f)
			{
				GUI.color = Color.black;
				GUI.Label(new Rect(vector2.x - vector.x / 2f + 1f, vector2.y + 1f, vector.x, vector.y), guicontent2);
				GUI.Label(new Rect(vector2.x - vector.x / 2f - 1f, vector2.y - 1f, vector.x, vector.y), guicontent2);
				GUI.Label(new Rect(vector2.x - vector.x / 2f + 1f, vector2.y - 1f, vector.x, vector.y), guicontent2);
				GUI.Label(new Rect(vector2.x - vector.x / 2f - 1f, vector2.y + 1f, vector.x, vector.y), guicontent2);
				GUI.color = textcolor;
				GUI.Label(new Rect(vector2.x - vector.x / 2f, vector2.y, vector.x, vector.y), guicontent);
				GUI.color = Main.GUIColor;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00016094 File Offset: 0x00014294
		public static int GetTextSize(ESPVisual vis, double dist)
		{
			int num;
			if (!vis.TextScaling)
			{
				num = vis.FixedTextSize;
			}
			else if (dist > (double)vis.MinTextSizeDistance)
			{
				num = vis.MinTextSize;
			}
			else
			{
				int num2 = vis.MaxTextSize - vis.MinTextSize;
				double num3 = (double)(vis.MinTextSizeDistance / (float)num2);
				num = vis.MaxTextSize - (int)(dist / num3);
			}
			return num;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000160F0 File Offset: 0x000142F0
		public static Vector2[] GetRectangleLines(Camera cam, Bounds b, Color c)
		{
			Vector3[] array = new Vector3[]
			{
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z))
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].y = (float)Screen.height - array[i].y;
			}
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int j = 1; j < array.Length; j++)
			{
				vector = Vector3.Min(vector, array[j]);
				vector2 = Vector3.Max(vector2, array[j]);
			}
			return new Vector2[]
			{
				new Vector2(vector.x, vector.y),
				new Vector2(vector2.x, vector.y),
				new Vector2(vector.x, vector2.y),
				new Vector2(vector2.x, vector2.y)
			};
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000164C0 File Offset: 0x000146C0
		public static Bounds GetBoundsRecursively(GameObject go)
		{
			Bounds bounds = default(Bounds);
			Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				bounds.Encapsulate(componentsInChildren[i].bounds);
			}
			return bounds;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000164FC File Offset: 0x000146FC
		public static Bounds TransformBounds(Transform _transform, Bounds _localBounds)
		{
			Vector3 vector = _transform.TransformPoint(_localBounds.center);
			Vector3 extents = _localBounds.extents;
			Vector3 vector2 = _transform.TransformVector(extents.x, 0f, 0f);
			Vector3 vector3 = _transform.TransformVector(0f, extents.y, 0f);
			Vector3 vector4 = _transform.TransformVector(0f, 0f, extents.z);
			extents.x = Mathf.Abs(vector2.x) + Mathf.Abs(vector3.x) + Mathf.Abs(vector4.x);
			extents.y = Mathf.Abs(vector2.y) + Mathf.Abs(vector3.y) + Mathf.Abs(vector4.y);
			extents.z = Mathf.Abs(vector2.z) + Mathf.Abs(vector3.z) + Mathf.Abs(vector4.z);
			return new Bounds
			{
				center = vector,
				extents = extents
			};
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00016600 File Offset: 0x00014800
		public static void DrawTextWithOutline(Rect centerRect, string text, GUIStyle style, Color innerColor, Color borderColor, int borderWidth, string outlineText = null)
		{
			if (outlineText == null)
			{
				outlineText = text;
			}
			style.normal.textColor = borderColor;
			Rect rect = centerRect;
			rect.x -= (float)borderWidth;
			rect.y -= (float)borderWidth;
			GUI.Label(rect, text, style);
			while (rect.x <= centerRect.x + (float)borderWidth)
			{
				float x = rect.x;
				rect.x = x + 1f;
				GUI.Label(rect, outlineText, style);
			}
			while (rect.y <= centerRect.y + (float)borderWidth)
			{
				float y = rect.y;
				rect.y = y + 1f;
				GUI.Label(rect, outlineText, style);
			}
			while (rect.x >= centerRect.x - (float)borderWidth)
			{
				float x2 = rect.x;
				rect.x = x2 - 1f;
				GUI.Label(rect, outlineText, style);
			}
			while (rect.y >= centerRect.y - (float)borderWidth)
			{
				float y2 = rect.y;
				rect.y = y2 - 1f;
				GUI.Label(rect, outlineText, style);
			}
			style.normal.textColor = innerColor;
			GUI.Label(centerRect, text, style);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00003FF8 File Offset: 0x000021F8
		public static Vector2 InvertScreenSpace(Vector2 dim)
		{
			return new Vector2(dim.x, (float)Screen.height - dim.y);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001673C File Offset: 0x0001493C
		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00016794 File Offset: 0x00014994
		public static void DrawLabel(Font Font, LabelLocation Location, Vector2 W2SVector, string Content, Color BorderColor, Color InnerColor, int BorderWidth, string outerContent = null, int fontSize = 12)
		{
			GUIContent guicontent = new GUIContent(Content);
			GUIStyle guistyle = new GUIStyle
			{
				font = Font,
				fontSize = fontSize
			};
			Vector2 vector = guistyle.CalcSize(guicontent);
			float x = vector.x;
			float y = vector.y;
			Rect rect = new Rect(0f, 0f, x, y);
			switch (Location)
			{
			case LabelLocation.TopRight:
				rect.x = W2SVector.x;
				rect.y = W2SVector.y - y;
				guistyle.alignment = TextAnchor.UpperLeft;
				break;
			case LabelLocation.TopMiddle:
				rect.x = W2SVector.x - x / 2f;
				rect.y = W2SVector.y - y;
				guistyle.alignment = TextAnchor.LowerCenter;
				break;
			case LabelLocation.TopLeft:
				rect.x = W2SVector.x - x;
				rect.y = W2SVector.y - y;
				guistyle.alignment = TextAnchor.UpperRight;
				break;
			case LabelLocation.MiddleRight:
				rect.x = W2SVector.x;
				rect.y = W2SVector.y - y / 2f;
				guistyle.alignment = TextAnchor.MiddleLeft;
				break;
			case LabelLocation.Center:
				rect.x = W2SVector.x - x / 2f;
				rect.y = W2SVector.y - y / 2f;
				guistyle.alignment = TextAnchor.MiddleCenter;
				break;
			case LabelLocation.MiddleLeft:
				rect.x = W2SVector.x - x;
				rect.y = W2SVector.y - y / 2f;
				guistyle.alignment = TextAnchor.MiddleRight;
				break;
			case LabelLocation.BottomRight:
				rect.x = W2SVector.x;
				rect.y = W2SVector.y;
				guistyle.alignment = TextAnchor.LowerLeft;
				break;
			case LabelLocation.BottomMiddle:
				rect.x = W2SVector.x - x / 2f;
				rect.y = W2SVector.y;
				guistyle.alignment = TextAnchor.UpperCenter;
				break;
			case LabelLocation.BottomLeft:
				rect.x = W2SVector.x - x;
				rect.y = W2SVector.y;
				guistyle.alignment = TextAnchor.LowerRight;
				break;
			}
			if (rect.x - 10f >= 0f && rect.y - 10f >= 0f && rect.x + 10f <= (float)Screen.width && rect.y + 10f <= (float)Screen.height)
			{
				DrawUtilities.DrawTextWithOutline(rect, guicontent.text, guistyle, BorderColor, InnerColor, BorderWidth, outerContent);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00016A0C File Offset: 0x00014C0C
		public static Vector2 Get3DW2SVector(Camera cam, Bounds b, LabelLocation location)
		{
			Vector2 vector;
			switch (location)
			{
			case LabelLocation.TopRight:
			case LabelLocation.TopMiddle:
			case LabelLocation.TopLeft:
				vector = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(new Vector3(b.center.x, b.center.y + b.extents.y, b.center.z)));
				break;
			case LabelLocation.MiddleRight:
			case LabelLocation.Center:
			case LabelLocation.MiddleLeft:
				vector = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(b.center));
				break;
			case LabelLocation.BottomRight:
			case LabelLocation.BottomMiddle:
			case LabelLocation.BottomLeft:
				vector = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(new Vector3(b.center.x, b.center.y - b.extents.y, b.center.z)));
				break;
			default:
				vector = Vector2.zero;
				break;
			}
			return vector;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00016AFC File Offset: 0x00014CFC
		public static Vector2 Get2DW2SVector(Camera cam, Vector2[] Corners, LabelLocation location)
		{
			Vector2 vector;
			switch (location)
			{
			case LabelLocation.TopRight:
				vector = Corners[1];
				break;
			case LabelLocation.TopMiddle:
				vector = new Vector2((Corners[0].x + Corners[1].x) / 2f, Corners[0].y);
				break;
			case LabelLocation.TopLeft:
				vector = Corners[0];
				break;
			case LabelLocation.MiddleRight:
				vector = new Vector2(Corners[0].x, (Corners[1].y + Corners[2].y) / 2f);
				break;
			case LabelLocation.Center:
				vector = new Vector2(Corners[2].x, (Corners[1].y + Corners[2].y) / 2f);
				break;
			case LabelLocation.MiddleLeft:
				vector = new Vector2((Corners[2].x + Corners[3].x) / 2f, (Corners[1].y + Corners[2].y) / 2f);
				break;
			case LabelLocation.BottomRight:
				vector = Corners[2];
				break;
			case LabelLocation.BottomMiddle:
				vector = new Vector2((Corners[2].x + Corners[3].x) / 2f, Corners[2].y);
				break;
			case LabelLocation.BottomLeft:
				vector = Corners[3];
				break;
			default:
				vector = Vector2.zero;
				break;
			}
			return vector;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00016C90 File Offset: 0x00014E90
		public static Vector3[] GetBoxVectors(Vector3 boxPosition, Vector3[] boxVectors, Bounds b) 
        {
			Vector3 center = b.center;
			Vector3 extents = b.extents;
			return new Vector3[]
			{
				new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z),
				new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z),
				new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z),
				new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z),
				new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z),
				new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z),
				new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z),
				new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z)
			};
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00016E4C File Offset: 0x0001504C
		public static void PrepareRectangleLines(Vector2[] nvectors, Color c)
		{
			ESPVariables.DrawBuffer2.Enqueue(new ESPBox2
			{
				Color = c,
				Vertices = new Vector2[]
				{
					nvectors[0],
					nvectors[1],
					nvectors[1],
					nvectors[3],
					nvectors[3],
					nvectors[2],
					nvectors[2],
					nvectors[0] 
				}
			});
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00016EF0 File Offset: 0x000150F0
		public static void PrepareBoxLines(Vector3[] vectors, Color c)
		{
			ESPVariables.DrawBuffer.Enqueue(new ESPBox
			{
				Color = c,
				Vertices = new Vector3[]
				{
					vectors[0],
					vectors[1],
					vectors[1],
					vectors[3],
					vectors[3],
					vectors[2],
					vectors[2],
					vectors[0],
					vectors[4],
					vectors[5],
					vectors[5],
					vectors[7],
					vectors[7],
					vectors[6],
					vectors[6],
					vectors[4],
					vectors[0],
					vectors[4],
					vectors[1],
					vectors[5],
					vectors[2],
					vectors[6],
					vectors[3],
					vectors[7]
				}
			});
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00017084 File Offset: 0x00015284
		public static void DrawCircle(Color Col, Vector2 Center, float Radius)
		{
			GL.PushMatrix();
			T.DrawMaterial.SetPass(0);
			GL.Begin(1);
			GL.Color(Col);
			for (float num = 0f; num < 6.2831855f; num += RaycastOptions.FovKalınlık)
			{
				GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
				GL.Vertex(new Vector3(Mathf.Cos(num + RaycastOptions.FovKalınlık) * Radius + Center.x, Mathf.Sin(num + RaycastOptions.FovKalınlık) * Radius + Center.y));
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001712C File Offset: 0x0001532C
		public static void DrawMenuRect(float x, float y, float width, float height, Color fillcolor)
		{
			Color black = Color.black;
			Drawing.DrawRect(new Rect(x, y, width, 5f), black, null);
			Drawing.DrawRect(new Rect(x, y, 5f, height), black, null);
			Drawing.DrawRect(new Rect(x, y + (height - 5f), width, 5f), black, null);
			Drawing.DrawRect(new Rect(x + (width - 5f), 0f, 5f, height), black, null);
			Drawing.DrawRect(new Rect(5f, 5f, width - 10f, height - 10f), fillcolor, null);
		}

        internal static void GetBoxVectors(Vector3 boxPosition, Vector3[] boxVectors, ColorVariable colorVariable, int textSize, bool v)
        {
            throw new NotImplementedException();
        }

        internal static void DrawTextWithOutline(Vector2 vector2, string simpleText, ColorVariable colorVariable, int textSize, bool v)
        {
            throw new NotImplementedException();
        }

        internal static void DrawLabel(Vector3 labelPosition, string displayText, ColorVariable colorVariable, int textSize, bool v)
        {
            throw new NotImplementedException();
        }

        // Token: 0x0400039D RID: 925
        public static Color GUIColor;
	}
}
