using BluetoothDeviceScanner.Services;
using BluetoothDeviceScanner.ViewModels;
using BluetoothDeviceScanner.Views;

namespace BluetoothDeviceScanner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Register BluetoothService as a singleton
            DependencyService.RegisterSingleton<IBluetoothService>(new BluetoothService());

            // Create the MainViewModel and pass the BluetoothService to it
            var mainViewModel = new MainViewModel(DependencyService.Get<IBluetoothService>());

            // Set the MainPage to a new NavigationPage with the MainPage view
            MainPage = new NavigationPage(new MainPage(mainViewModel));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}