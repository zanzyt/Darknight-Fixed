using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000092 RID: 146
	public class OV_PlayerInput
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00003D0C File Offset: 0x00001F0C
		public static List<PlayerInputPacket> ClientsidePackets
		{
			get
			{
				if (!DrawUtilities.ShouldRun() || !OV_PlayerInput.Run)
				{
					return null;
				}
				return (List<PlayerInputPacket>)OV_PlayerInput.ClientsidePacketsField.GetValue(OptimizationVariables.MainPlayer.input);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00014830 File Offset: 0x00012A30
		public static void OV_askAck(PlayerInput instance, CSteamID steamId, int ack)
		{
			if (!(steamId != Provider.server))
			{
				for (int i = OV_PlayerInput.Packets.Count - 1; i >= 0; i--)
				{
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00014864 File Offset: 0x00012A64
		public static void OV_FixedUpdate()
		{
			Player mainPlayer = OptimizationVariables.MainPlayer;
			if (MiscOptions.PunchSilentAim)
			{
				OV_DamageTool.OVType = OverrideType.PlayerHit;
			}
			DamageTool.raycast(new Ray(mainPlayer.look.aim.position, mainPlayer.look.aim.forward), 15.5f, RayMasks.DAMAGE_SERVER);
			OverrideUtilities.CallOriginal(null, new object[0]);
			List<PlayerInputPacket> clientsidePackets = OV_PlayerInput.ClientsidePackets;
			OV_PlayerInput.LastPacket = ((clientsidePackets != null) ? clientsidePackets.Last<PlayerInputPacket>() : null);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000148E0 File Offset: 0x00012AE0
		[Override(typeof(PlayerInput), "InitializePlayer", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public static void OV_InitializePlayer(PlayerInput instance)
		{
			if (instance.player != Player.player)
			{
				OverrideUtilities.CallOriginal(instance, new object[0]);
				return;
			}
			OptimizationVariables.MainPlayer = Player.player;
			OV_PlayerInput.Rate = 4;
			OV_PlayerInput.Count = 0;
			OV_PlayerInput.Buffer = 0;
			OV_PlayerInput.Packets.Clear();
			OV_PlayerInput.LastPacket = null;
			OV_PlayerInput.SequenceDiff = 0;
			OV_PlayerInput.ClientSequence = 0;
			OverrideUtilities.CallOriginal(instance, new object[0]);
		}

		// Token: 0x0400037E RID: 894
		public static FieldInfo ClientsidePacketsField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400037F RID: 895
		public static PlayerInputPacket LastPacket;

		// Token: 0x04000380 RID: 896
		public static float Yaw;

		// Token: 0x04000381 RID: 897
		public static float Pitch;

		// Token: 0x04000382 RID: 898
		public static int Count;

		// Token: 0x04000383 RID: 899
		public static int Buffer;

		// Token: 0x04000384 RID: 900
		public static int Choked;

		// Token: 0x04000385 RID: 901
		public static uint Clock = 1U;

		// Token: 0x04000386 RID: 902
		public static int Rate;

		// Token: 0x04000387 RID: 903
		public static int ClientSequence = 1;

		// Token: 0x04000388 RID: 904
		public static int SequenceDiff;

		// Token: 0x04000389 RID: 905
		public static List<PlayerInputPacket> Packets = new List<PlayerInputPacket>();

		// Token: 0x0400038A RID: 906
		public static Queue<PlayerInputPacket> WaitingPackets = new Queue<PlayerInputPacket>();

		// Token: 0x0400038B RID: 907
		public static float LastReal;

		// Token: 0x0400038C RID: 908
		public static bool Run;

		// Token: 0x0400038D RID: 909
		public static FieldInfo SimField = typeof(PlayerInput).GetField("_simulation", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400038E RID: 910
		public static Vector3 lastSentPositon = Vector3.zero;
	}
}
