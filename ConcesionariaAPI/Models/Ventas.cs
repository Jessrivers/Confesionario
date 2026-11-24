using System;
using System.Collections.Generic;

namespace ConcesionariaAPI.Models;

public partial class Ventas
{
    public string NoVenta { get; set; } = null!;

    public string NoSerie { get; set; } = null!;

    public string NoCliente { get; set; } = null!;

    public string NoVendedor { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public decimal Precio { get; set; }

    public virtual Clientes NoClienteNavigation { get; set; } = null!;

    public virtual Carros NoSerieNavigation { get; set; } = null!;

    public virtual Vendedores NoVendedorNavigation { get; set; } = null!;
}
