using Andreani.Context;
using Andreani.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreani.RabbitMQ.Interfaces
{
    public interface IRabbitEventBus
    {
        //void Initialize(AndreaniContext context);
        void Publish(Geolocalizacion geo);

        void Consume();
    }
}
