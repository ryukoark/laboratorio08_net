using Lab08Robbiejude.Models;

namespace Lab08.Services
{
    public interface IReportService
    {
        // Reporte 1: Recibe una lista de clientes y devuelve un Excel
        byte[] GenerateClientsReport(IEnumerable<Client> clients);

        // Reporte 2: Recibe una lista de productos y devuelve un Excel
        byte[] GenerateProductsReport(IEnumerable<Product> products);
    }
}