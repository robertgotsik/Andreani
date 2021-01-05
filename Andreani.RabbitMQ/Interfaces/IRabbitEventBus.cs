using Andreani.RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreani.RabbitMQ.Interfaces
{
    public interface IRabbitEventBus
    {
        void Publish(Geolocalizacion geo);

        void Consume();
    }
}
