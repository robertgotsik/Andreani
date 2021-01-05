using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Andreani.Models
{
    public class Geolocalizacion
    {
        public int Id { get; set; }
        [NotMapped]
        public string Calle { get; set; }
        [NotMapped]
        public string Numero { get; set; }
        [NotMapped]
        public string Ciudad { get; set; }
        [NotMapped]
        public string Codigo_postal { get; set; }
        [NotMapped]
        public string Provincia { get; set; }
        [NotMapped]
        public string Pais { get; set; }
        [MaxLength]
        public string? Latitud { get; set; }
        [MaxLength]
        public string? Longitud { get; set; }
        public int? Estado { get; set; }
    }
}