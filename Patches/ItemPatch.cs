using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Replug.Patches
{
    internal class ItemPatch
    {
        [HarmonyPatch(typeof(ItemGun), nameof(ItemGun.ShootBullet))]
        [HarmonyPostfix]
        private static void GunVibration(CameraShake __instance)
        {
            if (Config.GunToggle.Value)
                ReplugMod.DeviceManager.VibrateAllWithDuration((float)Config.GunIntensity.Value / 20, 0.25f);
        }




    }
}
