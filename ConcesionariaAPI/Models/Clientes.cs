using System;
using System.Collections.Generic;

namespace ConcesionariaAPI.Models;

public partial class Clientes
{
    public string NoCliente { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Correo { get; set; }

    public virtual ICollection<Ventas> Ventas { get; set; } = new List<Ventas>();
}
