using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Andreani.Context;
using Andreani.Models;
using RabbitMQ.Client;
using Newtonsoft.Json;
using Andreani.DTO;
using Andreani.RabbitMQ.Bus;
using Andreani.RabbitMQ.Interfaces;

namespace Andreani.Controllers
{
    [Route("")]
    [ApiController]
    public class GeolocalizacionsController : ControllerBase
    {
        private readonly AndreaniContext _context;
        private readonly IRabbitEventBus _bus;
        private Geocodificacion geocodificacion;

        public GeolocalizacionsController(AndreaniContext context, IRabbitEventBus bus)
        {
            _context = context;
            _bus = bus;
            geocodificacion = new Geocodificacion();
        }

        // GET: api/Geolocalizacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Geolocalizacion>>> GetGeolocalizacion()
        {
            return await _context.Geolocalizacion.ToListAsync();
        }

        // GET: api/Geolocalizacions/5
        [HttpGet("geocodificar/{id}")]
        public async Task<ActionResult<Geocodificacion>> GetGeolocalizacion(int id)
        {
            var geolocalizacion = await _context.Geolocalizacion.FindAsync(id);

            geocodificacion.Id = geolocalizacion.Id;
            geocodificacion.Latitud = geolocalizacion.Latitud;
            geocodificacion.Longitud = geolocalizacion.Longitud;
            geocodificacion.Estado = (geolocalizacion.Estado == 0) ? "PROCESANDO" : "TERMINADO";

            return geocodificacion;
        }

        // PUT: api/Geolocalizacions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeolocalizacion(int id, Geolocalizacion geolocalizacion)
        {
            if (id != geolocalizacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(geolocalizacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeolocalizacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Geolocalizacions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("geolocalizar")]
        public async Task<ActionResult<Andreani.DTO.GeolocalizacionDTO>> PostGeolocalizacion(Geolocalizacion geolocalizacion)
        {
            _context.Geolocalizacion.Add(geolocalizacion);

            await _context.SaveChangesAsync();

            _bus.Publish(geolocalizacion);

            return Accepted(new GeolocalizacionDTO { Id = geolocalizacion.Id });
            //return CreatedAtAction("GetGeolocalizacion", new GeolocalizacionDTO { Id = geolocalizacion.Id });
            //return Accepted(geolocalizacion.Id);
        }

        // DELETE: api/Geolocalizacions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Geolocalizacion>> DeleteGeolocalizacion(int id)
        {
            var geolocalizacion = await _context.Geolocalizacion.FindAsync(id);
            if (geolocalizacion == null)
            {
                return NotFound();
            }

            _context.Geolocalizacion.Remove(geolocalizacion);
            await _context.SaveChangesAsync();

            return geolocalizacion;
        }

        private bool GeolocalizacionExists(int id)
        {
            return _context.Geolocalizacion.Any(e => e.Id == id);
        }
    }
}
