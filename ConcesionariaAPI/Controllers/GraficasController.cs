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
    [Route("api/Graficas")]
    public class GraficasController : ControllerBase
    {
        private readonly ConcesionariaDbContext _db;

        public GraficasController(ConcesionariaDbContext db)
        {
            _db = db;
        }

        [HttpGet("CarrosPorTipo")]
        public async Task<IActionResult> GetCarrosPorTipo()
        {
            var carros = await _db.Carros.CountAsync(c => c.Tipo == "Carro");
            var camionetas = await _db.Carros.CountAsync(c => c.Tipo == "Camioneta");
            return Ok(new { carros, camionetas });
        }

        [HttpGet("CarrosPorMarca")]
        public async Task<IActionResult> GetCarrosPorMarca()
        {
            var datos = await _db.Marcas.Select(m => new { m.NombreMarca, total = m.Carros.Count }).ToListAsync();
            return Ok(datos);
        }
    }
}
