using Andreani.RabbitMQ.Interfaces;
using Andreani.RabbitMQ.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreaniCodificador.RabbitMQ.Bus
{
    public class RabbitEventBus : IRabbitEventBus
    {
        public void Publish(Geolocalizacion geo)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq-web" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("geo-queue", false, false, false, null);

                var message = JsonConvert.SerializeObject(geo);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "geo-queue", null, body);
            }
        }

        public void Consume()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq-web" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("geo-queue", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var deserializedEvent = JsonConvert.DeserializeObject<Geolocalizacion>(message);
            };

            channel.BasicConsume("geo-queue", true, consumer);
        }
    }
}
