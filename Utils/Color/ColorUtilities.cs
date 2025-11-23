using System;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200009E RID: 158
	internal class ColorUtilities
	{
		// Token: 0x060002BB RID: 699 RVA: 0x00003F5C File Offset: 0x0000215C
		public static void addColor(ColorVariable ColorVariable)
		{
			if (!ColorOptions.DefaultColorDict.ContainsKey(ColorVariable.identity))
			{
				ColorOptions.DefaultColorDict.Add(ColorVariable.identity, ColorVariable);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00015E0C File Offset: 0x0001400C
		public static ColorVariable getColor(string identifier)
		{
			ColorVariable colorVariable;
			ColorVariable colorVariable2;
			if (ColorOptions.ColorDict.TryGetValue(identifier, out colorVariable))
			{
				colorVariable2 = colorVariable;
			}
			else
			{
				colorVariable2 = ColorOptions.errorColor;
			}
			return colorVariable2;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00015E34 File Offset: 0x00014034
		public static string getHex(string identifier)
		{
			ColorVariable colorVariable;
			string text;
			if (ColorOptions.ColorDict.TryGetValue(identifier, out colorVariable))
			{
				text = ColorUtilities.ColorToHex(colorVariable);
			}
			else
			{
				text = ColorUtilities.ColorToHex(ColorOptions.errorColor);
			}
			return text;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00015E70 File Offset: 0x00014070
		public static void setColor(string identifier, Color32 color)
		{
			ColorVariable colorVariable;
			if (ColorOptions.ColorDict.TryGetValue(identifier, out colorVariable))
			{
				colorVariable.color = color.ToSerializableColor();
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00003F84 File Offset: 0x00002184
		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + "FF";
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00003FC3 File Offset: 0x000021C3
		public static ColorVariable[] getArray()
		{
			return ColorOptions.ColorDict.Values.ToArray<ColorVariable>();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00015E98 File Offset: 0x00014098
		public static Color32 HexToColor(string hex)
		{
			byte b = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
			byte b2 = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
			byte b3 = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
			return new Color32(b, b2, b3, byte.MaxValue);
		}
	}
}
