using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcesionariaAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaAPI.Controllers
{
    public class LoginDatos
    {
        public string Correo { get; set; } = "";
        public string Password { get; set; } = "";
    }

    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly ConcesionariaDbContext db;
        private readonly PasswordHasher<Cuentas> hasher = new PasswordHasher<Cuentas>();

        public AuthController(ConcesionariaDbContext db)
        {
            this.db = db;
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Registro(LoginDatos datos)
        {
            if (datos.Password.Length < 8)
                return BadRequest("La contraseña debe tener mínimo 8 caracteres");
            if (await db.Cuentas.AnyAsync(c => c.Correo == datos.Correo))
                return Conflict("Ese correo ya tiene cuenta");

            // el número de cliente nuevo es el total de clientes + 1 (CLI0000003, CLI0000004...)
            var total = await db.Clientes.CountAsync();
            var cliente = new Clientes
            {
                NoCliente = "CLI" + (total + 1).ToString("D7"),
                Nombre = "",
                Apellidos = "",
                Correo = datos.Correo
            };

            var cuenta = new Cuentas
            {
                Correo = datos.Correo,
                Rol = "Cliente",
                NoCliente = cliente.NoCliente
            };
            cuenta.PasswordHash = hasher.HashPassword(cuenta, datos.Password);

            await db.Clientes.AddAsync(cliente);
            await db.Cuentas.AddAsync(cuenta);
            await db.SaveChangesAsync();
            return Ok(new { cuenta.Correo, cuenta.Rol, cuenta.NoCliente });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDatos datos)
        {
            var cuenta = await db.Cuentas.FirstOrDefaultAsync(c => c.Correo == datos.Correo);
            if (cuenta is null)
                return Unauthorized("Correo o contraseña incorrectos");

            var resultado = hasher.VerifyHashedPassword(cuenta, cuenta.PasswordHash, datos.Password);
            if (resultado == PasswordVerificationResult.Failed)
                return Unauthorized("Correo o contraseña incorrectos");

            return Ok(new { cuenta.Correo, cuenta.Rol, cuenta.NoCliente });
        }
    }
}
