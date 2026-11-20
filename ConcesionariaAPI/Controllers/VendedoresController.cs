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
    [Route("api/Vendedores")]
    public class VendedoresController : ControllerBase
    {
        private readonly ConcesionariaDbContext _db;

        public VendedoresController(ConcesionariaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vendedores>>> GetVendedores()
        {
            return await _db.Vendedores.ToListAsync();
        }
    }
}
