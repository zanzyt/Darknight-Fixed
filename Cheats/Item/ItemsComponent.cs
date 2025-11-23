using System;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200002A RID: 42
	[Component]
	public class ItemsComponent : MonoBehaviour
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public static void RefreshItems()
		{
			ItemsComponent.items.Clear();
			for (ushort num = 0; num < 65535; num += 1)
			{
				ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
				if (!string.IsNullOrEmpty((itemAsset != null) ? itemAsset.itemName : null) && !ItemsComponent.items.Contains(itemAsset))
				{
					ItemsComponent.items.Add(itemAsset);
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000023F2 File Offset: 0x000005F2
		public void Start()
		{
			CoroutineComponent.ItemPickupCoroutine = base.StartCoroutine(ItemCoroutines.PickupItems());
			//CoroutineComponent.ItemPickupCoroutine2 = base.StartCoroutine(ItemCoroutines2.PickupItems());
		}

		// Token: 0x040000D3 RID: 211
		public static List<ItemAsset> items = new List<ItemAsset>();
	}
}
