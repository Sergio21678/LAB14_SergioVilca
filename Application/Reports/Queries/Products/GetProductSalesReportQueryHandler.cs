using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.Queries.Products
{
    public class GetProductSalesReportQueryHandler : IRequestHandler<GetProductSalesReportQuery, IEnumerable<ProductSalesReportDto>>
    {
        // Cambiar de AppDbContext a IAppDbContext
        private readonly IAppDbContext _context;

        public GetProductSalesReportQueryHandler(IAppDbContext context) // <-- Cambiar aquí
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductSalesReportDto>> Handle(GetProductSalesReportQuery request, CancellationToken cancellationToken)
        {
            var reportData = await _context.OrderDetails
                .AsNoTracking()
                .GroupBy(od => od.Product)
                .Select(group => new ProductSalesReportDto
                {
                    ProductId = group.Key.ProductId,
                    ProductName = group.Key.Name,
                    TotalQuantitySold = group.Sum(od => od.Quantity),
                    TotalRevenue = group.Sum(od => od.Quantity * group.Key.Price)
                })
                .ToListAsync(cancellationToken);

            return reportData;
        }
    }
}