using BluetoothDeviceScanner.Models;
using BluetoothDeviceScanner.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace BluetoothDeviceScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;

            DevicesListView.ItemSelected += DevicesListView_ItemSelected;
        }

        private async void DevicesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            var device = (BluetoothDeviceModel)e.SelectedItem;
            await _viewModel.ConnectToDeviceAsync(device);

            // Deselect the item after connecting.
            ((ListView)sender).SelectedItem = null;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CheckPermissionsAsync();
        }

        private async Task CheckPermissionsAsync()
        {
            var statusBluetooth = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
            var statusLocation = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (statusBluetooth != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Bluetooth>();
            }

            if (statusLocation != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
        }
    }
}