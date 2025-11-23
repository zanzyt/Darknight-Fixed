using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000B3 RID: 179
	public static class RaycastUtilities
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00018A28 File Offset: 0x00016C28
		public static bool NoShootthroughthewalls(Transform transform)
		{
			Vector3 vector = AimbotCoroutines.GetAimPosition(transform, "Skull") - Player.LocalPlayer.look.aim.position;
			RaycastHit raycastHit;
			return Physics.Raycast(new Ray(Player.LocalPlayer.look.aim.position, vector), out raycastHit, vector.magnitude, RayMasks.DAMAGE_CLIENT, QueryTriggerInteraction.UseGlobal) && raycastHit.transform.IsChildOf(transform);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00018A9C File Offset: 0x00016C9C
		public static RaycastInfo GenerateOriginalRaycast(Ray ray, float range, int mask, Player ignorePlayer = null)
		{
			RaycastHit raycastHit;
			Physics.Raycast(ray, out raycastHit, range, mask);
			RaycastInfo raycastInfo = new RaycastInfo(raycastHit); // I take a pause from they. 
			raycastInfo.direction = ray.direction;
			raycastInfo.limb = ELimb.SPINE;
			if (raycastInfo.transform != null)
			{
				if (raycastInfo.transform.CompareTag("Barricade"))
				{
					raycastInfo.transform = DamageTool.getBarricadeRootTransform(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Structure"))
				{
					raycastInfo.transform = DamageTool.getStructureRootTransform(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Resource"))
				{
					raycastInfo.transform = DamageTool.getResourceRootTransform(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Enemy"))
				{
					raycastInfo.player = DamageTool.getPlayer(raycastInfo.transform);
					if (raycastInfo.player == ignorePlayer)
					{
						raycastInfo.player = null;
					}
					raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Zombie"))
				{
					raycastInfo.zombie = DamageTool.getZombie(raycastInfo.transform);
					raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Animal"))
				{
					raycastInfo.animal = DamageTool.getAnimal(raycastInfo.transform);
					raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
				}
				else if (raycastInfo.transform.CompareTag("Vehicle"))
				{
					raycastInfo.vehicle = DamageTool.getVehicle(raycastInfo.transform);
				}
				if (raycastInfo.zombie != null && raycastInfo.zombie.isRadioactive)
				{
					raycastInfo.materialName = "ALIEN_DYNAMIC";
				}
				else
				{
					raycastInfo.materialName = PhysicsTool.GetMaterialName(raycastHit.point, raycastInfo.transform, raycastInfo.collider);
				}
			}
			return raycastInfo;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00018C80 File Offset: 0x00016E80
		public static bool GenerateRaycast(out RaycastInfo info)
		{
			ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
			ItemMeleeAsset itemMeleeAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemMeleeAsset;
			float num;
			if (itemGunAsset != null)
			{
				num = itemGunAsset.range;
			}
			else if (itemMeleeAsset != null)
			{
				num = (MiscOptions.ExtendMeleeRange ? 15.5f : itemMeleeAsset.range);
			}
			else
			{
				num = 15.5f;
			}
			info = RaycastUtilities.GenerateOriginalRaycast(new Ray(OptimizationVariables.MainPlayer.look.aim.position, OptimizationVariables.MainPlayer.look.aim.forward), num, RayMasks.DAMAGE_CLIENT, null);
			if (RaycastOptions.EnablePlayerSelection && RaycastUtilities.TargetedPlayer != null)
			{
				GameObject gameObject = RaycastUtilities.TargetedPlayer.gameObject;
				bool flag = true;
				Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
				if (Vector3.Distance(position, gameObject.transform.position) > num)
				{
					flag = false;
				}
				Vector3 vector;
				if (!SphereUtilities.GetRaycast(gameObject, position, out vector))
				{
					flag = false;
				}
				if (flag)
				{
					info = RaycastUtilities.GenerateRaycast(gameObject, vector, info.collider);
					return true;
				}
				if (RaycastOptions.OnlyShootAtSelectedPlayer)
				{
					return false;
				}
			}
			GameObject gameObject2;
			Vector3 vector2;
			if (RaycastUtilities.GetTargetObject(RaycastUtilities.Objects, out gameObject2, out vector2, num))
			{
				info = RaycastUtilities.GenerateRaycast(gameObject2, vector2, info.collider);
				return true;
			}
			return false;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00018D98 File Offset: 0x00016F98
		public static RaycastInfo GenerateRaycast(GameObject Object, Vector3 Point, Collider col)
		{
			ELimb elimb = RaycastOptions.TargetLimb;
			if (RaycastOptions.UseRandomLimb)
			{
				ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
				elimb = array[MathUtilities.Random.Next(0, array.Length)];
			}
			return new RaycastInfo(Object.transform)
			{
				point = Point,
				direction = OptimizationVariables.MainPlayer.look.aim.forward,
				limb = elimb,
				player = Object.GetComponent<Player>(),
				zombie = Object.GetComponent<Zombie>(),
				vehicle = Object.GetComponent<InteractableVehicle>()
			};
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00018E30 File Offset: 0x00017030
		public static bool GetTargetObject(HashSet<GameObject> Objects, out GameObject Object, out Vector3 Point, float Range)
		{
			double num = (double)(Range + 1f);
			double num2 = 180.0;
			Object = null;
			Point = Vector3.zero;
			Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
			Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
			foreach (GameObject gameObject in Objects)
			{
				if (!(gameObject == null))
				{
					if (gameObject.GetComponent<RaycastComponent>() == null)
					{
						gameObject.AddComponent<RaycastComponent>();
					}
					else
					{
						Vector3 position2 = gameObject.transform.position;
						Player component = gameObject.GetComponent<Player>();
						if ((!component || (!component.life.isDead && !FriendUtilities.IsFriendly(component))) && (!WeaponOptions.SafeZone || !LevelNodes.isPointInsideSafezone(component.transform.position, out RaycastUtilities.isSafeInfo)) && (!WeaponOptions.Admin || !component.channel.owner.isAdmin))
						{
							if (AimbotOptions.HitChance != 100 && new global::System.Random().Next(0, 100) >= AimbotOptions.HitChance)
							{
								return false;
							}
							Zombie component2 = gameObject.GetComponent<Zombie>();
							if (!component2 || !component2.isDead)
							{
								double distance = VectorUtilities.GetDistance(position, position2);
								if (distance <= (double)Range)
								{
									if (RaycastOptions.SilentAimUseFOV)
									{
										double angleDelta = VectorUtilities.GetAngleDelta(position, forward, position2);
										if (angleDelta > (double)RaycastOptions.SilentAimFOV || angleDelta > num2)
										{
											continue;
										}
										num2 = angleDelta;
									}
									else if (distance > num)
									{
										continue;
									}
									Vector3 vector;
									if (SphereUtilities.GetRaycast(gameObject, position, out vector))
									{
										Object = gameObject;
										num = distance;
										Point = vector;
									}
								}
							}
						}
					}
				}
			}
			return Object != null;
		}

		// Token: 0x040003B9 RID: 953
		public static SafezoneNode isSafeInfo;

		// Token: 0x040003BA RID: 954
		public static HashSet<GameObject> Objects = new HashSet<GameObject>();

		// Token: 0x040003BB RID: 955
		public static List<GameObject> AttachedObjects = new List<GameObject>();

		// Token: 0x040003BC RID: 956
		public static Player TargetedPlayer;
	}
}
