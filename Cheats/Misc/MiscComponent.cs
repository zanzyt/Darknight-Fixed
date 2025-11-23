using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SDG.Provider;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200002C RID: 44
	[Component]
	public class MiscComponent : MonoBehaviour
	{
		// Token: 0x06000080 RID: 128 RVA: 0x0000AE70 File Offset: 0x00009070
		[Initializer]
		public static void Initialize()
		{
			HotkeyComponent.ActionDict.Add("_Cam", delegate
			{
				if (MiscOptions.Freecam = !MiscOptions.Freecam)
				{
					T.AddNotification("Freecam <b> ON</b>");
					return;
				}
				T.AddNotification("Freecam <b> OFF</b>");
			});
			HotkeyComponent.ActionDict.Add("_Çık", delegate
			{
				Provider.disconnect();
				T.AddNotification("Disconnected");
			});
			HotkeyComponent.ActionDict.Add("_VFToggle", delegate
			{
				if (MiscOptions.VehicleFly = !MiscOptions.VehicleFly)
				{
					T.AddNotification("Vechicle Fly<b> ON</b>");
					return;
				}
				T.AddNotification("Vechicle Fly<b> OFF</b>");
			});
			HotkeyComponent.ActionDict.Add("_ToggleAimbot", delegate
			{
				AimbotOptions.Enabled = !AimbotOptions.Enabled;
			});
			HotkeyComponent.ActionDict.Add("_AimbotOnKey", delegate
			{
				AimbotOptions.OnKey = !AimbotOptions.OnKey;
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000024A9 File Offset: 0x000006A9
		[OnSpy]
		public static void Disable()
		{
			if (MiscOptions.WasNightVision)
			{
				MiscComponent.NightvisionBeforeSpy = true;
				MiscOptions.NightVision = false;
			}
			if (ESPOptions.ShowMap)
			{
				ESPOptions.ShowMap2 = true;
				ESPOptions.ShowMap = false;
			}
			if (MiscOptions.Freecam)
			{
				MiscComponent.FreecamBeforeSpy = true;
				MiscOptions.Freecam = false;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000024E4 File Offset: 0x000006E4
		[OffSpy]
		public static void Enable()
		{
			if (ESPOptions.ShowMap2)
			{
				ESPOptions.ShowMap2 = false;
				ESPOptions.ShowMap = true;
			}
			if (MiscComponent.NightvisionBeforeSpy)
			{
				MiscComponent.NightvisionBeforeSpy = false;
				MiscOptions.NightVision = true;
			}
			if (MiscComponent.FreecamBeforeSpy)
			{
				MiscComponent.FreecamBeforeSpy = false;
				MiscOptions.Freecam = true;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000AF64 File Offset: 0x00009164
		public void Start()
		{
			MiscComponent.Instance = this;
			Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(delegate
			{
				if (MiscOptions.AlwaysCheckMovementVerification)
				{
					MiscComponent.CheckMovementVerification();
					return;
				}
				MiscOptions.NoMovementVerification = false;
			}));
			SkinsUtilities.RefreshEconInfo();
			HotkeyComponent.ActionDict.Add("_Com1", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com1);
			});
			HotkeyComponent.ActionDict.Add("_Com2", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com2);
			});
			HotkeyComponent.ActionDict.Add("_Com3", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com3);
			});
			HotkeyComponent.ActionDict.Add("_Com4", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com4);
			});
			HotkeyComponent.ActionDict.Add("_Com5", delegate
			{
				ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com5);
			});
			HotkeyComponent.ActionDict.Add("_AutoPickUp", delegate
			{
				ItemOptions.AutoItemPickup = !ItemOptions.AutoItemPickup;
			});
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public void Update()
		{
			if (Camera.main != null && OptimizationVariables.MainCam == null)
			{
				OptimizationVariables.MainCam = Camera.main;
			}
			if (OptimizationVariables.MainPlayer && DrawUtilities.ShouldRun())
			{
				if (MiscOptions.hang)
				{
					Player.player.movement.pluginGravityMultiplier = -1f;
				}
				else
				{
					Player.player.movement.pluginGravityMultiplier = 1f;
				}
				int num;
				Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num);
				int num2;
				Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num2);
				int num3;
				Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num3);
				int num4;
				Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num4);
				int num5;
				Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num5);
				int num6;
				Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num6);
				if (WeaponOptions.OofOnDeath)
				{
					if (num != this.currentKills)
					{
						if (this.currentKills != -1)
						{
							OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["oof"], WeaponOptions.KillSoundVolume);
						}
						this.currentKills = num;
					}
				}
				else
				{
					this.currentKills = num;
				}
				if (WeaponOptions.Cod)
				{
					if (num2 != this.currentKills2)
					{
						if (this.currentKills2 != -1)
						{
							OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["cod"], WeaponOptions.KillSoundVolume);
						}
						this.currentKills2 = num2;
					}
				}
				else
				{
					this.currentKills2 = num2;
				}
				if (WeaponOptions.hypixe)
				{
					if (num3 != this.currentKills3)
					{
						if (this.currentKills3 != -1)
						{
							OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["hypiexl"], WeaponOptions.KillSoundVolume);
						}
						this.currentKills3 = num3;
					}
				}
				else
				{
					this.currentKills3 = num3;
				}
				if (WeaponOptions.Ez4ence)
				{
					if (num6 != this.currentKills6)
					{
						if (this.currentKills6 != -1)
						{
							OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["EZ4ANCE"], WeaponOptions.KillSoundVolume);
						}
						this.currentKills6 = num6;
					}
				}
				else
				{
					this.currentKills6 = num6;
				}
				if (WeaponOptions.coin)
				{
					if (num4 != this.currentKills4)
					{
						if (this.currentKills4 != -1)
						{
							OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["Coin"], WeaponOptions.KillSoundVolume);
						}
						this.currentKills4 = num4;
					}
				}
				else
				{
					this.currentKills4 = num4;
				}
				if (WeaponOptions.sigma)
				{
					if (num5 != this.currentKills5)
					{
						if (this.currentKills5 != -1)
						{
							OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetUtilities.AudioClip["sigma"], WeaponOptions.KillSoundVolume);
						}
						this.currentKills5 = num5;
					}
				}
				else
				{
					this.currentKills5 = num5;
				}
				int num7;
				Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out num7);
				if (MiscOptions.KillSpam)
				{
					if (num7 != this.Killer2)
					{
						if (this.Killer2 != -1)
						{
							ChatManager.sendChat(EChatMode.GLOBAL, MiscOptions.KillSpamText);
						}
						this.Killer2 = num7;
					}
				}
				else
				{
					this.Killer2 = num7;
				}
				if (MiscOptions.NightVision2)
				{
					LevelLighting.vision = ELightingVision.MILITARY;
					LevelLighting.nightvisionColor = new Color(0.078f, 0.471f, 0.314f, 1f);
					LevelLighting.nightvisionFogIntensity = 0.25f;
					LevelLighting.updateLighting();
					LevelLighting.updateLocal();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision2 = true;
				}
				else if (MiscOptions.WasNightVision2)
				{
					LevelLighting.vision = ELightingVision.NONE;
					LevelLighting.nightvisionColor = new Color(0f, 0f, 0f, 0f);
					LevelLighting.nightvisionFogIntensity = 0f;
					LevelLighting.updateLighting();
					LevelLighting.updateLocal();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision2 = false;
				}
				if (MiscOptions.NightVision3)
				{
					LevelLighting.vision = ELightingVision.HEADLAMP;
					LevelLighting.nightvisionColor = new Color(0.078f, 0f, 0f, 0f);
					LevelLighting.nightvisionFogIntensity = 0.25f;
					LevelLighting.updateLighting();
					LevelLighting.updateLocal();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision3 = true;
				}
				else if (MiscOptions.WasNightVision3)
				{
					LevelLighting.vision = ELightingVision.NONE;
					LevelLighting.nightvisionColor = new Color(0f, 0f, 0f, 0f);
					LevelLighting.nightvisionFogIntensity = 0f;
					LevelLighting.updateLighting();
					LevelLighting.updateLocal();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision3 = false;
				}
				if (MiscOptions.NightVision)
				{
					LevelLighting.vision = ELightingVision.MILITARY;
					LevelLighting.updateLighting();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision = true;
				}
				else if (MiscOptions.WasNightVision)
				{
					LevelLighting.vision = ELightingVision.NONE;
					LevelLighting.updateLighting();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision = false;
				}
				if (MiscOptions.NightVision2)
				{
					LevelLighting.vision = ELightingVision.MILITARY;
					LevelLighting.updateLighting();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision2 = true;
				}
				else if (MiscOptions.WasNightVision2)
				{
					LevelLighting.vision = ELightingVision.NONE;
					LevelLighting.updateLighting();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision2 = false;
				}
				if (MiscOptions.NightVision3)
				{
					LevelLighting.vision = ELightingVision.HEADLAMP;
					LevelLighting.updateLighting();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision3 = true;
				}
				else if (MiscOptions.WasNightVision3)
				{
					LevelLighting.vision = ELightingVision.NONE;
					LevelLighting.updateLighting();
					PlayerLifeUI.updateGrayscale();
					MiscOptions.WasNightVision3 = false;
				}
				if (OptimizationVariables.MainPlayer.life.isDead)
				{
					MiscComponent.LastDeath = OptimizationVariables.MainPlayer.transform.position;
				}
				if (MiscOptions.NoFlash && MiscOptions.NoFlash && ((Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).a > 0f)
				{
					Color color = (Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					color.a = 0f;
					typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, color);
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000B6A4 File Offset: 0x000098A4
		public void FixedUpdate()
		{
			if (OptimizationVariables.MainPlayer)
			{
				MiscComponent.VehicleFlight();
				MiscComponent.PlayerFlight();
			}
			MiscComponent.PlayerFlight();
			if (MiscOptions.RunspeedMult2 || MiscOptions.JumpMult2)
			{
				this.standSpeed.SetValue(this.sprintSpeed, MiscOptions.RunspeedMult);
				this.proneSpeed.SetValue(this.sprintSpeed, MiscOptions.RunspeedMult);
				this.sprintSpeed.SetValue(this.sprintSpeed, MiscOptions.RunspeedMult);
				this.jumpHeight.SetValue(this.jumpHeight, MiscOptions.JumpMult);
				return;
			}
			this.standSpeed.SetValue(this.sprintSpeed, MiscComponent.SPEED_STAND);
			this.proneSpeed.SetValue(this.sprintSpeed, MiscComponent.SPEED_PRONE);
			this.sprintSpeed.SetValue(this.sprintSpeed, MiscComponent.SPEED_SPRINT);
			this.jumpHeight.SetValue(this.jumpHeight, MiscComponent.JUMP);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public static void PlayerFlight()
		{
			Player mainPlayer = OptimizationVariables.MainPlayer;
			if (!MiscOptions.PlayerFlight)
			{
				ItemCloudAsset itemCloudAsset = mainPlayer.equipment.asset as ItemCloudAsset;
				mainPlayer.movement.itemGravityMultiplier = ((itemCloudAsset != null) ? itemCloudAsset.gravity : 1f);
				return;
			}
			mainPlayer.movement.itemGravityMultiplier = 0f;
			float flightSpeedMultiplier = MiscOptions.FlightSpeedMultiplier;
			if (HotkeyUtilities.IsHotkeyHeld("_FlyUp"))
			{
				mainPlayer.transform.position += mainPlayer.transform.up / 5f * flightSpeedMultiplier;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_FlyDown"))
			{
				mainPlayer.transform.position -= mainPlayer.transform.up / 5f * flightSpeedMultiplier;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_FlyLeft"))
			{
				mainPlayer.transform.position -= mainPlayer.transform.right / 5f * flightSpeedMultiplier;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_FlyRight"))
			{
				mainPlayer.transform.position += mainPlayer.transform.right / 5f * flightSpeedMultiplier;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_FlyForward"))
			{
				mainPlayer.transform.position += mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_FlyBackward"))
			{
				mainPlayer.transform.position -= mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000B988 File Offset: 0x00009B88
		public static void VehicleFlight()
		{
			InteractableVehicle vehicle = Player.player.movement.getVehicle();
			if (vehicle == null)
			{
				return;
			}
			Rigidbody component = vehicle.GetComponent<Rigidbody>();
			if (component == null)
			{
				return;
			}
			if (!vehicle.isDriver)
			{
				return;
			}
			if (!MiscOptions.VehicleFly)
			{
				if (MiscComponent.fly)
				{
					MiscComponent.fly = false;
					component.isKinematic = false;
				}
				return;
			}
			MiscComponent.fly = true;
			component.isKinematic = true;
			float num = (MiscOptions.VehicleUseMaxSpeed ? (vehicle.asset.speedMax * Time.fixedDeltaTime) : (MiscOptions.SpeedMultiplier / 3f));
			num *= 0.98f;
			Transform transform = component.transform;
			Vector3 zero = Vector3.zero;
			Vector3 vector = Vector3.zero;
			if (HotkeyUtilities.IsHotkeyHeld("_VFRotateRight"))
			{
				zero.y += 2f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFRotateLeft"))
			{
				zero.y += -2f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFRollLeft"))
			{
				zero.z += 2f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFRollRight"))
			{
				zero.z += -2f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFRotateUp"))
			{
				zero.x += -1.5f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFRotateDown"))
			{
				zero.x += 1.5f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeUp"))
			{
				vector.y += 0.6f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeDown"))
			{
				vector.y -= 0.6f;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeLeft"))
			{
				vector -= transform.right;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFStrafeRight"))
			{
				vector += transform.right;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFMoveForward"))
			{
				vector += transform.forward;
			}
			if (HotkeyUtilities.IsHotkeyHeld("_VFMoveBackward"))
			{
				vector -= transform.forward;
			}
			vector = vector * num + transform.position;
			if (MiscOptions.VehicleRigibody)
			{
				transform.position = vector;
				transform.Rotate(zero);
				return;
			}
			component.MovePosition(vector);
			component.MoveRotation(transform.localRotation * Quaternion.Euler(zero));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000251F File Offset: 0x0000071F
		public static void CheckMovementVerification()
		{
			MiscComponent.Instance.StartCoroutine(MiscComponent.CheckVerification(OptimizationVariables.MainPlayer.transform.position));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		public static void incrementStatTrackerValue(ushort itemID, int newValue)
		{
			if (Player.player == null)
			{
				return;
			}
			SteamPlayer owner = Player.player.channel.owner;
			if (owner == null)
			{
				return;
			}
			int num;
			if (!owner.getItemSkinItemDefID(itemID, out num))
			{
				return;
			}
			string text;
			string text2;
			if (!owner.getTagsAndDynamicPropsForItem(num, out text, out text2))
			{
				return;
			}
			DynamicEconDetails dynamicEconDetails = new DynamicEconDetails(text, text2);
			EStatTrackerType estatTrackerType;
			int num2;
			if (dynamicEconDetails.getStatTrackerValue(out estatTrackerType, out num2))
			{
				if (!owner.modifiedItems.Contains(itemID))
				{
					owner.modifiedItems.Add(itemID);
				}
				int i = 0;
				while (i < owner.skinItems.Length)
				{
					if (owner.skinItems[i] != num)
					{
						i++;
					}
					else
					{
						if (i < owner.skinDynamicProps.Length)
						{
							owner.skinDynamicProps[i] = dynamicEconDetails.getPredictedDynamicPropsJsonForStatTracker(estatTrackerType, newValue);
							return;
						}
						break;
					}
				}
				return;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002540 File Offset: 0x00000740
		public static IEnumerator CheckVerification(Vector3 LastPos)
		{
			if (Time.realtimeSinceStartup - MiscComponent.LastMovementCheck < 0.8f)
			{
				yield break;
			}
			MiscComponent.LastMovementCheck = Time.realtimeSinceStartup;
			OptimizationVariables.MainPlayer.transform.position = new Vector3(0f, -1337f, 0f);
			yield return new WaitForSeconds(3f);
			if (VectorUtilities.GetDistance(OptimizationVariables.MainPlayer.transform.position, LastPos) < 10.0)
			{
				MiscOptions.NoMovementVerification = false;
			}
			else
			{
				MiscOptions.NoMovementVerification = true;
				OptimizationVariables.MainPlayer.transform.position = LastPos + new Vector3(0f, 5f, 0f);
			}
			yield break;
		}

		// Token: 0x040000D8 RID: 216
		private FieldInfo standSpeed = typeof(PlayerMovement).GetField("SPEED_STAND", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000D9 RID: 217
		private FieldInfo sprintSpeed = typeof(PlayerMovement).GetField("SPEED_SPRINT", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000DA RID: 218
		private FieldInfo proneSpeed = typeof(PlayerMovement).GetField("SPEED_PRONE", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000DB RID: 219
		private FieldInfo jumpHeight = typeof(PlayerMovement).GetField("JUMP", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000DC RID: 220
		private static readonly float SPEED_STAND = 4.5f;

		// Token: 0x040000DD RID: 221
		private static readonly float SPEED_PRONE = 1.5f;

		// Token: 0x040000DE RID: 222
		private static readonly float SPEED_SPRINT = 7f;

		// Token: 0x040000DF RID: 223
		private static readonly float JUMP = 7f;

		// Token: 0x040000E0 RID: 224
		private static bool fly;

		// Token: 0x040000E1 RID: 225
		public static Vector3 LastDeath;

		// Token: 0x040000E2 RID: 226
		public static MiscComponent Instance;

		// Token: 0x040000E3 RID: 227
		public static float LastMovementCheck;

		// Token: 0x040000E4 RID: 228
		public static bool FreecamBeforeSpy;

		// Token: 0x040000E5 RID: 229
		public static bool NightvisionBeforeSpy;

		// Token: 0x040000E6 RID: 230
		public static List<PlayerInputPacket> ClientsidePackets;

		// Token: 0x040000E7 RID: 231
		public static FieldInfo Primary = typeof(PlayerEquipment).GetField("_primary", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000E8 RID: 232
		public static FieldInfo Sequence = typeof(PlayerInput).GetField("sequence", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000E9 RID: 233
		public static FieldInfo CPField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x040000EA RID: 234
		public int currentKills = -1;

		// Token: 0x040000EB RID: 235
		public int currentKills2 = -1;

		// Token: 0x040000EC RID: 236
		public int currentKills3 = -1;

		// Token: 0x040000ED RID: 237
		public int currentKills4 = -1;

		// Token: 0x040000EE RID: 238
		public int currentKills5 = -1;

		// Token: 0x040000EF RID: 239
		public int currentKills6 = -1;

		// Token: 0x040000F0 RID: 240
		public int Killer2 = -1;

		// Token: 0x040000F1 RID: 241
		public bool _isBroken;
	}
}
