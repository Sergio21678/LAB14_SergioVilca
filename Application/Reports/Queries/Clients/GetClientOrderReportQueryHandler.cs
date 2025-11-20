using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.Queries.Clients
{
    public class GetClientOrderReportQueryHandler : IRequestHandler<GetClientOrderReportQuery, IEnumerable<ClientOrderReportDto>>
    {
        // Cambiar de AppDbContext a IAppDbContext
        private readonly IAppDbContext _context;

        public GetClientOrderReportQueryHandler(IAppDbContext context) // <-- Cambiar aquí
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientOrderReportDto>> Handle(GetClientOrderReportQuery request, CancellationToken cancellationToken)
        {
            var reportData = await _context.Clients
                .AsNoTracking() 
                .Select(client => new ClientOrderReportDto
                {
                    ClientId = client.ClientId,
                    ClientName = client.Name,
                    ClientEmail = client.Email,
                    TotalOrders = client.Orders.Count()
                })
                .ToListAsync(cancellationToken);

            return reportData;
        }
    }
}