using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000005 RID: 5
	public class Manager : MonoBehaviour
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00004B60 File Offset: 0x00002D60
		public void Start()
		{
			T.DrawMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			T.DrawMaterial.SetInt("_SrcBlend", 5);
			T.DrawMaterial.SetInt("_DstBlend", 10);
			T.DrawMaterial.SetInt("_Cull", 0);
			T.DrawMaterial.SetInt("_ZWrite", 0);
			AssetUtilities.GetAssets();
			ConfigManager.Init();
			AttributeManager.Init();
		}
	}
}
