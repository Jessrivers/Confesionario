using System;
using System.Collections.Generic;

namespace ConcesionariaAPI.Models;

public partial class Marcas
{
    public byte Id { get; set; }

    public string Clave { get; set; } = null!;

    public string NombreMarca { get; set; } = null!;

    public string? Logo { get; set; }

    public virtual ICollection<Carros> Carros { get; set; } = new List<Carros>();
}
