using Andreani.Context;
using Andreani.Models;
using Andreani.RabbitMQ.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andreani.RabbitMQ.Bus
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public RabbitEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish(Geolocalizacion geo)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
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
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("coor-queue", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var deserializedEvent = JsonConvert.DeserializeObject<Geolocalizacion>(message);

                //Inyectar context y Updatear datos
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = (AndreaniContext)scope.ServiceProvider.GetRequiredService<AndreaniContext>();

                                        var geo = new Geolocalizacion() { Id = deserializedEvent.Id };
                    geo.Latitud = deserializedEvent.Latitud;
                    geo.Longitud = deserializedEvent.Longitud;
                    geo.Estado = 1;
                    dbContext.Update<Geolocalizacion>(geo);
                    dbContext.SaveChanges();
                }
            };

            channel.BasicConsume("coor-queue", true, consumer);
        }
    }
}
