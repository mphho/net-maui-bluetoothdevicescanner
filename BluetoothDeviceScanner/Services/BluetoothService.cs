using BluetoothDeviceScanner.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace BluetoothDeviceScanner.Services
{
    public class BluetoothService : IBluetoothService
    {
        private readonly IAdapter _adapter;
        private IDevice _connectedDevice;

        public event EventHandler<BluetoothDeviceModel> DeviceDiscovered;
        public event EventHandler<BluetoothDeviceModel> DeviceConnected;
        public event EventHandler<BluetoothDeviceModel> DeviceDisconnected;

        public BluetoothService()
        {
            _adapter = CrossBluetoothLE.Current.Adapter;
            _adapter.DeviceDiscovered += OnDeviceDiscovered;
            _adapter.DeviceConnected += OnDeviceConnected;
            _adapter.DeviceDisconnected += OnDeviceDisconnected;
        }

        // Starts scanning for Bluetooth devices
        public async Task StartScanningAsync()
        {
            await _adapter.StartScanningForDevicesAsync();
        }

        // Stops scanning for Bluetooth devices
        public async Task StopScanningAsync()
        {
            await _adapter.StopScanningForDevicesAsync();
        }

        // Connects to the specified Bluetooth device
        public async Task ConnectToDeviceAsync(BluetoothDeviceModel device)
        {
            if (_connectedDevice != null)
            {
                await _adapter.DisconnectDeviceAsync(_connectedDevice);
            }

            var bleDevice = await _adapter.ConnectToKnownDeviceAsync(Guid.Parse(device.Address));
            _connectedDevice = bleDevice;
        }

        // Disconnects from the connected Bluetooth device
        public async Task DisconnectFromDeviceAsync(BluetoothDeviceModel device)
        {
            if (_connectedDevice != null)
            {
                await _adapter.DisconnectDeviceAsync(_connectedDevice);
            }
        }

        // Event handler for when a new device is discovered
        private void OnDeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs args)
        {
            var device = new BluetoothDeviceModel(args.Device.Name, args.Device.Id.ToString(), args.Device.Rssi, DateTime.Now);
            DeviceDiscovered?.Invoke(this, device);
        }

        // Event handler for when a device is connected
        private void OnDeviceConnected(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs args)
        {
            var device = new BluetoothDeviceModel(args.Device.Name, args.Device.Id.ToString(), args.Device.Rssi, DateTime.Now);
            DeviceConnected?.Invoke(this, device);
        }

        // Event handler for when a device is disconnected
        private void OnDeviceDisconnected(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs args)
        {
            var device = new BluetoothDeviceModel(args.Device.Name, args.Device.Id.ToString(), args.Device.Rssi, DateTime.Now);
            DeviceDisconnected?.Invoke(this, device);
        }
    }
}