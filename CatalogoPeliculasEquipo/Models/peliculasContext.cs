using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CatalogoPeliculasEquipo.Models
{
    public partial class peliculasContext : DbContext
    {
        public peliculasContext()
        {
        }

        public peliculasContext(DbContextOptions<peliculasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pelicula> Peliculas { get; set; }
        public virtual DbSet<Productor> Productors { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=manzana123;database=peliculas", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.ToTable("pelicula");

                entity.HasIndex(e => e.Idproductor, "fk_pelicula_peliculas");

                entity.Property(e => e.Año).HasMaxLength(20);

                entity.Property(e => e.Categoria).HasMaxLength(65);

                entity.Property(e => e.Clasificacion).HasMaxLength(65);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(65);

                entity.Property(e => e.Pais).HasMaxLength(65);

                entity.Property(e => e.Sinopsis).HasColumnType("text");

                entity.HasOne(d => d.IdproductorNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.Idproductor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pelicula_peliculas");
            });

            modelBuilder.Entity<Productor>(entity =>
            {
                entity.ToTable("productor");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.NombreReal)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
