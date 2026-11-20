using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaAPI.Models;

public partial class ConcesionariaDbContext : DbContext
{
    public ConcesionariaDbContext()
    {
    }

    public ConcesionariaDbContext(DbContextOptions<ConcesionariaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carros> Carros { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Marcas> Marcas { get; set; }

    public virtual DbSet<Vendedores> Vendedores { get; set; }

    public virtual DbSet<Ventas> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=SqlServerConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carros>(entity =>
        {
            entity.HasKey(e => e.NoSerie).HasName("PK__Carros__73E977FE25F22FAC");

            entity.Property(e => e.NoSerie)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Carros)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carros_Marcas");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.NoCliente).HasName("PK__Clientes__E6378607CC0D6B32");

            entity.Property(e => e.NoCliente)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Marcas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marcas__3214EC073798D3B1");

            entity.HasIndex(e => e.Clave, "UQ__Marcas__E8181E110DE3E9F1").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Clave)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vendedores>(entity =>
        {
            entity.HasKey(e => e.NoVendedor).HasName("PK__Vendedor__92C169DDC2AD8A00");

            entity.Property(e => e.NoVendedor)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ventas>(entity =>
        {
            entity.HasKey(e => e.NoVenta).HasName("PK__Ventas__5B0B9BA96CF3EC78");

            entity.HasIndex(e => e.NoSerie, "UQ__Ventas__73E977FF2E49AAB5").IsUnique();

            entity.Property(e => e.NoVenta)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NoCliente)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NoSerie)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NoVendedor)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.NoClienteNavigation).WithMany(p => p.Ventas)
                .HasForeignKey(d => d.NoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Clientes");

            entity.HasOne(d => d.NoSerieNavigation).WithOne(p => p.Ventas)
                .HasForeignKey<Ventas>(d => d.NoSerie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Carros");

            entity.HasOne(d => d.NoVendedorNavigation).WithMany(p => p.Ventas)
                .HasForeignKey(d => d.NoVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Vendedores");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
