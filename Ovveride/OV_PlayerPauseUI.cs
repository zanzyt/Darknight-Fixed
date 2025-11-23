using System;
using System.Reflection;
using SDG.Unturned;

namespace ZahidAGA
{
	// Token: 0x02000095 RID: 149
	public static class OV_PlayerPauseUI
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00003E97 File Offset: 0x00002097
		[Override(typeof(PlayerPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_onClickedExitButton(ISleekElement button)
		{
			Provider.disconnect();
		}
        [Override(typeof(PlayerPauseUI), "onClickedQuitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
        public static void OV_onClickedQuitButton(ISleekElement button)
        {
			Provider.QuitGame("Why not?");
        }

        // Token: 0x060002A7 RID: 679 RVA: 0x00003E9E File Offset: 0x0000209E
        [Override(typeof(PlayerPauseUI), "onClickedSuicideButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
		public static void OV_onClickedSuicideButton(ISleekElement button)
		{
			PlayerPauseUI.closeAndGotoAppropriateHUD();
			Player.LocalPlayer.life.sendSuicide();
		}
	}
}
