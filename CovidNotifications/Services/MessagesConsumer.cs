using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CovidNotifications.Services
{
    public class MessagesConsumer
    {
        private readonly EmailService _emailService;
        private ServiceBusClient _client;
        private ServiceBusProcessor _processor;

        public MessagesConsumer(IConfiguration configuration, EmailService emailService)
        {
            _client = new ServiceBusClient(configuration.GetConnectionString("ServiceBusConnection"));
            _emailService = emailService;
        }

        public async Task Register()
        {
            _processor = _client.CreateProcessor("messages");
            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync();
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            Console.WriteLine("Error occured: "  + arg.Exception);
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            var payload=arg.Message.Body.ToObjectFromJson<MessagePayload>();
            if (payload.EventName == "PatientCreated")
            {
                _emailService.SendNewPatientEmail(payload.PatientEmail);
                await arg.CompleteMessageAsync(arg.Message);
            }
        }
    }
    public class MessagePayload
    {
        public string EventName { get; set; }
        public string PatientEmail { get; set; }
    }
}
