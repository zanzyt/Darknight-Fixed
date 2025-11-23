using System;
using UnityEngine;

namespace ZahidAGA
{
	// Token: 0x02000033 RID: 51
	[Component]
	public class SpectatorComponent : MonoBehaviour
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public void FixedUpdate()
		{
			if (DrawUtilities.ShouldRun())
			{
				if (MiscOptions.SpectatedPlayer != null && !G.BeingSpied)
				{
					OptimizationVariables.MainPlayer.look.IsControllingFreecam = true;
					OptimizationVariables.MainPlayer.look.orbitPosition = MiscOptions.SpectatedPlayer.transform.position - OptimizationVariables.MainPlayer.transform.position;
					OptimizationVariables.MainPlayer.look.orbitPosition += new Vector3(0f, 3f, 0f);
					return;
				}
				OptimizationVariables.MainPlayer.look.IsControllingFreecam = MiscOptions.Freecam;
			}
		}
	}
}
