using System;
using System.Collections;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200004E RID: 78
	public static class ItemCoroutines2
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00003744 File Offset: 0x00001944
		public static IEnumerator PickupItems()
		{
			for (;;)
			{
				if (!DrawUtilities.ShouldRun() || !ItemOptions.AutoItemPickupFiltresiz)
				{
					yield return new WaitForSeconds(0.5f);
				}
				else
				{
					Collider[] array = Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, (float)ItemOptions.ItemPickupDelayFiltresizRange, 8192);
					int num;
					for (int i = 0; i < array.Length; i = num + 1)
					{
						Collider collider = array[i];
						if (!(collider == null) && !(collider.GetComponent<InteractableItem>() == null) && collider.GetComponent<InteractableItem>().asset != null)
						{
							InteractableItem component = collider.GetComponent<InteractableItem>();
							ItemUtilities.Whitelisted(component.asset, ItemOptions.ItemFilterOptions);
							component.use();
						}
						num = i;
					}
					yield return new WaitForSeconds(ItemOptions.ItemPickupDelayFiltresizDelay);
				}
			}
			yield break;
		}
	}
}
