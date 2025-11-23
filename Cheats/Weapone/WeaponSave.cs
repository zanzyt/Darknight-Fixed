using System;

namespace ZahidAGA
{
	// Token: 0x0200006D RID: 109
	public class WeaponSave
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00003A31 File Offset: 0x00001C31
		public WeaponSave(ushort WeaponID, int SkinID)
		{
			this.WeaponID = WeaponID;
			this.SkinID = SkinID;
		}

		// Token: 0x040001F3 RID: 499
		public ushort WeaponID;

		// Token: 0x040001F4 RID: 500
		public int SkinID;
	}
}
