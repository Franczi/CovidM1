using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Module1Registration.Services
{
    public class MessagesService
    {
        private ServiceBusClient _client;

        public MessagesService(IConfiguration configuration)
        {
            _client = new ServiceBusClient(configuration.GetConnectionString("ServiceBusConnection"));
        }

        public async Task sendMessage(MessagePayload payload)
        {
            await _client.CreateSender("messages").SendMessageAsync(new ServiceBusMessage(JsonConvert.SerializeObject(payload)));
        }

        public class MessagePayload
        {
            public string EventName { get; set; }
            public string PatientEmail { get; set; }
        }
    }
}
