using System;
using System.Collections.Generic;

namespace ConcesionariaAPI.Models;

public partial class Sucursales
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public virtual ICollection<Carros> Carros { get; set; } = new List<Carros>();
}
