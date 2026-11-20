using System.Linq;
using System.Threading.Tasks;
using ConcesionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaAPI.Controllers
{
    [ApiController]
    [Route("api/Carros")]
    public class CarrosController : ControllerBase
    {
        private readonly ConcesionariaDbContext db;

        public CarrosController(ConcesionariaDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Carros>>> GetCarros()
        {
            return await db.Carros.AsNoTracking().ToListAsync();
        }

        [HttpGet("CarrosConMarca")]
        public async Task<ActionResult<List<Carros>>> GetCarrosConMarca()
        {
            return await db
                .Carros.AsNoTracking()
                .Include(x => x.IdMarcaNavigation)
                .ToListAsync();
        }

        [HttpGet("CarroPorNS")]
        public async Task<ActionResult<Carros>> GetCarroPorNS(string ns)
        {
            var carro = db.Carros.AsNoTracking().Include(c => c.IdMarcaNavigation).FirstOrDefault(a => a.NoSerie == ns);
            if(carro is null)
                return NotFound();
            return carro;
        }

        [HttpPost]
        public async Task<ActionResult<Carros>> Crear(Carros carro)
        {
            if (!await db.Marcas.AnyAsync(c => c.Id == carro.IdMarca))
                return BadRequest("La marca no existe");
            if (await db.Carros.AnyAsync(a => a.NoSerie == carro.NoSerie))
                return Conflict("Ya existe un carro con el número de serie indicado");

            await db.Carros.AddAsync(carro);
            await db.SaveChangesAsync();
            //return Ok();
            return CreatedAtAction("GetCarroPorNS", new { ns = carro.NoSerie }, carro);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(string ns, Carros carro)
        {
            if (ns != carro.NoSerie)
                return BadRequest("La información no coincide");

            if (!await db.Carros.AnyAsync(a => a.NoSerie == ns))
                return NotFound();

            if (!await db.Marcas.AnyAsync(c => c.Id == carro.IdMarca))
                return BadRequest("La marca no existe");

            if (await db.Carros.AnyAsync(a => a.NoSerie == carro.NoSerie && a.NoSerie != ns))
                return Conflict("La información sobre el número de serie no coincide");

            db.Entry(carro).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(string ns)
        {
            var carro = await db.Carros.FindAsync(ns);

            if (carro is null)
                return NotFound();

            db.Carros.Remove(carro);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
