using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Replug.Patches
{
    internal class PlayerPatch
    {
        private static bool canVibeShake;
        

        [HarmonyPatch(typeof(PlayerHealth), nameof(PlayerHealth.Hurt))]
        [HarmonyPostfix]
        private static void VibrateOnHurt(PlayerHealth __instance)
        {
            if (__instance.invincibleTimer > 0 && !__instance.photonView.IsMine)
            {
                return;
            }
            else if (Config.HurtToggle.Value)
            {
                ReplugMod.DeviceManager.VibrateAllWithDuration((float)Config.HurtIntensity.Value / 20, 1);
            }
        }

        [HarmonyPatch(typeof(PlayerHealth), nameof(PlayerHealth.Heal))]
        [HarmonyPostfix]
        private static void VibrateOnHeal(PlayerHealth __instance)
        {
            if (__instance.invincibleTimer > 0 && !__instance.photonView.IsMine)
            {
                return;
            }
            else if (Config.HealToggle.Value)
            {
                ReplugMod.DeviceManager.VibrateAllWithDuration((float)Config.HealIntensity.Value, 0.5f);
            }
        }

        [HarmonyPatch(typeof(CameraShake), nameof(CameraShake.Update))]
        [HarmonyPostfix]
        private static void VibrateCameraShake(CameraShake __instance)
        {

            if (__instance.Strength > 4 && Config.CameraShakeToggle.Value)
            {
                canVibeShake = true;
            }
            else if (__instance.Strength < 1 && canVibeShake && Config.CameraShakeToggle.Value)
            {
                canVibeShake = false;
                ReplugMod.DeviceManager.StopAllDevices();
            }

            if (canVibeShake && !ReplugMod.DeviceManager.isVibing)
            {
                ReplugMod.DeviceManager.VibrateAllDevices((__instance.Strength * (float)Config.CameraShakeIntensity.Value) / 600);
            }
        }

        [HarmonyPatch(typeof(PlayerAvatar), nameof(PlayerAvatar.StandToCrouch))]
        [HarmonyPostfix]
        private static void VibrateOnCrouch(PlayerAvatar __instance)
        {
            if (Config.CrouchStandToggle.Value)
                ReplugMod.DeviceManager.VibrateAllWithDuration((float)Config.CrouchStandIntensity.Value / 20, 0.25f);
        }

        [HarmonyPatch(typeof(PlayerAvatar), nameof(PlayerAvatar.CrouchToStand))]
        [HarmonyPostfix]
        private static void VibrateOnStand(PlayerAvatar __instance)
        {
            if (Config.CrouchStandToggle.Value)
                ReplugMod.DeviceManager.VibrateAllWithDuration((float)Config.CrouchStandIntensity.Value / 20, 0.25f);
        }
    }
}
