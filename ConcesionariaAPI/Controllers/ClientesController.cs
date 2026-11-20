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
    [Route("api/Clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ConcesionariaDbContext _db;

        public ClientesController(ConcesionariaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Clientes>>> GetClientes()
        {
            return await _db.Clientes.ToListAsync();
        }
    }
}
