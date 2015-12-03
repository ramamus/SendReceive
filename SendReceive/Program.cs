using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading;

namespace SendReceive
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "mtclab.azure-devices.net";
        static string deviceKey = "aPSbArrBgEu/GL3+9/AiaOZRrNY2oxDWS7ZSy5kERy4=";

        static void Main(string[] args)
        {
            Console.WriteLine("SimulatedDevice\n");
            deviceClient = DeviceClient.Create(iotHubUri,
                new DeviceAuthenticationWithRegistrySymmetricKey("NewIoT-11-Device", deviceKey)
                );
            SendMessageAsync();
            Console.ReadKey();
        }

        private static async void SendMessageAsync()
        {
            double avgWindSpeed = 10;
            Random rand = new Random();

            while(true)
            {
                var currentWindSpeed = avgWindSpeed + rand.NextDouble() * 4 + 2;
                var telemetry = new
                {
                    deviceId = "NewIoT-11-Device",
                    windSpeed = currentWindSpeed
                };
                var json = JsonConvert.SerializeObject(telemetry);
                var msg = new Message(Encoding.ASCII.GetBytes(json));

                await deviceClient.SendEventAsync(msg);
                Console.WriteLine("{0} > Sending Message: {1}", DateTime.Now, json);
                Thread.Sleep(1000);
            }
        }
    }
}
