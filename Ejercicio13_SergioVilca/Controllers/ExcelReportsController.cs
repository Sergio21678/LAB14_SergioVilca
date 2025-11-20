using Application.Reports.Queries.Clients;
using Application.Reports.Queries.Products;
using ClosedXML.Excel;
using MediatR; // Para enviar los queries
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Ejercicio13_SergioVilca.Controllers
{
    [ApiController]
    [Route("api/reports")] // URL base: /api/reports
    public class ExcelReportsController : ControllerBase
    {
        private readonly ISender _mediator; // ISender es de MediatR, lo usamos para enviar queries

        public ExcelReportsController(ISender mediator)
        {
            _mediator = mediator;
        }

        // --- REPORTE 1 ---
        // URL: GET /api/reports/clients
        [HttpGet("clients")]
        public async Task<IActionResult> GetClientOrderReport()
        {
            // 1. Ejecutar el Query CQRS para obtener los datos
            var query = new GetClientOrderReportQuery();
            var data = await _mediator.Send(query);

            // 2. Generar el Excel usando ClosedXML
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Clients Report");
                
                // Encabezados
                worksheet.Cell(1, 1).Value = "Client ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Email";
                worksheet.Cell(1, 4).Value = "Total Orders";
                
                // Estilo para el encabezado
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

                // Cargar los datos (empezando desde la fila 2)
                worksheet.Cell(2, 1).InsertData(data);
                
                worksheet.Columns().AdjustToContents(); // Ajustar columnas

                // 3. Guardar el libro en un MemoryStream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = "ClientOrderReport.xlsx";

                    // 4. Devolver el archivo
                    return File(content, contentType, fileName);
                }
            }
        }

        // --- REPORTE 2 ---
        // URL: GET /api/reports/products
        [HttpGet("products")]
        public async Task<IActionResult> GetProductSalesReport()
        {
            // 1. Ejecutar el Query CQRS
            var query = new GetProductSalesReportQuery();
            var data = await _mediator.Send(query);

            // 2. Generar el Excel
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products Report");

                // Encabezados
                worksheet.Cell(1, 1).Value = "Product ID";
                worksheet.Cell(1, 2).Value = "Product Name";
                worksheet.Cell(1, 3).Value = "Total Quantity Sold";
                worksheet.Cell(1, 4).Value = "Total Revenue";
                
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGreen;
                
                // Cargar datos
                worksheet.Cell(2, 1).InsertData(data);
                
                // Formatear la columna de ingresos como Moneda
                worksheet.Column(4).Style.NumberFormat.Format = "$#,##0.00";
                
                worksheet.Columns().AdjustToContents();

                // 3. Guardar y devolver el archivo
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = "ProductSalesReport.xlsx";

                    return File(content, contentType, fileName);
                }
            }
        }
    }
}