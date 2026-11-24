using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcesionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaAPI.Controllers
{
    [ApiController]
    [Route("api/Marcas")]
    public class MarcasController : ControllerBase
    {
        private readonly ConcesionariaDbContext _db;

        public MarcasController(ConcesionariaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Marcas>>> GetMarcas()
        {
            return await _db.Marcas.ToListAsync();
        }

        [HttpPost("SubirLogo")]
        public async Task<IActionResult> SubirLogo(byte id, IFormFile archivo)
        {
            var marca = await _db.Marcas.FindAsync(id);
            if (marca is null)
                return NotFound();

            Directory.CreateDirectory("wwwroot/imagenes");
            var ruta = "imagenes/" + marca.Clave + ".jpg";
            using (var stream = System.IO.File.Create("wwwroot/" + ruta))
                await archivo.CopyToAsync(stream);

            marca.Logo = ruta;
            await _db.SaveChangesAsync();
            return Ok(marca.Logo);
        }
    }
}
