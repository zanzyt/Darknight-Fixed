using System;
using System.Collections;
using SDG.Unturned;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000030 RID: 48
	[DisallowMultipleComponent]
	public class RaycastComponent : MonoBehaviour
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000026E4 File Offset: 0x000008E4
		public void Awake()
		{
			base.StartCoroutine(this.RedoSphere());
			base.StartCoroutine(this.CalcSphere());
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002700 File Offset: 0x00000900
		public IEnumerator CalcSphere()
		{
			for (;;)
			{
				yield return new WaitForSeconds(0.1f);
				if (this.Sphere)
				{
					Rigidbody component = base.gameObject.GetComponent<Rigidbody>();
					if (component)
					{
						float num = 1f - Provider.ping * component.velocity.magnitude * 2f;
						this.Sphere.transform.localScale = new Vector3(num, num, num);
					}
				}
			}
			yield break;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000270F File Offset: 0x0000090F
		public IEnumerator RedoSphere()
		{
			for (;;)
			{
				global::UnityEngine.Object sphere = this.Sphere;
				this.Sphere = IcoSphere.Create("HitSphere", SphereOptions.SpherePrediction ? 15.5f : SphereOptions.SphereRadius, (float)SphereOptions.RecursionLevel);
				this.Sphere.layer = 24;
				this.Sphere.transform.parent = base.transform;
				this.Sphere.transform.localPosition = Vector3.zero;
				global::UnityEngine.Object.Destroy(sphere);
				yield return new WaitForSeconds(1f);
			}
			yield break;
		}

		// Token: 0x04000106 RID: 262
		public GameObject Sphere;
	}
}
