using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000027 RID: 39
	[Component]
	public class AimbotComponent : MonoBehaviour
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002381 File Offset: 0x00000581
		public void Start()
		{
			CoroutineComponent.LockCoroutine = base.StartCoroutine(AimbotCoroutines.SetLockedObject());
			CoroutineComponent.AimbotCoroutine = base.StartCoroutine(AimbotCoroutines.AimToObject());
			base.StartCoroutine(RaycastCoroutines.UpdateObjects());
		}
	}
}
