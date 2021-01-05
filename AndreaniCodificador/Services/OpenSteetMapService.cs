using AndreaniCodificador.RabbitMQ.Bus.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AndreaniCodificador.Services
{
    public class OpenSteetMapService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenSteetMapService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Coordinates> GetCoordinates(Geolocalizacion geo)
        {
            try
            {
                var GeolocalizadorClient = _httpClientFactory.CreateClient("OpenStreetMap");
                //GeolocalizadorClient.DefaultRequestHeaders.Add("q", $"{geo.Numero}+{geo.Calle}+{geo.Ciudad}+{geo.Codigo_postal}+{geo.Provincia}+{geo.Pais}");
                //GeolocalizadorClient.DefaultRequestHeaders.Add("format", "json");
                GeolocalizadorClient.DefaultRequestHeaders.Add("User-Agent", "C# App");
                var response = await GeolocalizadorClient.GetAsync($"?q={geo.Numero}+{geo.Calle}+{geo.Ciudad}+{geo.Codigo_postal}+{geo.Provincia}+{geo.Pais}&format=json");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //Como obtener las coordenadas del http response.
                    //var objectcontent = JsonSerializer.Deserialize<dynamic>(content);
                    OpenStreetMap jObject = JsonConvert.DeserializeObject<OpenStreetMap>(content.Substring(1, content.Length - 2));
                    var latitud = jObject.lat;
                    var longitud = jObject.lon;
                    var id = geo.Id;
                    var coordinates = new Coordinates() { Id = id, Latitud = latitud, Longitud = longitud };

                    return coordinates;
                }

                return (null);
            }
            catch (Exception ex)
            {
                return (null);
            }
        }
    }
}
