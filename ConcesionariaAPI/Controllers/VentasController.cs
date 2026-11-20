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
    [Route("api/Ventas")]
    public class VentasController : ControllerBase
    {
        private readonly ConcesionariaDbContext _db;

        public VentasController(ConcesionariaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ventas>>> GetVentas()
        {
            return await _db.Ventas.ToListAsync();
        }
    }
}
