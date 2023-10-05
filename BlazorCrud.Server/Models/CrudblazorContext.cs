using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Models;

public partial class CrudblazorContext : DbContext
{
    public CrudblazorContext()
    {
    }

    public CrudblazorContext(DbContextOptions<CrudblazorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433DAC960429");

            entity.ToTable("Departamento");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__empleado__CE6D8B9EA00068C2");

            entity.ToTable("empleado");

            entity.Property(e => e.FechaContrato).HasColumnType("date");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__empleado__IdDepa__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
