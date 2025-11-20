namespace Domain.Entities
{
    // Nombre de la tabla "clients" en la DB
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Propiedad de navegación: Un cliente puede tener muchas órdenes
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}