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
    }
}
