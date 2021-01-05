# Andreani
Evaluacion Tecnica Andreani

Realizar un HTTP POST a /geolocalizar con el siguiente formato:
{
	"calle": "Pringles",
    "numero": "604",
    "ciudad": "Bernal",
    "codigo_postal": "1876",
    "provincia": "Buenos Aires",
    "pais": "Argentina"
}

Como respuesta recibirá un Id representando a su peticion.

Luego, puede ejecutar un GET a /geocodificar/{id} para conocer las coordenadas devuelvas por la API de One Street Map de su request anterior.

Proyecto realizado utilizando:
.Net Core 3.1
Docker
RabbitMQ
Entity Framework Core
SQL Server
