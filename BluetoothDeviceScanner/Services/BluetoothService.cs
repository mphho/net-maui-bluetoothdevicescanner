using System;
using System.Threading.Tasks;
using BluetoothDeviceScanner.Models;
using BluetoothDeviceScanner.Services;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace BluetoothDeviceScanner
{
    public class BluetoothService : IBluetoothService
    {
        private readonly IAdapter _adapter;
        private readonly IBluetoothLE _bluetoothLE;

        public event EventHandler<BluetoothDeviceModel> DeviceDiscovered;
        public event EventHandler<BluetoothDeviceModel> DeviceConnected;
        public event EventHandler<BluetoothDeviceModel> DeviceDisconnected;

        public BluetoothService()
        {
            _bluetoothLE = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;

            _adapter.DeviceDiscovered += (s, e) =>
            {
                var device = new BluetoothDeviceModel(
                    e.Device.Name,
                    e.Device.Id.ToString(),
                    0, // Replace with the actual value
                    DateTime.Now // Replace with the actual value
                );

                DeviceDiscovered?.Invoke(this, device);
            };

            _adapter.DeviceConnected += (s, e) =>
            {
                var device = new BluetoothDeviceModel(
                    e.Device.Name,
                    e.Device.Id.ToString(),
                    0, // Replace with the actual value
                    DateTime.Now // Replace with the actual value
                );

                DeviceConnected?.Invoke(this, device);
            };

            _adapter.DeviceDisconnected += (s, e) =>
            {
                var device = new BluetoothDeviceModel(
                    e.Device.Name,
                    e.Device.Id.ToString(),
                    0, // Replace with the actual value
                    DateTime.Now // Replace with the actual value
                );

                DeviceDisconnected?.Invoke(this, device);
            };
        }

        public async Task StartScanningAsync()
        {
            if (!_adapter.IsScanning)
            {
                await _adapter.StartScanningForDevicesAsync();
            }
        }

        public async Task StopScanningAsync()
        {
            if (_adapter.IsScanning)
            {
                await _adapter.StopScanningForDevicesAsync();
            }
        }

        public async Task ConnectToDeviceAsync(BluetoothDeviceModel device)
        {
            if (device != null)
            {
                var bleDevice = await _adapter.ConnectToKnownDeviceAsync(Guid.Parse(device.Address));
            }
        }

        public Task DisconnectFromDeviceAsync(BluetoothDeviceModel device)
        {
            throw new NotImplementedException();
        }
    }
}