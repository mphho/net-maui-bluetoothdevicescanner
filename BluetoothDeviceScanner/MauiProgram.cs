using BluetoothDeviceScanner.Services;
using BluetoothDeviceScanner.ViewModels;

namespace BluetoothDeviceScanner
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Register services here
            builder.Services.AddSingleton<IBluetoothService, BluetoothService>();
            builder.Services.AddTransient(serviceProvider => new MainViewModel(serviceProvider.GetRequiredService<IBluetoothService>()));

            return builder.Build();
        }
    }
}