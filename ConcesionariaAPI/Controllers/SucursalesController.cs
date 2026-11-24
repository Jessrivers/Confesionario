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
    [Route("api/Sucursales")]
    public class SucursalesController : ControllerBase
    {
        private readonly ConcesionariaDbContext db;

        public SucursalesController(ConcesionariaDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sucursales>>> GetSucursales()
        {
            return await db.Sucursales.ToListAsync();
        }

        [HttpGet("SucursalPorId")]
        public async Task<ActionResult<Sucursales>> GetSucursalPorId(byte id)
        {
            var sucursal = db.Sucursales.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(sucursal is null)
                return NotFound();
            return sucursal;
        }

        [HttpPost]
        public async Task<ActionResult<Sucursales>> Crear(Sucursales sucursal)
        {
            await db.Sucursales.AddAsync(sucursal);
            await db.SaveChangesAsync();
            //return Ok();
            return CreatedAtAction("GetSucursalPorId", new {id = sucursal.Id}, sucursal);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(byte id, Sucursales sucursal)
        {
            if (id != sucursal.Id)
                return BadRequest("La información no coincide");
            if (!await db.Sucursales.AnyAsync(a => a.Id == id))
                return NotFound();

            db.Entry(sucursal).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(byte id)
        {
            var sucursal = await db.Sucursales.FindAsync(id);
            if (sucursal is null)
                return NotFound();

            db.Sucursales.Remove(sucursal);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
