using BepInEx;
using BepInEx.Configuration;
using System;

namespace Replug
{
    internal class Config
    {
        private static ConfigFile ConfigFile { get; set; }

        internal static ConfigEntry<string> ServerUri { get; set; }

        internal static ConfigEntry<int> HurtIntensity { get; set; }
        internal static ConfigEntry<bool> HurtToggle { get; set; }

        internal static ConfigEntry<int> HealIntensity { get; set; }
        internal static ConfigEntry<bool> HealToggle { get; set; }

        internal static ConfigEntry<int> GunIntensity { get; set; }
        internal static ConfigEntry<bool> GunToggle { get; set; }

        internal static ConfigEntry<int> CrouchStandIntensity { get; set; }
        internal static ConfigEntry<bool> CrouchStandToggle { get; set; }

        internal static ConfigEntry<int> CameraShakeIntensity { get; set; }
        internal static ConfigEntry<bool> CameraShakeToggle { get; set; }

        internal static ConfigEntry<int> ButtonHoverIntensity { get; set; }
        internal static ConfigEntry<bool> ButtonHoverToggle { get; set; }

        internal static ConfigEntry<int> ButtonSelectIntensity { get; set; }
        internal static ConfigEntry<bool> ButtonSelectToggle { get; set; }

        internal static ConfigEntry<int> DiscoverValuableIntensity { get; set; }
        internal static ConfigEntry<bool> DiscoverValuableToggle { get; set; }



        static Config()
        {
            ConfigFile = new ConfigFile(Paths.ConfigPath + "\\Replug.cfg", true);

            ServerUri = ConfigFile.Bind("Connection", "Server URI", "ws://127.0.0.1:12345", "URI for Intiface server");

            HurtIntensity = ConfigFile.Bind("Buttplug", "Hurt Intensity", 20, new ConfigDescription("Hurt intensity setting", new AcceptableValueRange<int>(0, 20)));
            HurtToggle = ConfigFile.Bind("Toggles", "Hurt vibrate toggle", true, "Hurt vibration toggle");

            HealIntensity = ConfigFile.Bind("Buttplug", "Heal Intensity", 8, new ConfigDescription("Heal intensity setting", new AcceptableValueRange<int>(0, 20)));
            HealToggle = ConfigFile.Bind("Toggles", "Heal vibrate toggle", true, "Hurt vibration toggle");

            GunIntensity = ConfigFile.Bind("Buttplug", "Gun Intensity", 12, new ConfigDescription("Gun item intensity setting", new AcceptableValueRange<int>(0, 20)));
            GunToggle = ConfigFile.Bind("Toggles", "Gun vibrate toggle", false, "Hurt vibration toggle");

            CameraShakeIntensity = ConfigFile.Bind("Buttplug", "Camera Shake Intensity", 10, new ConfigDescription("Camera shake intensity setting", new AcceptableValueRange<int>(0, 100)));
            CameraShakeToggle = ConfigFile.Bind("Toggles", "Camera shake vibrate toggle", true, "Camera vibrate toggle");

            CrouchStandIntensity = ConfigFile.Bind("Buttplug", "Crouch Stand Intensity", 1, new ConfigDescription("Button hover intensity setting", new AcceptableValueRange<int>(0, 20)));
            CrouchStandToggle = ConfigFile.Bind("Toggles", "Crouch stand vibration toggle", true);

            ButtonHoverIntensity = ConfigFile.Bind("Buttplug", "Hover Button Intensity", 1, new ConfigDescription("Button hover intensity setting", new AcceptableValueRange<int>(0, 20)));
            ButtonHoverToggle = ConfigFile.Bind("Toggles", "Hover button vibration toggle", true);

            ButtonSelectIntensity = ConfigFile.Bind("Buttplug", "Select button intensity", 1, new ConfigDescription("Button select intensity setting", new AcceptableValueRange<int>(0, 20)));
            ButtonSelectToggle = ConfigFile.Bind("Toggles", "Select button vibration toggle", true);

            DiscoverValuableIntensity = ConfigFile.Bind("Buttplug", "Discover valuable intensity", 2, new ConfigDescription("Discover valuable intensity setting", new AcceptableValueRange<int>(0, 20)));
            DiscoverValuableToggle = ConfigFile.Bind("Toggles", "Discover valuable toggle", true);
        }
    }
}