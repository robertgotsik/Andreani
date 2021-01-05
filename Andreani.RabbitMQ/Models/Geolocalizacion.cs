using System;
using System.Collections.Generic;
using System.Text;

namespace Andreani.RabbitMQ.Models
{
    public class Geolocalizacion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Código_postal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public int? Estado { get; set; }
    }
}
