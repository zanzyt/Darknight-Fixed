using System;

namespace ZahidAGA
{
	// Token: 0x02000067 RID: 103
	public class ESPVisual
	{
		// Token: 0x040001C1 RID: 449
		public bool Enabled;

		// Token: 0x040001C2 RID: 450
		public bool Boxes;

		// Token: 0x040001C3 RID: 451
		public bool Target;

		// Token: 0x040001C4 RID: 452
		public bool ShowName;

		// Token: 0x040001C5 RID: 453
		public bool ShowDistance;

		// Token: 0x040001C6 RID: 454
		public bool ShowAngle;

		// Token: 0x040001C7 RID: 455
		public bool TwoDimensional;

		// Token: 0x040001C8 RID: 456
		public bool Glow;

		// Token: 0x040001C9 RID: 457
		public bool RGBGlow;

		// Token: 0x040001CA RID: 458
		public bool InfiniteDistance;

		// Token: 0x040001CB RID: 459
		public bool LineToObject;

		// Token: 0x040001CC RID: 460
		public bool TextScaling;

		// Token: 0x040001CD RID: 461
		public bool UseObjectCap;

		// Token: 0x040001CE RID: 462
		public bool CustomTextColor;

		// Token: 0x040001CF RID: 463
		public LabelLocation Location;

		// Token: 0x040001D0 RID: 464
		public float Distance;

		// Token: 0x040001D1 RID: 465
		public float MinTextSizeDistance;

		// Token: 0x040001D2 RID: 466
		public int BorderStrength;

		// Token: 0x040001D3 RID: 467
		public int FixedTextSize;

		// Token: 0x040001D4 RID: 468
		public int MinTextSize;

		// Token: 0x040001D5 RID: 469
		public int MaxTextSize;

		// Token: 0x040001D6 RID: 470
		public int ObjectCap;

        public bool ShowLabel { get; internal set; }
        public bool ShowBox { get; internal set; }
    }
}
