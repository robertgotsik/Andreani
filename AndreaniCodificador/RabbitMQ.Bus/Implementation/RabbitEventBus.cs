using AndreaniCodificador.RabbitMQ.Bus.Models;
using AndreaniCodificador.RabbitMQ.Interfaces;
using AndreaniCodificador.Services;
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
        private readonly OpenSteetMapService _openSteetMapService;
        public RabbitEventBus(OpenSteetMapService openSteetMapService)
        {
            _openSteetMapService = openSteetMapService;
        }
        public void Publish(Coordinates coor)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("coor-queue", false, false, false, null);

                var message = JsonConvert.SerializeObject(coor);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "coor-queue", null, body);
            }
        }

        public void Consume()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("geo-queue", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var deserializedEvent = JsonConvert.DeserializeObject<Geolocalizacion>(message);
                //Llamo al servicio que realizara el Get a Open Street Map y lo publico.
                var openStreetMapCoordinates = await _openSteetMapService.GetCoordinates(deserializedEvent);
                Publish(openStreetMapCoordinates);
            };

            channel.BasicConsume("geo-queue", true, consumer);
        }
    }
}
