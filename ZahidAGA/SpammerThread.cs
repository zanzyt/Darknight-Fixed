using System;
using System.Threading;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x0200009D RID: 157
	public static class SpammerThread
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00015D94 File Offset: 0x00013F94
		[Thread]
		public static void Spammer()
		{
			for (;;)
			{
				Thread.Sleep(MiscOptions.SpammerDelay);
				if (MiscOptions.SpammerEnabled & MiscOptions.ChatGlobal)
				{
					ChatManager.sendChat(EChatMode.GLOBAL, MiscOptions.SpamText);
				}
				if (MiscOptions.SpammerEnabled & MiscOptions.ChatArea)
				{
					ChatManager.sendChat(EChatMode.LOCAL, MiscOptions.SpamText);
				}
				if (MiscOptions.SpammerEnabled & MiscOptions.ChatGroup)
				{
					ChatManager.sendChat(EChatMode.GROUP, MiscOptions.SpamText);
				}
				if (MiscOptions.SpammerEnabled & MiscOptions.ChatSay)
				{
					ChatManager.sendChat(EChatMode.SAY, MiscOptions.SpamText);
				}
			}
		}
	}
}
