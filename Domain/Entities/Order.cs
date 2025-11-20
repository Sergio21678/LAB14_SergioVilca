using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    // Nombre de la tabla "orders" en la DB
    public class Order
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public DateTime OrderDate { get; set; }

        // Propiedades de navegación
        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}