using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; // <--- Importante

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Mapeo de Entidades a Tablas
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- CONFIGURACIÓN DE NOMBRES (para PostgreSQL) ---

            // Mapeo de Entidad Client a la tabla "clients"
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");
                entity.Property(e => e.ClientId).HasColumnName("clientid").UseIdentityAlwaysColumn();
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
            });

            // Mapeo de Entidad Product a la tabla "products"
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.Property(e => e.ProductId).HasColumnName("productid").UseIdentityAlwaysColumn();
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
            });

            // Mapeo de Entidad Order a la tabla "orders"
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.Property(e => e.OrderId).HasColumnName("orderid").UseIdentityAlwaysColumn();
                entity.Property(e => e.ClientId).HasColumnName("clientid");
                entity.Property(e => e.OrderDate).HasColumnName("orderdate");
                
                // Relación
                entity.HasOne(o => o.Client)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.ClientId);
            });

            // Mapeo de Entidad OrderDetail a la tabla "orderdetails"
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("orderdetails");
                entity.Property(e => e.OrderDetailId).HasColumnName("orderdetailid").UseIdentityAlwaysColumn();
                entity.Property(e => e.OrderId).HasColumnName("orderid");
                entity.Property(e => e.ProductId).HasColumnName("productid");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                
                // Relaciones
                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId);
            
                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId);
            });
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}