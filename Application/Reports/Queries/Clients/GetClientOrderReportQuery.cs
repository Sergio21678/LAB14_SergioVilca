using MediatR;

namespace Application.Reports.Queries.Clients
{
    // El DTO (Data Transfer Object) - Lo que devolvemos
    public class ClientOrderReportDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
    }

    // El Query (La solicitud de datos)
    public class GetClientOrderReportQuery : IRequest<IEnumerable<ClientOrderReportDto>>
    {
        // Este query no necesita parámetros, pedimos todos los clientes
    }
}