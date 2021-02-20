using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empregare.Models;

namespace Empregare.Data
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Vaga> Vaga { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        /// Iniciar Configurações das tabelas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Cryptography cryptography = new Cryptography(System.Security.Cryptography.MD5.Create());

            modelBuilder.Entity<Usuario>
            ( etd =>
                {
                    etd.ToTable("TabelaUsuario");
                    etd.HasKey(p => p.UsuarioId).HasName("UsuarioId");
                    etd.Property(p => p.UsuarioId).HasColumnType("int").ValueGeneratedOnAdd();
                    etd.Property(p => p.Nome).IsRequired().HasMaxLength(50);
                    etd.Property(p => p.Telefone).IsRequired().HasMaxLength(15);
                    etd.Property(p => p.Email).IsRequired().HasMaxLength(50);
                    etd.Property(p => p.Senha).IsRequired();

                }
            );

            modelBuilder.Entity<Vaga>
           (etd =>
           {
               etd.ToTable("TabelaVagas");
               etd.Property(v => v.Id).IsRequired();
               etd.Property(v => v.Cargo).IsRequired().HasMaxLength(50);
               etd.Property(v => v.Localizacao).IsRequired().HasMaxLength(50);
               etd.Property(v => v.Salario).IsRequired().HasMaxLength(15);
               etd.Property(v => v.Data).IsRequired();

           }
           );
        }   
    }
}
