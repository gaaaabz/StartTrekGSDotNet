using Microsoft.EntityFrameworkCore;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

      
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }    
        public DbSet<Esp32> Esp32 { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Trabalho> Trabalhos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==== T_ST_TIPO_USUARIO ====
            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.ToTable("T_ST_TIPO_USUARIO");

                entity.HasKey(e => e.IdTipoUsuario);

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnName("id_tipo_usuario");

                entity.Property(e => e.NomeTipoUsuario)
                    .HasColumnName("nm_tipo_usuario")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            // ==== T_ST_ESP32 ====
            modelBuilder.Entity<Esp32>(entity =>
            {
                entity.ToTable("T_ST_ESP32");

                entity.HasKey(e => e.IdEsp32);

                entity.Property(e => e.IdEsp32)
                    .HasColumnName("id_esp32");
            });

            // ==== T_ST_USUARIO ====
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("T_ST_USUARIO");

                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario");

                entity.Property(e => e.NomeUsuario)
                    .HasColumnName("nm_usuario")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.Senha)
                    .HasColumnName("senha")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.Foto)
                    .HasColumnName("foto");

                // bool <-> CHAR(1)
                entity.Property(e => e.Ativo)
                    .HasColumnName("at_usuario")
                    .HasMaxLength(1)
                    .HasConversion(
                        v => v ? "1" : "0",
                        v => v == "1"
                    );

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnName("id_tipo_usuario");

                entity.Property(e => e.IdEsp32)
                    .HasColumnName("id_esp32");

                entity.HasOne(e => e.TipoUsuario)
                    .WithMany(t => t.Usuarios)
                    .HasForeignKey(e => e.IdTipoUsuario)
                    .HasConstraintName("fk_usuario_tipo");

                entity.HasOne(e => e.Esp32)
                    .WithMany(e2 => e2.Usuarios)
                    .HasForeignKey(e => e.IdEsp32)
                    .HasConstraintName("fk_usuario_esp32");
            });

            // ==== T_ST_CATEGORIA ====
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("T_ST_CATEGORIA");

                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria)
                    .HasColumnName("id_categoria");

                entity.Property(e => e.NomeCategoria)
                    .HasColumnName("nm_categoria")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.DescricaoCategoria)
                    .HasColumnName("cd_categoria")
                    .IsRequired();
            });

            // ==== T_ST_TRABALHO ====
            modelBuilder.Entity<Trabalho>(entity =>
            {
                entity.ToTable("T_ST_TRABALHO");

                entity.HasKey(e => e.IdTrabalho);

                entity.Property(e => e.IdTrabalho)
                    .HasColumnName("id_trabalho");

                entity.Property(e => e.NomeTrabalho)
                    .HasColumnName("nm_trabalho")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.DescricaoTrabalho)
                    .HasColumnName("cd_trabalho")
                    .IsRequired();

                entity.Property(e => e.IdCategoria)
                    .HasColumnName("id_categoria");

                entity.HasOne(e => e.Categoria)
                    .WithMany(c => c.Trabalhos)
                    .HasForeignKey(e => e.IdCategoria)
                    .HasConstraintName("fk_trabalho_categoria");
            });

            // ==== T_ST_COMENTARIO ====
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("T_ST_COMENTARIO");

                entity.HasKey(e => e.IdComentario);

                entity.Property(e => e.IdComentario)
                    .HasColumnName("id_comentario");

                entity.Property(e => e.TextoComentario)
                    .HasColumnName("cd_comentario");

                // bool <-> CHAR(1)
                entity.Property(e => e.Ativo)
                    .HasColumnName("at_comentario")
                    .HasMaxLength(1)
                    .HasConversion(
                        v => v ? "1" : "0",
                        v => v == "1"
                    );

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario");

                entity.Property(e => e.IdTrabalho)
                    .HasColumnName("id_trabalho");

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.Comentarios)
                    .HasForeignKey(e => e.IdUsuario)
                    .HasConstraintName("fk_comentario_usuario");

                entity.HasOne(e => e.Trabalho)
                    .WithMany(t => t.Comentarios)
                    .HasForeignKey(e => e.IdTrabalho)
                    .HasConstraintName("fk_comentario_trabalho");
            });
        }
    }
}
