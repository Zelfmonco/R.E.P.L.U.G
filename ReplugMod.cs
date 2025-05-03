using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using REPOLib.Commands;
using UnityEngine;

namespace Replug;

[BepInPlugin("Zelfmonco.Replug", "Replug", "1.0")]
[BepInDependency(REPOLib.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]

public class ReplugMod : BaseUnityPlugin
{
    internal static ReplugMod Instance { get; private set; } = null!;
    public new static ManualLogSource Logger => Instance._logger;
    public ManualLogSource _logger => base.Logger;
    internal Harmony? Harmony { get; set; }
    internal static DeviceManager DeviceManager { get; private set; }


    private void Awake()
    {
        Instance = this;

        // Prevent the plugin from being deleted
        this.gameObject.transform.parent = null;
        this.gameObject.hideFlags = HideFlags.HideAndDontSave;

        DeviceManager = new DeviceManager("R.E.P.L.U.G");
        DeviceManager.Connect();

        var harmony = new Harmony(Info.Metadata.GUID);
        harmony.PatchAll(typeof(Patches.PlayerPatch));
        harmony.PatchAll(typeof(Patches.ItemPatch));
        harmony.PatchAll(typeof(Patches.MenuPatch));

        Logger.LogInfo($"{Info.Metadata.GUID} v{Info.Metadata.Version} has loaded!");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.C))
        {
            DeviceManager.Connect();
            Debug.Log("retrying connection");
        }
    }
}