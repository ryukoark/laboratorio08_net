using ClosedXML.Excel;
using Lab08.Services;
using Lab08Robbiejude.Models;

namespace Lab08Robbiejude.services
{
    public class ReportService : IReportService
    {
        // Implementación del Reporte de Clientes
        public byte[] GenerateClientsReport(IEnumerable<Client> clients)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Clientes");
                
                // Crear Cabeceras
                worksheet.Cell(1, 1).Value = "Client ID";
                worksheet.Cell(1, 2).Value = "First Name";
                worksheet.Cell(1, 3).Value = "Last Name";
                worksheet.Cell(1, 4).Value = "Email";
                // +++ CAMBIO AQUÍ +++
                worksheet.Cell(1, 5).Value = "Phone";
                
                // Estilo para cabeceras (como en la Parte 5)
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

                // Llenar datos
                int currentRow = 2;
                foreach (var client in clients)
                {
                    worksheet.Cell(currentRow, 1).Value = client.Clientid;
                    worksheet.Cell(currentRow, 2).Value = client.Name;
                    worksheet.Cell(currentRow, 4).Value = client.Email;
                    currentRow++;
                }

                // Guardar en un stream de memoria
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        // Implementación del Reporte de Productos (Este estaba correcto)
        public byte[] GenerateProductsReport(IEnumerable<Product> products)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Productos");
                
                // Crear Cabeceras
                worksheet.Cell(1, 1).Value = "Product ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Price";
                worksheet.Cell(1, 4).Value = "Stock";
                
                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightBlue;
                
                // Llenar datos
                int currentRow = 2;
                foreach (var product in products)
                {
                    worksheet.Cell(currentRow, 1).Value = product.Productid;
                    worksheet.Cell(currentRow, 2).Value = product.Name;
                    worksheet.Cell(currentRow, 3).Value = product.Price;
                    currentRow++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}