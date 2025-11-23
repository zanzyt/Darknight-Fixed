using System;
using System.Collections;
using System.Collections.Generic;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x0200005B RID: 91
	public static class TrajectoryCoroutines
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00003892 File Offset: 0x00001A92
		public static IEnumerator UpdateBodiesInMotionSet()
		{
			for (;;)
			{
				if (!DrawUtilities.ShouldRun() || !WeaponOptions.EnableBulletDropPrediction)
				{
					yield return new WaitForSeconds(1f);
				}
				else
				{
					TransformEx.FindAllChildrenWithName(Level.effects, "Projectile", TrajectoryCoroutines.gameObjectOut);
					TransformEx.FindAllChildrenWithName(Level.effects, "Throwable", TrajectoryCoroutines.gameObjectOut);
					foreach (GameObject gameObject in TrajectoryCoroutines.gameObjectOut)
					{
						Rigidbody component = gameObject.GetComponent<Rigidbody>();
						Vector3? vector = ((component != null) ? new Vector3?(component.velocity) : null);
						Vector3 zero = Vector3.zero;
						if (vector == null || (vector != null && !(vector.GetValueOrDefault() == zero)))
						{
							if (gameObject.name == "Projectile")
							{
								TrajectoryComponent.BodiesInMotion.Add(gameObject);
							}
							else if (gameObject.name == "Throwable")
							{
								if (gameObject.GetComponent<StickyGrenade>() != null)
								{
									Rigidbody component2 = gameObject.GetComponent<Rigidbody>();
									if (component2 != null && !component2.useGravity)
									{
										continue;
									}
								}
								TrajectoryComponent.BodiesInMotion.Add(gameObject);
							}
						}
					}
					TrajectoryCoroutines.gameObjectOut.Clear();
					yield return new WaitForSeconds(0.2f);
				}
			}
			yield break;
		}

		// Token: 0x040001A9 RID: 425
		private static readonly List<GameObject> gameObjectOut = new List<GameObject>();
	}
}
