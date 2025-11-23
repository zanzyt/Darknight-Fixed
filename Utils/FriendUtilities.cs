using System;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x020000A0 RID: 160
	public static class FriendUtilities
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000171C8 File Offset: 0x000153C8
		public static bool IsFriendly(Player player)
		{
			return player.quests.isMemberOfGroup(OptimizationVariables.MainPlayer.quests.groupID) || MiscOptions.Friends.Contains(player.channel.owner.playerID.steamID.m_SteamID);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00017218 File Offset: 0x00015418
		public static void AddFriend(Player Friend)
		{
			ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
			if (!MiscOptions.Friends.Contains(steamID))
			{
				MiscOptions.Friends.Add(steamID);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001725C File Offset: 0x0001545C
		public static void RemoveFriend(Player Friend)
		{
			ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
			if (MiscOptions.Friends.Contains(steamID))
			{
				MiscOptions.Friends.Remove(steamID);
			}
		}
	}
}
