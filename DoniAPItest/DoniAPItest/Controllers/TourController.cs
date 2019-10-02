using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoniAPItest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoniAPItest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TourController(ApplicationDbContext context)
        {
            this.context = context;
        }


        // Devuelve una lista de todos los tours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> Get()
        {
            return await context.Tours.ToListAsync();
        }

        // Devuelve un tour especificado por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Tour>> GetById(int id)
        {
            var tour = await context.Tours.SingleOrDefaultAsync(x => x.Id == id);

            if (tour == null)
                return NotFound();

            return Ok(tour);
        }


        // Crea un tour nuevo con los valores recibidos y develve el tour creado
        [HttpPost]
        public async Task<ActionResult<Tour>> Post(Tour tour)
        {
            tour.FechaCreacion = DateTime.UtcNow;
            tour.FechaModificacion = DateTime.UtcNow;
            context.Tours.Add(tour);

            await context.SaveChangesAsync();
            return CreatedAtAction("GetById", new { id = tour.Id }, tour);
        }


        // Modifica un tour existente con los valores recibidos
        // La FechaCreacion no cambia y la FechaModificacion la pone automaticamente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Tour tour, int id)
        {
            if (tour.Id != id)
                return BadRequest();

            var OldTour = await context.Tours.SingleOrDefaultAsync(x => x.Id == id);
            OldTour.Nombre = tour.Nombre;
            OldTour.UrlImagen = tour.UrlImagen;
            OldTour.Descripcion = tour.Descripcion;
            OldTour.Observaciones = tour.Observaciones;
            OldTour.FechaModificacion = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return Ok();
        }

        // Borra un Tour coincidente con la ID especificada
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tour>> Delete(int id)
        {
            var tour = await context.Tours.SingleOrDefaultAsync(x => x.Id == id);

            if (tour == null)
                return NotFound();

            context.Tours.Remove(tour);
            await context.SaveChangesAsync();
            return Ok(tour);
        }

    }
}