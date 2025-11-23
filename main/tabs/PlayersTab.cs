using System;
using System.Linq;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000018 RID: 24
	public static class PlayersTab
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000079B8 File Offset: 0x00005BB8
		public static SteamPlayer GetSteamPlayer(Player player)
		{
			foreach (SteamPlayer steamPlayer in Provider.clients)
			{
				if (steamPlayer.player == player)
				{
					return steamPlayer;
				}
			}
			return null;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00007A18 File Offset: 0x00005C18
		public static void Tab()
		{
			GUILayout.BeginArea(new Rect(340f, 20f, 650f, 420f), "Online Players", "box");
			PlayersTab.SearchString = Prefab.TextField(PlayersTab.SearchString, "Search: ");
			PlayersTab.scrollPosition1 = GUILayout.BeginScrollView(PlayersTab.scrollPosition1, Array.Empty<GUILayoutOption>());
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				Player player = Provider.clients[i].player;
				if (!(player == OptimizationVariables.MainPlayer) && !(player == null) && (!(PlayersTab.SearchString != "") || player.name.IndexOf(PlayersTab.SearchString, StringComparison.OrdinalIgnoreCase) != -1))
				{
					bool flag = FriendUtilities.IsFriendly(player);
					bool flag2 = MiscOptions.SpectatedPlayer == player;
					bool flag3 = false;
					bool flag4 = player == PlayersTab.SelectedPlayer;
					string text = (flag3 ? "<color=#ff0000ff>" : (flag ? "<color=#00ff00ff>" : ""));
					if (GUILayout.Button(string.Concat(new string[]
					{
						flag4 ? "<b>" : "",
						flag2 ? "<color=#0000ffff>[SPECTATING]</color> " : "",
						text,
						player.name,
						(flag || flag3) ? "</color>" : "",
						flag4 ? "</b>" : ""
					}), "NavBox", Array.Empty<GUILayoutOption>()))
					{
						PlayersTab.SelectedPlayer = player;
					}
					GUILayout.Space(2f);
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(340f, 450f, 250f, 180f), "Other", "box");
			if (PlayersTab.SelectedPlayer != null)
			{
				CSteamID steamID = PlayersTab.SelectedPlayer.channel.owner.playerID.steamID;
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.BeginVertical(new GUILayoutOption[0]);
				if (FriendUtilities.IsFriendly(PlayersTab.SelectedPlayer))
				{
					if (GUILayout.Button("Remove Friends", "verticalslider", Array.Empty<GUILayoutOption>()))
					{
						FriendUtilities.RemoveFriend(PlayersTab.SelectedPlayer);
					}
				}
				else if (GUILayout.Button("Add Friends", "verticalslider", Array.Empty<GUILayoutOption>()))
				{
					FriendUtilities.AddFriend(PlayersTab.SelectedPlayer);
				}
				if (GUILayout.Button("Open Steam Profile", "verticalslider", Array.Empty<GUILayoutOption>()))
				{
					Provider.provider.browserService.open("http://steamcommunity.com/profiles/" + PlayersTab.SelectedPlayer.channel.owner.playerID.steamID.ToString());
				}
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(600f, 450f, 390f, 180f), "Info", "box");
			if (!(PlayersTab.SelectedPlayer == null))
			{
				string text2 = Convert.ToString(Convert.ToInt32(Provider.clients.Count((SteamPlayer c) => c.player != PlayersTab.SelectedPlayer && c.player.quests.isMemberOfSameGroupAs(PlayersTab.SelectedPlayer))) + 1);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.BeginVertical(new GUILayoutOption[0]);
				float x = PlayersTab.SelectedPlayer.transform.position.x;
				float y = PlayersTab.SelectedPlayer.transform.position.y;
				float z = PlayersTab.SelectedPlayer.transform.position.z;
				string.Format("", Math.Round((double)x, 2).ToString(), Math.Round((double)y, 2).ToString(), Math.Round((double)z, 2).ToString());
				GUILayout.TextField("Location: " + LocationUtilities.GetClosestLocation(PlayersTab.SelectedPlayer.transform.position).name, new GUILayoutOption[0]);
				GUILayout.TextField("Coordinates X,Y,Z:\r\n" + PlayersTab.SelectedPlayer.transform.position.ToString(), new GUILayoutOption[0]);
				GUILayout.Label("Weapon: " + ((PlayersTab.SelectedPlayer.equipment.asset != null) ? PlayersTab.SelectedPlayer.equipment.asset.itemName : "Fists"), new GUILayoutOption[0]);
				GUILayout.Label("Vehicle: " + ((PlayersTab.SelectedPlayer.movement.getVehicle() != null) ? PlayersTab.SelectedPlayer.movement.getVehicle().asset.name : "No Vehicle"), new GUILayoutOption[0]);
				GUILayout.Label("Number in a group: " + text2, new GUILayoutOption[0]);
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
		}

		// Token: 0x040000A7 RID: 167
		private static Vector2 scrollPosition1 = new Vector2(0f, 0f);

		// Token: 0x040000A8 RID: 168
		public static Vector2 PlayersScroll;

		// Token: 0x040000A9 RID: 169
		public static Player SelectedPlayer;

		// Token: 0x040000AA RID: 170
		public static string SearchString = "";
	}
}
