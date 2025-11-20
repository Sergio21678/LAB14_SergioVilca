namespace Domain.Entities
{
    // Nombre de la tabla "products" en la DB
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        // Propiedad de navegación
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}