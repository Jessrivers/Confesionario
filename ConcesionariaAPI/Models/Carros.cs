using System;
using System.Collections.Generic;

namespace ConcesionariaAPI.Models;

public partial class Carros
{
    public string NoSerie { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public byte IdMarca { get; set; }

    public string? Imagen { get; set; }

    public string? Color { get; set; }

    public string Tipo { get; set; } = null!;

    public short Anio { get; set; }

    public decimal Precio { get; set; }

    public byte IdSucursal { get; set; }

    public virtual Marcas IdMarcaNavigation { get; set; } = null!;

    public virtual Sucursales IdSucursalNavigation { get; set; } = null!;

    public virtual Ventas? Ventas { get; set; }
}
