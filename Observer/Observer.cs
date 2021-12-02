using System;
using System.Media;
using System.Linq;
using Windows.Devices.Bluetooth.Advertisement;

namespace Observer
{
    public class Observer
    {
        private readonly BluetoothLEAdvertisementWatcher _watcher;
        private const string ServiceUuid = "b501f4f4-aef5-408d-b855-b6ac744dd7d9";

        public Observer()
        {
            _watcher = new BluetoothLEAdvertisementWatcher();
        }

        public void Start()
        {
            _watcher.Received += OnAdvertisementReceived;
            _watcher.AdvertisementFilter.Advertisement.ServiceUuids.Add(new Guid(ServiceUuid));
            _watcher.Start();
        }
        
        private static void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher,
            BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            var rssi = eventArgs.RawSignalStrengthInDBm;
            var uuid = eventArgs.Advertisement.ServiceUuids.FirstOrDefault();
            
            if (rssi < -60)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ALARM: {rssi} dBm ({uuid})");
                Console.ResetColor();
                
                SystemSounds.Exclamation.Play();
            }
            else
            {
                Console.WriteLine($"{rssi} dBm ({uuid})");
            }
        }
    }
}