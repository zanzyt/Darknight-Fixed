using System;
using UnityEngine;
using ZahidAGA;

namespace DarkNight
{
    public static class Loader
    {
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Load()
        {
            if (oko != null) return;

            oko = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(oko);
            oko.AddComponent<Manager>();
        }

        public static GameObject oko;
        public static string Name = "ExploitHub";
        public static int Gün = -1;
        public static string assett = "https://github.com/Kovalsky1243/Cheat/blob/main/DarkNight.bundle?raw=true";
        public static string appdata2 = Environment.ExpandEnvironmentVariables("%appdata%");
    }
}