using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    // Nombre de la tabla "orderdetails" en la DB
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Propiedades de navegación
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}