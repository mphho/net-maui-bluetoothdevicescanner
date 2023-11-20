using System.Collections.ObjectModel;
using System.Windows.Input;
using BluetoothDeviceScanner.Models;
using BluetoothDeviceScanner.Services;

namespace BluetoothDeviceScanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IBluetoothService _bluetoothService;
        private string _statusMessage;
        private bool _isScanning;

        public ObservableCollection<BluetoothDeviceModel> Devices { get; }
        public ICommand StartScanCommand { get; }
        public ICommand StopScanCommand { get; }
        public ICommand ConnectToDeviceCommand { get; }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public bool IsScanning
        {
            get => _isScanning;
            set => SetProperty(ref _isScanning, value);
        }

        public MainViewModel(IBluetoothService bluetoothService)
        {
            _bluetoothService = bluetoothService;
            Devices = new ObservableCollection<BluetoothDeviceModel>();

            StartScanCommand = new Command(async () => await StartScanningAsync());
            StopScanCommand = new Command(async () => await StopScanningAsync());
            ConnectToDeviceCommand = new Command<BluetoothDeviceModel>(async (device) => await ConnectToDeviceAsync(device));

            _bluetoothService.DeviceDiscovered += OnDeviceDiscovered;
            _bluetoothService.DeviceConnected += OnDeviceConnected;
            _bluetoothService.DeviceDisconnected += OnDeviceDisconnected;
        }

        // Starts scanning for Bluetooth devices
        private async Task StartScanningAsync()
        {
            if (!IsScanning)
            {
                Devices.Clear();
                StatusMessage = "Scanning...";
                IsScanning = true;
                await _bluetoothService.StartScanningAsync();
            }
        }

        // Stops scanning for Bluetooth devices
        private async Task StopScanningAsync()
        {
            if (IsScanning)
            {
                StatusMessage = "Stopped scanning.";
                IsScanning = false;
                await _bluetoothService.StopScanningAsync();
            }
        }

        // Connects to the selected Bluetooth device
        public async Task ConnectToDeviceAsync(BluetoothDeviceModel device)
        {
            if (device != null)
            {
                StatusMessage = $"Connecting to {device.Name}...";
                await _bluetoothService.ConnectToDeviceAsync(device);
            }
        }

        // Event handler for when a new device is discovered
        private void OnDeviceDiscovered(object sender, BluetoothDeviceModel device)
        {
            Devices.Add(device);
        }

        // Event handler for when a device is connected
        private void OnDeviceConnected(object sender, BluetoothDeviceModel device)
        {
            StatusMessage = $"Connected to {device.Name}.";
        }

        // Event handler for when a device is disconnected
        private void OnDeviceDisconnected(object sender, BluetoothDeviceModel device)
        {
            StatusMessage = $"Disconnected from {device.Name}.";
        }
    }
}