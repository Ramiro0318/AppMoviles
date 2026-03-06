using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AsistenciasAPI.Models.Entities;

public partial class RegistroasistenciaContext : DbContext
{
    public RegistroasistenciaContext()
    {
    }

    public RegistroasistenciaContext(DbContextOptions<RegistroasistenciaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumno { get; set; }

    public virtual DbSet<Asistencia> Asistencia { get; set; }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<Grupo> Grupo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=registroasistencia;password=root;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.0.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alumno");

            entity.HasIndex(e => e.NumControl, "NumControl").IsUnique();

            entity.HasIndex(e => e.IdGrupo, "fk_alumno_grupo");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NumControl)
                .HasMaxLength(8)
                .IsFixedLength();

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Alumno)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_alumno_grupo");
        });

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asistencia");

            entity.HasIndex(e => e.IdAlumno, "fk_asistencia_alumno");

            entity.HasIndex(e => e.IdEstado, "fk_asistencia_estado");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Motivo).HasMaxLength(150);

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("fk_asistencia_alumno");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asistencia_estado");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estado");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("grupo");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
