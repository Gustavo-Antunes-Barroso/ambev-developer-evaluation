using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace Ambev.DeveloperEvaluation.Queue.Producer
{
    public class SaleProducer : IRabbitMQProducer<Sale>
    {
        private static string queue = "sales.v1.prd";

        public async Task<bool> SendMessage<T>(T message, string method)
        {

            try
            {
                //*****ATTENTION*****
                //PROCESS NOT WORKING

                //TODO: Separate to reuse the settings
                var factory = new ConnectionFactory() //TODO: Get parameters from configurations
                {
                    HostName = "rabbit-server-test",
                    Port = 5672,
                    UserName = "test",
                    Password = "test"
                };

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                var connection = factory.CreateConnection();

                using (var channel = connection.CreateModel())
                {
                    IBasicProperties props = channel.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = 2;
                    props.Headers = new Dictionary<string, object>();
                    props.Headers.Add("method", method);

                    channel.QueueDeclare(queue: queue,
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null
                                            );

                    channel.BasicPublish(exchange: string.Empty,
                                            routingKey: "sales",
                                            basicProperties: props,
                                            body: body);

                }

                return true;
            }
            catch (Exception ex)
            {
                //Returning true just for test
                return true;
            }
        }
    }
}
