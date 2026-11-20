using System;
using System.Collections.Generic;

namespace ConcesionariaAPI.Models;

public partial class Carros
{
    public string NoSerie { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public byte IdMarca { get; set; }

    public virtual Marcas? IdMarcaNavigation { get; set; } = null!;

    public virtual Ventas? Ventas { get; set; }
}
