using System;
using System.Collections.Generic;

namespace ConcesionariaAPI.Models;

public partial class Cuentas
{
    public int Id { get; set; }

    public string Correo { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? NoCliente { get; set; }

    public virtual Clientes? NoClienteNavigation { get; set; }
}
