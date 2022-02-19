using HarmonyLib;
using System;

namespace NoSfxSound
{
    public static class Patch
    {
        public static class PlaySfxPatch
        {
            public static bool Prefix(SfxSound sound)
            {
                switch (sound)
                {
                    case SfxSound.PurePerfect:
                        switch (Main.Settings.ppSound)
                        {
                            case 0:
                                return false;
                            case 1:
                                return !RDC.auto;
                            case 2:
                                return true;
                            default:
                                return true;
                        }
                    case SfxSound.ScreenWipeIn:
                    case SfxSound.ScreenWipeOut:
                        return !Main.Settings.windSound;
                }
                return true;
            }

            public static bool LegacyPrefix(SfxSound sound, ref bool ignoreListenerPause)
            {
                switch (sound)
                {
                    case SfxSound.PurePerfect:
                        switch (Main.Settings.ppSound)
                        {
                            case 0:
                                return false;
                            case 1:
                                return !RDC.auto;
                            case 2:
                                return true;
                            default:
                                return true;
                        }
                    case SfxSound.ScreenWipeIn:
                    case SfxSound.ScreenWipeOut:
                        ignoreListenerPause = true;
                        return !Main.Settings.windSound;
                }
                return true;
            }
        }
    }
}
