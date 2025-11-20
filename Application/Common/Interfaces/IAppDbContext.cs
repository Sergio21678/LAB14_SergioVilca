using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    // Este es el "contrato" que la capa Application conoce.
    // Solo exponemos lo que los Handlers necesitan.
    public interface IAppDbContext
    {
        DbSet<Client> Clients { get; }
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderDetail> OrderDetails { get; }

        // Esto es necesario para que los Handlers puedan
        // guardar cambios si tuviéramos Comandos (Commands).
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}