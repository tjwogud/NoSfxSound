using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;

namespace NoSfxSound
{
    public static class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony harmony;
        public static bool IsEnabled = false;
        public static Settings Settings;

        public static void Setup(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            Logger.Log("Loading Settings...");
            Settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
            Logger.Log("Load Completed!");
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            IsEnabled = value;
            if (value)
            {
                harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                harmony.UnpatchAll(modEntry.Info.Id);
            }
            return true;
        }

        private static string[] kr_texts1 = new string[] { "항상", "오토 시", "하지 않음" };
        private static string[] en_texts1 = new string[] { "Always", "Only Auto", "Never" };
        private static string[] kr_texts2 = new string[] { "항상", "하지 않음" };
        private static string[] en_texts2 = new string[] { "Always", "Never" };

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.Label(RDString.language == SystemLanguage.Korean ?
                "완벽한 플레이 소리 비활성화" :
                "Disable Pure Perfect Sound");
            Settings.ppSound = GUILayout.Toolbar(Settings.ppSound,
                RDString.language == SystemLanguage.Korean ? kr_texts1 : en_texts1,
                GUILayout.Width(270));
            GUILayout.Space(10);
            GUILayout.Label(RDString.language == SystemLanguage.Korean ?
                "화면 전환 시 바람소리 비활성화" :
                "Disable Wind Sound When Wipe Screen");
            Settings.windSound = GUILayout.Toolbar(Settings.windSound ? 0 : 1,
                RDString.language == SystemLanguage.Korean ? kr_texts2 : en_texts2,
                GUILayout.Width(180)) == 0;
            GUILayout.Space(10);
            GUILayout.Label(RDString.language == SystemLanguage.Korean ?
                "죽을 시 소리 비활성화" :
                "Disable Death Sound");
            GCS.playDeathSound = GUILayout.Toolbar(!GCS.playDeathSound ? 0 : 1,
                RDString.language == SystemLanguage.Korean ? kr_texts2 : en_texts2,
                GUILayout.Width(180)) != 0;
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Logger.Log("Saving Settings...");
            Settings.Save(modEntry);
            Logger.Log("Save Completed!");
        }
    }
}