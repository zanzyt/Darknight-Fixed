using System;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x020000A2 RID: 162
	[Component]
	public class Items : MonoBehaviour
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0001735C File Offset: 0x0001555C
		private void Update()
		{
			if (MiscOptions.FToplama && Input.GetKeyDown(KeyCode.F))
			{
				InteractableItem nearestItem = Items.GetNearestItem(null);
				if (nearestItem != null)
				{
					nearestItem.use();
				}
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00017398 File Offset: 0x00015598
		public static InteractableItem GetNearestItem(int? pixelfov = null)
		{
			InteractableItem interactableItem = null;
			foreach (Collider collider in Physics.OverlapSphere(Player.player.transform.position, 19f, 8192))
			{
				if (!(collider == null) && !(collider.GetComponent<InteractableItem>() == null) && collider.GetComponent<InteractableItem>().asset != null)
				{
					InteractableItem component = collider.GetComponent<InteractableItem>();
					Vector3 vector = OptimizationVariables.MainCam.WorldToScreenPoint(component.transform.position);
					if (vector.z > 0f)
					{
						int num = (int)Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector.x, vector.y));
						if (pixelfov != null)
						{
							int num2 = num;
							int? num3 = pixelfov;
							if ((num2 > num3.GetValueOrDefault()) & (num3 != null))
							{
								goto IL_015F;
							}
						}
						if (interactableItem == null)
						{
							interactableItem = component;
						}
						else
						{
							Vector3 vector2 = OptimizationVariables.MainCam.WorldToScreenPoint(interactableItem.transform.position);
							int num4 = (int)Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector2.x, vector2.y));
							if (pixelfov != null)
							{
								int num5 = num4;
								int? num3 = pixelfov;
								if ((num5 > num3.GetValueOrDefault()) & (num3 != null))
								{
									interactableItem = null;
								}
							}
							if (num < num4)
							{
								interactableItem = component;
							}
						}
					}
				}
				IL_015F:;
			}
			return interactableItem;
		}
	}
}
