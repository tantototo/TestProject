using TestSendMail.Models;
using TestSendMail.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace TestSendMail.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly MessengerSettings _messengerSettings;

        public MessageServices(IOptions<MessengerSettings> options)
        {
            _messengerSettings = options.Value;
        }

        public async Task GetMessageAsync(MailRequest mailRequest, IMailServices _mailServices)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(_messengerSettings.Queue,
                    _messengerSettings.Durable,
                    _messengerSettings.Exclusive,
                    _messengerSettings.AutoDelete,
                    _messengerSettings.Arguments);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, args) =>
                {
                    var body = args.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    mailRequest.Body = message;
                    _mailServices.SendMailAsync(mailRequest);
                };

                channel.BasicConsume(queue: _messengerSettings.Queue,
                    autoAck: true, //false,
                    consumer: consumer);
            }
        }
    }
}
