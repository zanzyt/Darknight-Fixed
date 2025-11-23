using System;
using System.Reflection;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000093 RID: 147
	public class OV_PlayerInteract
	{
		// Token: 0x06000291 RID: 657 RVA: 0x000149C4 File Offset: 0x00012BC4
		[Initializer]
		public static void Init()
		{
			OV_PlayerInteract.FocusField = typeof(PlayerInteract).GetField("focus", ReflectionVariables.publicStatic);
			OV_PlayerInteract.TargetField = typeof(PlayerInteract).GetField("target", ReflectionVariables.publicStatic);
			OV_PlayerInteract.InteractableField = typeof(PlayerInteract).GetField("_interactable", ReflectionVariables.publicStatic);
			OV_PlayerInteract.Interactable2Field = typeof(PlayerInteract).GetField("_interactable2", ReflectionVariables.publicStatic);
			OV_PlayerInteract.PurchaseAssetField = typeof(PlayerInteract).GetField("purchaseAsset", ReflectionVariables.publicStatic);

		}


		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00003D37 File Offset: 0x00001F37
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00003D49 File Offset: 0x00001F49
		public static Transform focus
		{
			get
			{
				return (Transform)OV_PlayerInteract.FocusField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.FocusField.SetValue(null, value);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00003D57 File Offset: 0x00001F57
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00003D69 File Offset: 0x00001F69
		public static Transform target
		{
			get
			{
				return (Transform)OV_PlayerInteract.TargetField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.TargetField.SetValue(null, value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00003D77 File Offset: 0x00001F77
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00003D89 File Offset: 0x00001F89
		public static Interactable interactable
		{
			get
			{
				return (Interactable)OV_PlayerInteract.InteractableField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.InteractableField.SetValue(null, value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00003D97 File Offset: 0x00001F97
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00003DA9 File Offset: 0x00001FA9
		public static Interactable2 interactable2
		{
			get
			{
				return (Interactable2)OV_PlayerInteract.Interactable2Field.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.Interactable2Field.SetValue(null, value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00003DB7 File Offset: 0x00001FB7
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00003DC9 File Offset: 0x00001FC9
		public static ItemAsset purchaseAsset
		{
			get
			{
				return (ItemAsset)OV_PlayerInteract.PurchaseAssetField.GetValue(null);
			}
			set
			{
				OV_PlayerInteract.PurchaseAssetField.SetValue(null, value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00003DD7 File Offset: 0x00001FD7
		public float salvageTime
		{
			get
			{
				if (MiscOptions.CustomSalvageTime)
				{
					return MiscOptions.SalvageTime;
				}
				if (!OptimizationVariables.MainPlayer.channel.owner.isAdmin)
				{
					return 8f;
				}
				return 1f;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00003E07 File Offset: 0x00002007
		public void onPurchaseUpdated(PurchaseNode node)
		{
			if (node == null)
			{
				OV_PlayerInteract.purchaseAsset = null;
				return;
			}
			OV_PlayerInteract.purchaseAsset = (ItemAsset)Assets.find(EAssetType.ITEM, node.id);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00014A68 File Offset: 0x00012C68
		public static void highlight(Transform target, Color color)
		{
			if (!target.CompareTag("Player") || target.CompareTag("Enemy") || target.CompareTag("Zombie") || target.CompareTag("Animal") || target.CompareTag("Agent"))
			{
				Highlighter highlighter = target.GetComponent<Highlighter>();
				if (highlighter == null)
				{
					highlighter = target.gameObject.AddComponent<Highlighter>();
				}
				highlighter.ConstantOn(color, 0.25f);
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00014AE0 File Offset: 0x00012CE0

		[OnSpy]
		public static void OnSpied()
		{
			Transform transform = OptimizationVariables.MainCam.transform;
			if (transform != null) //Я фикшу.
			{
				Physics.Raycast(new Ray(transform.position, transform.forward), out OV_PlayerInteract.hit, (float)((OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? 6 : 4), RayMasks.PLAYER_INTERACT, QueryTriggerInteraction.UseGlobal);
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00014B3C File Offset: 0x00012D3C
		[Override(typeof(PlayerInteract), "Update", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
		public void OV_Update()
		{

			if (!DrawUtilities.ShouldRun())
			{
				return;
			}
			if (!OptimizationVariables.MainPlayer.life.isDead && !OptimizationVariables.MainPlayer.workzone.isBuilding)
			{
				// Only do raycasting and highlighting if NOT driving/sitting
				if (OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.DRIVING && OptimizationVariables.MainPlayer.stance.stance != EPlayerStance.SITTING)
				{
					if (Time.realtimeSinceStartup - OV_PlayerInteract.lastInteract > 0.1f)
					{
						int num = 0;
						if (InteractionOptions.InteractThroughWalls && !G.BeingSpied)
						{
							if (!InteractionOptions.NoHitBarricades)
							{
								num |= 134217728;
							}
							if (!InteractionOptions.NoHitItems)
							{
								num |= 8192;
							}
							if (!InteractionOptions.NoHitResources)
							{
								num |= 16384;
							}
							if (!InteractionOptions.NoHitStructures)
							{
								num |= 268435456;
							}
							if (!InteractionOptions.NoHitVehicles)
							{
								num |= 67108864;
							}
							if (!InteractionOptions.NoHitEnvironment)
							{
								num |= 1671168;
							}
						}
						else
						{
							num = RayMasks.PLAYER_INTERACT;
						}
						OV_PlayerInteract.lastInteract = Time.realtimeSinceStartup;
						float num2 = ((InteractionOptions.InteractThroughWalls && !G.BeingSpied) ? 20f : 4f);
						Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out OV_PlayerInteract.hit, (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.THIRD) ? (num2 + 2f) : num2, num, QueryTriggerInteraction.UseGlobal);
					}
					Transform transform = ((!(OV_PlayerInteract.hit.collider != null)) ? null : OV_PlayerInteract.hit.collider.transform);
					if (transform != OV_PlayerInteract.focus)
					{
						if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
						{
							InteractableDoor componentInParent = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoor>();
							if (componentInParent != null)
							{
								HighlighterTool.unhighlight(componentInParent.transform);
							}
							else
							{
								HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
							}
						}

						OV_PlayerInteract.focus = null;
						OV_PlayerInteract.target = null;
						OV_PlayerInteract.interactable = null;
						OV_PlayerInteract.interactable2 = null;

						if (transform != null)
						{
							OV_PlayerInteract.focus = transform;
							OV_PlayerInteract.interactable = OV_PlayerInteract.focus.GetComponentInParent<Interactable>();
							OV_PlayerInteract.interactable2 = OV_PlayerInteract.focus.GetComponentInParent<Interactable2>();

							if (PlayerInteract.interactable != null)
							{
								OV_PlayerInteract.target = PlayerInteract.interactable.transform.FindChildRecursive("Target");

								if (PlayerInteract.interactable.checkInteractable())
								{
									if (PlayerUI.window.isEnabled)
									{
										if (PlayerInteract.interactable.checkUseable())
										{
											Color green;
											if (!PlayerInteract.interactable.checkHighlight(out green))
												green = Color.green;

											InteractableDoor componentInParent2 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoor>();
											if (componentInParent2 != null)
											{
												HighlighterTool.highlight(componentInParent2.transform, green);
											}
											else
											{
												HighlighterTool.highlight(PlayerInteract.interactable.transform, green);
											}
										}
										else
										{
											Color red = Color.red;

											InteractableDoor componentInParent3 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoor>();
											if (componentInParent3 != null)
											{
												HighlighterTool.highlight(componentInParent3.transform, red);
											}
											else
											{
												HighlighterTool.highlight(PlayerInteract.interactable.transform, red);
											}
										}
									}
								}
								else
								{
									OV_PlayerInteract.target = null;
									OV_PlayerInteract.interactable = null;
								}
							}
						}
					}
					else
					{
						if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
						{
							InteractableDoor componentInParent4 = OV_PlayerInteract.focus.GetComponentInParent<InteractableDoor>();
							if (componentInParent4 != null)
							{
								HighlighterTool.unhighlight(componentInParent4.transform);
							}
							else
							{
								HighlighterTool.unhighlight(PlayerInteract.interactable.transform);
							}
						}

						OV_PlayerInteract.focus = null;
						OV_PlayerInteract.target = null;
						OV_PlayerInteract.interactable = null;
						OV_PlayerInteract.interactable2 = null;
					}
				}

				if (OptimizationVariables.MainPlayer.life.isDead)
				{
					return;
				}
				if (PlayerInteract.interactable != null)
				{
					EPlayerMessage eplayerMessage;
					string text;
					Color color;
					if (PlayerInteract.interactable.checkHint(out eplayerMessage, out text, out color) && !PlayerUI.window.showCursor)
					{
						if (PlayerInteract.interactable.CompareTag("Item"))
						{
							PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, eplayerMessage, text, color, new object[]
							{
							((InteractableItem)PlayerInteract.interactable).item,
							((InteractableItem)PlayerInteract.interactable).asset
							});
						}
						else
						{
							PlayerUI.hint((!(OV_PlayerInteract.target != null)) ? OV_PlayerInteract.focus : OV_PlayerInteract.target, eplayerMessage, text, color, new object[0]);
						}
					}
				}
				else if (OV_PlayerInteract.purchaseAsset != null && OptimizationVariables.MainPlayer.movement.purchaseNode != null && !PlayerUI.window.showCursor)
				{
					PlayerUI.hint(null, EPlayerMessage.PURCHASE, string.Empty, Color.white, new object[]
					{
					OV_PlayerInteract.purchaseAsset.itemName,
					OptimizationVariables.MainPlayer.movement.purchaseNode.cost
					});
				}
				else if (OV_PlayerInteract.focus != null && OV_PlayerInteract.focus.CompareTag("Enemy"))
				{
					Player player = DamageTool.getPlayer(OV_PlayerInteract.focus);
					if (player != null && player != Player.player && !PlayerUI.window.showCursor)
					{
						PlayerUI.hint(null, EPlayerMessage.ENEMY, string.Empty, Color.white, new object[] { player.channel.owner });
					}
				}
				EPlayerMessage eplayerMessage2;
				float num3;
				if (PlayerInteract.interactable2 != null && PlayerInteract.interactable2.checkHint(out eplayerMessage2, out num3) && !PlayerUI.window.showCursor)
				{
					PlayerUI.hint2(eplayerMessage2, (!OV_PlayerInteract.isHoldingKey) ? 0f : ((Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown) / this.salvageTime), num3);
				}
				if (Input.GetKeyDown(ControlsSettings.interact))
				{
					OV_PlayerInteract.lastKeyDown = Time.realtimeSinceStartup;
					OV_PlayerInteract.isHoldingKey = true;
				}
				if (Input.GetKeyDown(ControlsSettings.inspect) && ControlsSettings.inspect != ControlsSettings.interact && OptimizationVariables.MainPlayer.equipment.canInspect)
				{
					OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
				if (OV_PlayerInteract.isHoldingKey)
				{
					if (Input.GetKeyUp(ControlsSettings.interact))
					{
						OV_PlayerInteract.isHoldingKey = false;
						if (PlayerUI.window.showCursor)
						{
							if (OptimizationVariables.MainPlayer.inventory.isStoring && OptimizationVariables.MainPlayer.inventory.shouldInteractCloseStorage)
							{
								PlayerDashboardUI.close();
								PlayerLifeUI.open();
								return;
							}
							if (PlayerBarricadeSignUI.active)
							{
								PlayerBarricadeSignUI.close();
								PlayerLifeUI.open();
								return;
							}
							if (PlayerBarricadeLibraryUI.active)
							{
								PlayerBarricadeLibraryUI.close();
								PlayerLifeUI.open();
								return;
							}
							//if (PlayerNPCDialogueUI.active)
							//{
							//	if (PlayerNPCDialogueUI.dialogueAnimating)
							//	{
							//		PlayerNPCDialogueUI.skipText();
							//		return;
							//	}
							//	if (PlayerNPCDialogueUI.dialogueHasNextPage)
							//	{
							//		PlayerNPCDialogueUI.nextPage();
							//		return;
							//	}
							//	PlayerNPCDialogueUI.close();
							//	PlayerLifeUI.open();
							//	return;
							//}
							else
							{
								if (PlayerNPCQuestUI.active)
								{
									PlayerNPCQuestUI.closeNicely();
									return;
								}
								if (PlayerNPCVendorUI.active)
								{
									PlayerNPCVendorUI.closeNicely();
									return;
								}
							}
						}
						else
						{
							if (OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SITTING)
							{
								if (Time.realtimeSinceStartup - OV_PlayerInteract.lastExitRequest > 0.5f)
								{
									VehicleManager.exitVehicle();
									OV_PlayerInteract.lastExitRequest = Time.realtimeSinceStartup;
								}
								return;
							}
							if (OV_PlayerInteract.focus != null && PlayerInteract.interactable != null)
							{
								if (PlayerInteract.interactable.checkUseable())
								{
									PlayerInteract.interactable.use();
									return;
								}
							}
							else if (OV_PlayerInteract.purchaseAsset != null)
							{
								if (OptimizationVariables.MainPlayer.skills.experience >= OptimizationVariables.MainPlayer.movement.purchaseNode.cost)
								{
									OptimizationVariables.MainPlayer.skills.sendPurchase(OptimizationVariables.MainPlayer.movement.purchaseNode);
									return;
								}
							}
							else if (ControlsSettings.inspect == ControlsSettings.interact && OptimizationVariables.MainPlayer.equipment.canInspect)
							{
								OptimizationVariables.MainPlayer.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
								return;
							}
						}
					}
					else if (Time.realtimeSinceStartup - OV_PlayerInteract.lastKeyDown > this.salvageTime)
					{
						OV_PlayerInteract.isHoldingKey = false;
						if (!PlayerUI.window.showCursor && PlayerInteract.interactable2 != null)
						{
							PlayerInteract.interactable2.use();
						}
					}
				}
			}
		}

		// Token: 0x0400038F RID: 911
		public static FieldInfo FocusField;

		// Token: 0x04000390 RID: 912
		public static FieldInfo TargetField;

		// Token: 0x04000391 RID: 913
		public static FieldInfo InteractableField;

		// Token: 0x04000392 RID: 914
		public static FieldInfo Interactable2Field;

		// Token: 0x04000393 RID: 915
		public static FieldInfo PurchaseAssetField;

		// Token: 0x04000394 RID: 916
		public static bool isHoldingKey;

		// Token: 0x04000395 RID: 917
		public static float lastInteract;

		// Token: 0x04000396 RID: 918
		public static float lastKeyDown;

		// Token: 0x04000397 RID: 919
		public static RaycastHit hit;

		public static float lastExitRequest;
	}
}
