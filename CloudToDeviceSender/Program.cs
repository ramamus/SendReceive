using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudToDeviceSender
{
    class Program
    {
        static string connectionString = "HostName=mtclab.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=27pCqd1J/UQmfm0RpfrvR8aXdhtSz5pQMLFylc8RHJo=";
        static ServiceClient serviceClient;
        static void Main(string[] args)
        {
            Console.WriteLine("Cloud To Device Sender");
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            Console.WriteLine("Press any key to send a C2D message");
            Console.ReadKey();
            SendCloudToDeviceMessageAsync().Wait();
            Console.WriteLine("Message Sent");
            Console.ReadKey();
        }

        private async static Task SendCloudToDeviceMessageAsync()
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes("This is satish's message"));
            await serviceClient.SendAsync("NewIoT-11-Device", commandMessage);
        }
    }
}
