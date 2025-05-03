using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace Replug.Patches
{
    internal class MenuPatch
    {
        [HarmonyPatch(typeof(MenuButton), nameof(MenuButton.OnHoverStart))]
        [HarmonyPostfix]
        private static void HoverVibrate(MenuButton __instance)
        {
            if (Config.ButtonHoverToggle.Value)
                ReplugMod.DeviceManager.VibrateAllWithDuration((float)Config.ButtonHoverIntensity.Value / 20, 0.1f);
        }

        [HarmonyPatch(typeof(MenuButton), nameof(MenuButton.OnSelectEnd))]
        [HarmonyPostfix]
        private static void SelectVibrate(MenuButton __instance)
        {
            if (Config.ButtonSelectToggle.Value)
                ReplugMod.DeviceManager.VibrateAllWithDuration((float)Config.ButtonSelectIntensity.Value / 20, 0.1f);
        }
    }
}
