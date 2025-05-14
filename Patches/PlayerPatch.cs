using HarmonyLib;
using UnityEngine;

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

        [HarmonyPatch(typeof(ValuableDiscover), nameof(ValuableDiscover.New))]
        [HarmonyPostfix]
        private static void VibrateOnDiscoverValuable(ValuableDiscover __instance, PhysGrabObject _target)
        {
            if (!Config.DiscoverValuableToggle.Value)
                return;

            float intensity;

            if (Config.DiscoverValuableScalesWithValue.Value)
            {
                if (!_target)
                    return;

                ValuableObject valuableObject = _target.transform.GetComponent<ValuableObject>();

                if (!valuableObject || valuableObject.discovered == false)
                    return;

                float itemValue = valuableObject.dollarValueCurrent;

                float minValue = Config.DiscoverValuableMinValue.Value;
                float maxValue = Config.DiscoverValuableMaxValue.Value;

                float minIntensity = Config.DiscoverValuableMinIntensity.Value;
                float maxIntensity = Config.DiscoverValuableMaxIntensity.Value;

                float t = Mathf.Clamp01((itemValue - minValue) / (maxValue - minValue));
                intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
            }
            else
            {
                intensity = Config.DiscoverValuableIntensity.Value;
            }

            ReplugMod.DeviceManager.VibrateAllWithDuration(intensity / 20f, 0.25f);
        }
    }
}
