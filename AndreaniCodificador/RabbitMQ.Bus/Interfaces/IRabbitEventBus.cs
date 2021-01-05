using AndreaniCodificador.RabbitMQ.Bus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndreaniCodificador.RabbitMQ.Interfaces
{
    public interface IRabbitEventBus
    {
        void Publish(Coordinates coor);

        void Consume();
    }
}
