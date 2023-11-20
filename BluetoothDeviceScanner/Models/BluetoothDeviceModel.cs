namespace BluetoothDeviceScanner.Models
{
    public class BluetoothDeviceModel(string name, string address, int rssi, DateTime timestamp)
    {
        public string Name { get; set; } = name;
        public string Address { get; set; } = address;
        public int Rssi { get; set; } = rssi;
        public DateTime Timestamp { get; set; } = timestamp;

        public override string ToString()
        {
            return $"{Name} ({Address}) - RSSI: {Rssi} dBm";
        }
    }
}