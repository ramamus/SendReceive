using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    class Program
    {
        static string connectionString = "HostName=mtclab.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=27pCqd1J/UQmfm0RpfrvR8aXdhtSz5pQMLFylc8RHJo=";
        static string iotEndPoint = "messages/events";
        static EventHubClient eventHubClient;
        static void Main(string[] args)
        {
            Console.WriteLine("Receiving Messages\n");
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotEndPoint);
            var partitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            foreach (var partition in partitions)
            {
                ReceiveMessages(partition);
            }
            Console.ReadKey();
        }

        private async static Task ReceiveMessages(string partition)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.Now);
            while(true)
            {
                var eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;
                var data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message rcvd: {0} Data {1}", partition, data);
            }
        }
    }
}
