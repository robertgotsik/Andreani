using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreaniCodificador.RabbitMQ.Bus.Models
{
    public class OpenStreetMap
    {
        //public int id { get; set; }
        public string place_id { get;set;}
        public string licence { get;set;}
        public string osm_type { get;set;}
        public string osm_id { get;set;}
        private readonly List<string> _boundingbox = new List<string>();
        public IList<string> boundingbox { get { return _boundingbox; } }
        public string lat { get;set;}
        public string lon { get;set;}
        public string display_name { get;set;}
        public string type { get;set;}
        public string importance { get;set;}

    }
}