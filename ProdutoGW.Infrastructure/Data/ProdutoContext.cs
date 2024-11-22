using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProdutoGW.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoGW.Infrastructure.Data
{
    public class ProdutoContext : DbContext
    {
        DbContextOptions<ProdutoContext> _options;
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options) { _options = options; }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produtos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Guid).IsRequired().HasColumnType("uniqueIdentifier");
                entity.Property(p => p.Nome).IsRequired().HasColumnType("varchar(100)");
                entity.Property(p => p.Descricao).HasColumnType("varchar(200)");
                entity.Property(p => p.Categoria).IsRequired().HasColumnType("varchar(50)");
                entity.Property(p => p.Marca).HasColumnType("varchar(50)");
                entity.Property(p => p.Preco).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Guid).IsRequired().HasColumnType("uniqueIdentifier");
                entity.Property(u => u.Nome).IsRequired().HasColumnType("varchar(100)");
                entity.Property(u => u.Email).IsRequired().HasColumnType("varchar(200)");
                entity.Property(u => u.SenhaHash).IsRequired().HasColumnType("varchar(MAX)");
                entity.Property(u => u.Role).IsRequired().HasColumnType("varchar(50)");
            });

            // Seed data para a tabela de usuários
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Guid = Guid.NewGuid(),
                    Nome = "Administrador",
                    Email = "admin@dominio.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin"), 
                    Role = "admin"
                }
            );
        }
    }
}
