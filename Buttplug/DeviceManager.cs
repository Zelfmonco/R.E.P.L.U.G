using Buttplug.Client;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Replug
{
    public class DeviceManager
    {
        private List<ButtplugClientDevice> ConnectedDevices {  get; set; }
        private ButtplugClient ButtplugClient { get; set; }

        public DeviceManager(string clientName)
        {
            ConnectedDevices = new List<ButtplugClientDevice>();
            ButtplugClient = new ButtplugClient(clientName);

            ButtplugClient.DeviceAdded += HandleDeviceAdded;
            ButtplugClient.DeviceRemoved += HandleDeviceRemoved;
        }

        public bool Connected() => ButtplugClient.Connected;

        public async void Connect()
        {
            try
            {
                await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri(Config.ServerUri.Value)));

                await ButtplugClient.StartScanningAsync();
            }
            catch (ButtplugClientConnectorException ex)
            {
                ReplugMod.Logger.LogError(ex);
            }
        }

        private void HandleDeviceAdded(object sender, DeviceAddedEventArgs args)
        {
            if (!IsVibratable(args.Device))
            {
                return;
            }

            ReplugMod.Logger.LogInfo($"{args.Device.Name} connected to {ButtplugClient.Name}");
            ConnectedDevices.Add(args.Device);
        }

        private void HandleDeviceRemoved(object sender, DeviceRemovedEventArgs args)
        {
            if (!IsVibratable(args.Device))
            {
                return;
            }

            ReplugMod.Logger.LogInfo($"{args.Device.Name} disconnected from {ButtplugClient.Name}");
            ConnectedDevices.Remove(args.Device);
        }

        public void VibrateAllWithDuration(double intensity, float time)
        {
            intensity *= Config.VibeAmp.Value;

            async void Repeat(ButtplugClientDevice device)
            {
                await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
                await Task.Delay((int)(time * 1000f));
                await device.VibrateAsync(0.0f);
            }
            ConnectedDevices.ForEach(Repeat);
        }

        public void VibrateAllDevices(double intensity)
        {
            intensity *= Config.VibeAmp.Value;

            async void Repeat(ButtplugClientDevice device)
            {
                await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
            }

            ConnectedDevices.ForEach(Repeat);
        }

        private bool IsVibratable(ButtplugClientDevice device)
        {
            return device.VibrateAttributes.Count > 0;
        }

        public void StopAllDevices()
        {
            ConnectedDevices.ForEach(async (ButtplugClientDevice device) => await device.Stop());
        }
    }
}
