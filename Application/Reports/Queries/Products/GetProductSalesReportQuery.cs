using MediatR;

namespace Application.Reports.Queries.Products
{
    // El DTO
    public class ProductSalesReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    // El Query
    public class GetProductSalesReportQuery : IRequest<IEnumerable<ProductSalesReportDto>>
    {
    }
}