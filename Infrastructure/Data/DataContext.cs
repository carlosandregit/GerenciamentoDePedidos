using System.Reflection;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<UsuariosSistema> UsuariosSistema { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }
            }
            ConfigureUsuariosSistema(modelBuilder);
            ConfigureClientes(modelBuilder);
            ConfigureProdutos(modelBuilder);
            ConfigurePedidos(modelBuilder);
            ConfigurePedidoProdutos(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ConfigureUsuariosSistema(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuariosSistema>(entity =>
            {
                entity.ToTable("usuarios_sistema");
                entity.HasKey(e => e.IdUsuario).HasName("pk_usuarios_sistema");
                entity.Property(e => e.Usuario).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Senha).HasMaxLength(255).IsRequired();
                entity.HasIndex(u => u.Usuario).HasDatabaseName("ix_usuarios_sistema_usuario").IsUnique();
            });
        }

        private void ConfigureClientes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");
                entity.HasKey(e => e.IdCliente).HasName("pk_clientes");
                entity.Property(e => e.NomeCliente).HasMaxLength(100).IsRequired();
                entity.Property(e => e.CPF).HasMaxLength(11).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.DataCadastro).HasDefaultValueSql("NOW()");
                entity.HasIndex(c => c.CPF).HasDatabaseName("ix_clientes_cpf").IsUnique();                
            });
        }

        private void ConfigureProdutos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("produtos");
                entity.HasKey(e => e.IdProduto).HasName("pk_produtos");
                entity.Property(e => e.DescricaoProduto).HasMaxLength(100).IsRequired();
                entity.Property(e => e.PrecoProduto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Estoque).HasDefaultValue(0);
                entity.HasIndex(p => p.DescricaoProduto).HasDatabaseName("ix_produtos_descricao");
            });
        }

        private void ConfigurePedidos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("pedidos");
                entity.HasKey(e => e.IdPedido).HasName("pk_pedidos");
                entity.Property(e => e.DataPedido).HasDefaultValueSql("NOW()");
                entity.Property(e => e.TotalCalculado).HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Cliente)
                    .WithMany(c => c.Pedidos)
                    .HasForeignKey(p => p.ClienteId)
                    .HasConstraintName("fk_pedidos_clientes")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigurePedidoProdutos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoProduto>(entity =>
            {
                entity.ToTable("pedido_produtos");
                entity.HasKey(e => e.IdPedidoProduto).HasName("pk_pedido_produtos");
                entity.Property(e => e.PrecoUnitario).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DataDoPedido).HasDefaultValueSql("NOW()");

                entity.HasOne(pp => pp.Pedido)
                    .WithMany(p => p.PedidoProdutos)
                    .HasForeignKey(pp => pp.PedidoId)
                    .HasConstraintName("fk_pedido_produtos_pedidos")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pp => pp.Produto)
                    .WithMany(p => p.PedidoProdutos)
                    .HasForeignKey(pp => pp.ProdutoId)
                    .HasConstraintName("fk_pedido_produtos_produtos")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<DateTimeToUtcConverter>();
            configurationBuilder.Properties<DateTime?>()
                .HaveConversion<DateTimeToUtcNullableConverter>();
        }

        private class DateTimeToUtcConverter : ValueConverter<DateTime, DateTime>
        {
            public DateTimeToUtcConverter() : base(
                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            { }
        }

        private class DateTimeToUtcNullableConverter : ValueConverter<DateTime?, DateTime?>
        {
            public DateTimeToUtcNullableConverter() : base(
                v => v.HasValue ? (v.Value.Kind == DateTimeKind.Utc ? v : v.Value.ToUniversalTime()) : null,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null)
            { }
        }

        
        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var buffer = new System.Text.StringBuilder();
            buffer.Append(char.ToLower(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    buffer.Append('_');
                    buffer.Append(char.ToLower(input[i]));
                }
                else
                {
                    buffer.Append(input[i]);
                }
            }

            return buffer.ToString();
        }
    }
}
