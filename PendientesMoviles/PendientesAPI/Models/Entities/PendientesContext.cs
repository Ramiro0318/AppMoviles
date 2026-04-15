using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PendientesAPI.Models.Entities;

public partial class PendientesContext : DbContext
{
    public PendientesContext()
    {
    }

    public PendientesContext(DbContextOptions<PendientesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pendientes> Pendientes { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Pendientes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pendientes");

            entity.HasIndex(e => e.IdUsuario, "FK_Pendientes_Usuarios_IdUsuario");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pendiente'");
            entity.Property(e => e.Titulo).HasMaxLength(100);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pendientes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Pendientes_Usuarios_IdUsuario");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.ContraseñaHash).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
