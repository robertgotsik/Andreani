using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreaniCodificador.RabbitMQ.Bus.Models
{
    public class Coordinates
    {
        public int Id { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}
