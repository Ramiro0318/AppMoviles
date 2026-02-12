using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using RecetasAPI.Models.Entities;

namespace RecetasAPI.Data;

public partial class GourmetRecetasContext : DbContext
{
    public GourmetRecetasContext()
    {
    }

    public GourmetRecetasContext(DbContextOptions<GourmetRecetasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorias> Categorias { get; set; }

    public virtual DbSet<Ingredientes> Ingredientes { get; set; }

    public virtual DbSet<Pasospreparacion> Pasospreparacion { get; set; }

    public virtual DbSet<Recetas> Recetas { get; set; }

    public virtual DbSet<Tips> Tips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=gourmet_recetas;password=root;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.0.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categorias>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.Tipo, "idx_tipo");

            entity.HasIndex(e => e.UrlAmigable, "idx_url_amigable").IsUnique();

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasColumnType("enum('tipo_plato','ingrediente_principal','cocina','momento_dia','ocasion')")
                .HasColumnName("tipo");
            entity.Property(e => e.UrlAmigable)
                .HasMaxLength(100)
                .HasColumnName("url_amigable");
        });

        modelBuilder.Entity<Ingredientes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ingredientes");

            entity.HasIndex(e => e.RecetaId, "idx_receta_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IngredienteTexto)
                .HasColumnType("text")
                .HasColumnName("ingrediente_texto");
            entity.Property(e => e.Orden)
                .HasDefaultValueSql("'0'")
                .HasColumnName("orden");
            entity.Property(e => e.RecetaId)
                .HasMaxLength(100)
                .HasColumnName("receta_id");
            entity.Property(e => e.Seccion)
                .HasMaxLength(100)
                .HasColumnName("seccion");

            entity.HasOne(d => d.Receta).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.RecetaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ingredientes_ibfk_1");
        });

        modelBuilder.Entity<Pasospreparacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pasospreparacion");

            entity.HasIndex(e => e.RecetaId, "idx_receta_id");

            entity.HasIndex(e => new { e.RecetaId, e.NumeroPaso, e.Seccion }, "uk_receta_paso").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.NumeroPaso).HasColumnName("numero_paso");
            entity.Property(e => e.RecetaId)
                .HasMaxLength(100)
                .HasColumnName("receta_id");
            entity.Property(e => e.Seccion)
                .HasMaxLength(100)
                .HasColumnName("seccion");

            entity.HasOne(d => d.Receta).WithMany(p => p.Pasospreparacion)
                .HasForeignKey(d => d.RecetaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("pasospreparacion_ibfk_1");
        });

        modelBuilder.Entity<Recetas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recetas");

            entity.HasIndex(e => e.DatabaseId, "database_id").IsUnique();

            entity.HasIndex(e => e.UrlAmigable, "idx_url_amigable").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Beneficios)
                .HasColumnType("text")
                .HasColumnName("beneficios");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DatabaseId).HasColumnName("database_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.FechaPublicacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_publicacion");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(500)
                .HasColumnName("imagen_url");
            entity.Property(e => e.Porcion)
                .HasMaxLength(100)
                .HasColumnName("porcion");
            entity.Property(e => e.Tiempo)
                .HasMaxLength(100)
                .HasColumnName("tiempo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
            entity.Property(e => e.UrlAmigable).HasColumnName("url_amigable");

            entity.HasMany(d => d.Categoria).WithMany(p => p.Receta)
                .UsingEntity<Dictionary<string, object>>(
                    "Recetacategorias",
                    r => r.HasOne<Categorias>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("recetacategorias_ibfk_2"),
                    l => l.HasOne<Recetas>().WithMany()
                        .HasForeignKey("RecetaId")
                        .HasConstraintName("recetacategorias_ibfk_1"),
                    j =>
                    {
                        j.HasKey("RecetaId", "CategoriaId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("recetacategorias");
                        j.HasIndex(new[] { "CategoriaId" }, "idx_categoria_id");
                        j.HasIndex(new[] { "RecetaId" }, "idx_receta_id");
                        j.IndexerProperty<string>("RecetaId")
                            .HasMaxLength(100)
                            .HasColumnName("receta_id");
                        j.IndexerProperty<int>("CategoriaId").HasColumnName("categoria_id");
                    });
        });

        modelBuilder.Entity<Tips>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tips");

            entity.HasIndex(e => e.RecetaId, "idx_receta_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.RecetaId)
                .HasMaxLength(100)
                .HasColumnName("receta_id");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Receta).WithMany(p => p.Tips)
                .HasForeignKey(d => d.RecetaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tips_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
