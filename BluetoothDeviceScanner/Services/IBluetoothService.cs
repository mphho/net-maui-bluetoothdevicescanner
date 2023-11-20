using BluetoothDeviceScanner.Models;

namespace BluetoothDeviceScanner.Services
{
    public interface IBluetoothService
    {
        event EventHandler<BluetoothDeviceModel> DeviceDiscovered;
        event EventHandler<BluetoothDeviceModel> DeviceConnected;
        event EventHandler<BluetoothDeviceModel> DeviceDisconnected;

        Task StartScanningAsync();
        Task StopScanningAsync();
        Task ConnectToDeviceAsync(BluetoothDeviceModel device);
        Task DisconnectFromDeviceAsync(BluetoothDeviceModel device);
    }
}