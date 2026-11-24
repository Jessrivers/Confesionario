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
        private readonly ConcesionariaDbContext db;

        public VentasController(ConcesionariaDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ventas>>> GetVentas()
        {
            return await db.Ventas.ToListAsync();
        }

        [HttpGet("VentasConDatos")]
        public async Task<ActionResult<List<Ventas>>> GetVentasConDatos()
        {
            return await db
                .Ventas.AsNoTracking()
                .Include(x => x.NoSerieNavigation)
                .Include(x => x.NoClienteNavigation)
                .Include(x => x.NoVendedorNavigation)
                .ToListAsync();
        }

        [HttpGet("VentaPorNV")]
        public async Task<ActionResult<Ventas>> GetVentaPorNV(string nv)
        {
            var venta = db.Ventas.AsNoTracking().Include(c => c.NoSerieNavigation).FirstOrDefault(a => a.NoVenta == nv);
            if(venta is null)
                return NotFound();
            return venta;
        }

        [HttpPost]
        public async Task<ActionResult<Ventas>> Crear(Ventas venta)
        {
            if (!await db.Carros.AnyAsync(c => c.NoSerie == venta.NoSerie))
                return BadRequest("El carro no existe");
            if (!await db.Clientes.AnyAsync(c => c.NoCliente == venta.NoCliente))
                return BadRequest("El cliente no existe");
            if (!await db.Vendedores.AnyAsync(c => c.NoVendedor == venta.NoVendedor))
                return BadRequest("El vendedor no existe");
            if (await db.Ventas.AnyAsync(a => a.NoVenta == venta.NoVenta))
                return Conflict("Ya existe una venta con el número indicado");

            await db.Ventas.AddAsync(venta);
            await db.SaveChangesAsync();
            //return Ok();
            return CreatedAtAction("GetVentaPorNV", new {nv = venta.NoVenta}, venta);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(string nv, Ventas venta)
        {
            if (nv != venta.NoVenta)
                return BadRequest("La información no coincide");
            if (!await db.Ventas.AnyAsync(a => a.NoVenta == nv))
                return NotFound();
            if (!await db.Carros.AnyAsync(c => c.NoSerie == venta.NoSerie))
                return BadRequest("El carro no coincide");
            if (
                await db.Ventas.AnyAsync(a => a.NoVenta == venta.NoVenta && a.NoVenta != nv)
            )
                return Conflict("La información sobre el número de venta no coincide");

            db.Entry(venta).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(string nv)
        {
            var venta = await db.Ventas.FindAsync(nv);
            if (venta is null)
                return NotFound();

            db.Ventas.Remove(venta);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
