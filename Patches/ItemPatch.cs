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
                ReplugMod.DeviceManager.VibrateAllWithDuration(Config.GunIntensity.Value * 5 / 100, 0.25f);
        }




    }
}
