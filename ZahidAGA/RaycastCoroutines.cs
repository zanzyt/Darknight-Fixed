using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000057 RID: 87
	public class RaycastCoroutines
	{
		// Token: 0x0600020C RID: 524 RVA: 0x000037DA File Offset: 0x000019DA
		public static IEnumerator UpdateObjects()
		{
			for (;;)
			{
				if (!DrawUtilities.ShouldRun())
				{
					RaycastUtilities.Objects.Clear();
					yield return new WaitForSeconds(1f);
				}
				else
				{
					try
					{
						ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
						float num = ((itemGunAsset != null) ? itemGunAsset.range : 15.5f);
						num += 10f;
						(from c in Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, num)
							select c.gameObject).ToArray<GameObject>();
						RaycastUtilities.Objects.Clear();
						ItemGunAsset itemGunAsset2 = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
						float num2 = ((itemGunAsset2 != null) ? itemGunAsset2.range : 15.5f);
						num2 += 10f;
						GameObject[] array = (from c in Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, num2)
							select c.gameObject).ToArray<GameObject>();
						if (RaycastOptions.Enabled)
						{
							if (RaycastOptions.TargetPlayers)
							{
								RaycastCoroutines.CachedPlayers.Clear();
								GameObject[] array2 = array;
								for (int i = 0; i < array2.Length; i++)
								{
									Player player = DamageTool.getPlayer(array2[i].transform);
									if (!(player == null) && !RaycastCoroutines.CachedPlayers.Contains(player) && !(player == OptimizationVariables.MainPlayer) && !player.life.isDead)
									{
										RaycastCoroutines.CachedPlayers.Add(player);
									}
								}
								RaycastUtilities.Objects.AddRange(RaycastCoroutines.CachedPlayers.Select((Player c) => c.gameObject));
							}
							if (RaycastOptions.TargetZombies)
							{
								RaycastUtilities.Objects.AddRange(array.Where((GameObject g) => g.GetComponent<Zombie>() != null).ToArray<GameObject>());
							}
							if (RaycastOptions.TargetSentries)
							{
								RaycastUtilities.Objects.AddRange(array.Where((GameObject g) => g.GetComponent<InteractableSentry>() != null));
							}
							if (RaycastOptions.TargetBeds)
							{
								RaycastUtilities.Objects.AddRange(array.Where((GameObject g) => g.GetComponent<InteractableBed>() != null));
							}
							if (RaycastOptions.TargetAnimal)
							{
								RaycastUtilities.Objects.AddRange(array.Where((GameObject g) => g.GetComponent<Animal>() != null).ToArray<GameObject>());
							}
							if (RaycastOptions.TargetClaimFlags)
							{
								RaycastUtilities.Objects.AddRange(array.Where((GameObject g) => g.GetComponent<InteractableClaim>() != null));
							}
							if (RaycastOptions.TargetVehicles)
							{
								RaycastUtilities.Objects.AddRange(array.Where((GameObject g) => g.GetComponent<InteractableVehicle>() != null));
							}
							if (RaycastOptions.TargetStorage)
							{
								RaycastUtilities.Objects.AddRange(array.Where((GameObject g) => g.GetComponent<InteractableStorage>() != null));
							}
						}
					}
					catch (Exception)
					{
					}
					yield return new WaitForSeconds(0.1f);
				}
			}
			yield break;
		}

		// Token: 0x04000199 RID: 409
		public static List<Player> CachedPlayers = new List<Player>();
	}
}
