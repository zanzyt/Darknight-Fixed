using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200003E RID: 62
	public static class Prefab
	{
		// Token: 0x0600017F RID: 383 RVA: 0x000033FA File Offset: 0x000015FA
		public static bool AbsButton(Rect area, string text, params GUILayoutOption[] options)
		{
			Drawing.DrawRect(area, MenuComponent._OutlineBorderBlack, null);
			return GUI.Button(MenuUtilities.Inline(area, 1f), text, Prefab._ButtonStyle);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003423 File Offset: 0x00001623
		public static bool Button(string text, Rect rect)
		{
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			return GUI.Button(MenuUtilities.Inline(rect, 1f), text, Prefab._ButtonStyle);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000F760 File Offset: 0x0000D960
		public static bool Button(string text, float width, float height = 25f, params GUILayoutOption[] options)
		{
			List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
			list.Add(GUILayout.Height(height));
			list.Add(GUILayout.Width(width));
			return Prefab.AbsButton(GUILayoutUtility.GetRect(width, height, list.ToArray()), text, options);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		public static int Arrows(float width, int listEntry, string content, int max)
		{
			Rect rect = GUILayoutUtility.GetRect(width, 25f, new GUILayoutOption[]
			{
				GUILayout.Height(25f),
				GUILayout.Width(width)
			});
			if (Prefab.Button("<", new Rect(rect.x, rect.y, 25f, 25f)))
			{
				listEntry--;
			} 
			GUI.Label(MenuUtilities.Inline(new Rect(rect.x + 25f, rect.y, rect.width - 50f, 25f), 1f), content, Prefab._listStyle);
			if (Prefab.Button(">", new Rect(rect.x + rect.width - 25f, rect.y, 25f, 25f)))
			{
				listEntry++;
			}
			if (listEntry < 0)
			{
				listEntry = max;
			}
			else if (listEntry > max)
			{
				listEntry = 0;
			}
			return listEntry;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000344C File Offset: 0x0000164C
		public static Rect Inline(Rect rect, int border = 1)
		{
			return new Rect(rect.x + (float)border, rect.y + (float)border, rect.width - (float)(border * 2), rect.height - (float)(border * 2));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000F894 File Offset: 0x0000DA94
		public static void ScrollView(Rect rect, ref Vector2 scrollpos, Action code)
		{
			GUILayout.BeginArea(rect);
			scrollpos = GUILayout.BeginScrollView(scrollpos, false, true, new GUILayoutOption[0]);
			try
			{
				code();
			}
			catch (Exception)
			{
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public static void ScrollView(Rect rect, ref SerializableVector2 scrollpos, Action code)
		{
			GUILayout.BeginArea(rect);
			scrollpos = GUILayout.BeginScrollView(scrollpos.ToVector2(), false, true, new GUILayoutOption[0]);
			try
			{
				code();
			}
			catch (Exception)
			{
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000F93C File Offset: 0x0000DB3C
		public static float Slider(float left, float right, float value, int size)
		{
			GUIStyle sliderThumbStyle = Prefab._sliderThumbStyle;
			GUIStyle sliderStyle = Prefab._sliderStyle;
			float num = ((sliderThumbStyle.fixedWidth != 0f) ? sliderThumbStyle.fixedWidth : ((float)sliderThumbStyle.padding.horizontal));
			value = GUILayout.HorizontalSlider(value, left, right, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, new GUILayoutOption[] { GUILayout.Width((float)size) });
			Rect lastRect = GUILayoutUtility.GetLastRect();
			float num2 = (lastRect.width - (float)sliderStyle.padding.horizontal - num) / (right - left);
			Rect rect = new Rect((value - left) * num2 + lastRect.x + (float)sliderStyle.padding.left, lastRect.y + (float)sliderStyle.padding.top, num, lastRect.height - (float)sliderStyle.padding.vertical);
			Drawing.DrawRect(lastRect, MenuComponent._FillLightBlack, null);
			Drawing.DrawRect(new Rect(lastRect.x, lastRect.y + lastRect.height / 2f - 2f, lastRect.width, 4f), Prefab._ToggleBoxBG, null);
			Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(MenuUtilities.Inline(rect, 1f), Prefab._MenuTabStyle.onNormal.textColor, null);
			GUILayout.Space(5f);
			return value;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		public static bool MenuTab2(string text, ref bool state, int fontsize = 29)
		{
			bool flag = false;
			int fontSize = Prefab._MenuTabStyle2.fontSize;
			Prefab._MenuTabStyle2.fontSize = fontsize;
			bool flag2 = GUILayout.Toggle(state, text.ToUpper(), Prefab._MenuTabStyle2, Array.Empty<GUILayoutOption>());
			if (state != flag2)
			{
				state = !state;
				flag = true;
			}
			Prefab._MenuTabStyle2.fontSize = fontSize;
			return flag;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000347F File Offset: 0x0000167F
		public static void ToggleButton(ref bool toggle, string head, GUIStyle gUIStyle)
		{
			if (GUILayout.Button(head, gUIStyle, new GUILayoutOption[0]))
			{
				toggle = !toggle;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00003497 File Offset: 0x00001697
		public static bool Toggle(ref bool value, string head, int fakeint = 0)
		{
			value = GUILayout.Toggle(value, head, new GUILayoutOption[0]);
			return value;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		public static void ToggleLast(ref bool state)
		{
			Rect lastRect = GUILayoutUtility.GetLastRect();
			lastRect = new Rect(lastRect.x + 161f, lastRect.y - 14f, 13f, 13f);
			Rect rect = MenuUtilities.Inline(lastRect, 1f);
			Drawing.DrawRect(lastRect, MenuComponent._OutlineBorderBlack, null);
			Drawing.DrawRect(rect, Prefab._ToggleBoxBG, null);
			if (GUI.Button(lastRect, GUIContent.none, Prefab._TextStyle))
			{
				state = !state;
			}
			if (state)
			{
				Drawing.DrawRect(rect, MenuComponent._Accent2, null);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000034AB File Offset: 0x000016AB
		public static bool Toggle(string head, ref bool value, int fakeint = 0)
		{
			value = GUILayout.Toggle(value, head, new GUILayoutOption[0]);
			return value;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public static string TextField(string text, string label, int width)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(label, new GUILayoutOption[0]);
			float y = new GUIStyle().CalcSize(new GUIContent("asdf")).y;
			Rect rect = GUILayoutUtility.GetRect((float)width, y + 10f);
			text = GUI.TextField(new Rect(rect.x + 4f, rect.y + 2f, rect.width, rect.height), text);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			return text;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000034BF File Offset: 0x000016BF
		public static string TextField(string text, string label)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(label, new GUILayoutOption[0]);
			text = GUILayout.TextField(text.ToString(), new GUILayoutOption[] { GUILayout.Width(565f) });
			GUILayout.EndHorizontal();
			return text;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000FC28 File Offset: 0x0000DE28
		public static int TextField(int text, string label, int maxL, int min = 0, int max = 255)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(label, new GUILayoutOption[0]);
			int num = int.Parse(GUILayout.TextField(text.ToString(), maxL, new GUILayoutOption[] { GUILayout.Width((float)(maxL * 10 + 1)) }));
			if (num >= min && num <= max)
			{
				text = num;
			}
			GUILayout.EndHorizontal();
			return text;
		}

		// Token: 0x04000141 RID: 321
		public static GUIStyle _None;

		// Token: 0x04000142 RID: 322
		public static GUIStyle _MenuTabStyle;

		// Token: 0x04000143 RID: 323
		public static GUIStyle _MenuTabStyle2;

		// Token: 0x04000144 RID: 324
		public static GUIStyle _HeaderStyle;

		// Token: 0x04000145 RID: 325
		public static GUIStyle _HeaderStyle2;

		// Token: 0x04000146 RID: 326
		public static GUIStyle _TextStyle;

		// Token: 0x04000147 RID: 327
		public static GUIStyle _sliderStyle;

		// Token: 0x04000148 RID: 328
		public static GUIStyle _sliderThumbStyle;

		// Token: 0x04000149 RID: 329
		public static GUIStyle _sliderVThumbStyle;

		// Token: 0x0400014A RID: 330
		public static GUIStyle _listStyle;

		// Token: 0x0400014B RID: 331
		public static GUIStyle _ButtonStyle;

		// Token: 0x0400014C RID: 332
		public static Color32 _ToggleBoxBG;

		// Token: 0x0400014D RID: 333
		private static int popupListHash = "PopupList".GetHashCode();

		// Token: 0x0400014E RID: 334
		public static Regex digitsOnly = new Regex("[^\\d]");
	}
}
