using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000017 RID: 23
	public class NotificationWindow
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000077CC File Offset: 0x000059CC
		public void Run()
		{
			GUI.skin = AssetUtilities.Skin;
			if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > this.ExpireTime)
			{
				T.Notifications.Remove(this);
				return;
			}
			if (100L > this.ExpireTime - DateTimeOffset.Now.ToUnixTimeMilliseconds())
			{
				long num = (this.ExpireTime - DateTimeOffset.Now.ToUnixTimeMilliseconds()) * 3L;
				GUI.Window(this.NotificationNum + 10, new Rect((float)((long)Screen.width - num), (float)(250 + 70 * this.NotificationNum), (float)num, 60f), new GUI.WindowFunction(this.Draw), "", "verticalsliderthumb");
				return;
			}
			GUI.Window(this.NotificationNum + 10, new Rect((float)(Screen.width - 250), (float)(250 + 70 * this.NotificationNum), 300f, 60f), new GUI.WindowFunction(this.Draw), "", "verticalsliderthumb");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000078DC File Offset: 0x00005ADC
		private void Draw(int windowID)
		{
			GUI.Label(new Rect(25f, 25f, 260f, 70f), "<size=20>" + this.info + "</size>");
			GUI.DrawTexture(new Rect(300f - (float)(this.ExpireTime - DateTimeOffset.Now.ToUnixTimeMilliseconds() - 100L) / 9900f * 300f, 50f, 300f, 10f), AssetUtilities.Skin.verticalScrollbar.normal.background, ScaleMode.StretchToFill);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00007978 File Offset: 0x00005B78
		public NotificationWindow()
		{
			this.ExpireTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000L;
		}

		// Token: 0x040000A4 RID: 164
		public string info;

		// Token: 0x040000A5 RID: 165
		private long ExpireTime; 

		// Token: 0x040000A6 RID: 166
		public int NotificationNum = T.Notifications.Count + 1;
	}
}
