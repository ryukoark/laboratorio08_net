using Lab08Robbiejude.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lab08.Services;

namespace Lab08Robbiejude.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinqController(ILinqService linqService, IReportService reportService) : ControllerBase
    {
        [HttpGet("search-linq")]
        public async Task<IActionResult> GetClientesByName([FromQuery] string name)
            => Ok(await linqService.GetClientesByName(name));

        [HttpGet("by-price")]
        public async Task<IActionResult> GetProductosByPrecio([FromQuery] decimal minPrice)
            => Ok(await linqService.GetProductosByPrecio(minPrice));

        [HttpGet("order-details")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] int orderId)
            => Ok(await linqService.GetOrderDetails(orderId));

        [HttpGet("order-total")]
        public async Task<IActionResult> GetOrderTotal([FromQuery] int orderId)
            => Ok(new { OrderId = orderId, TotalProductos = await linqService.GetOrderTotalQuantity(orderId) });

        [HttpGet("most-expensive-product")]
        public async Task<IActionResult> GetMostExpensiveProduct()
            => Ok(await linqService.GetMostExpensiveProduct());

        [HttpGet("orders-after-date")]
        public async Task<IActionResult> GetOrdersAfterDate([FromQuery] DateTime fecha)
            => Ok(await linqService.GetOrdersAfterDate(fecha));

        [HttpGet("average-product-price")]
        public async Task<IActionResult> GetAverageProductPrice()
            => Ok(new { PromedioPrecio = await linqService.GetAverageProductPrice() });

        [HttpGet("products-without-description")]
        public async Task<IActionResult> GetProductsWithoutDescription()
            => Ok(await linqService.GetProductsWithoutDescription());

        [HttpGet("client-with-most-orders")]
        public async Task<IActionResult> GetClientWithMostOrders()
            => Ok(await linqService.GetClientWithMostOrders());

        [HttpGet("orders-and-details")]
        public async Task<IActionResult> GetOrdersAndDetails()
            => Ok(await linqService.GetOrdersAndDetails());

        [HttpGet("products-by-client")]
        public async Task<IActionResult> GetProductsByClient([FromQuery] int clientId)
            => Ok(await linqService.GetProductsByClient(clientId));

        [HttpGet("clients-by-product")]
        public async Task<IActionResult> GetClientsByProduct([FromQuery] int productId)
            => Ok(await linqService.GetClientsByProduct(productId));
        
        [HttpGet("clients-orders")]
        public async Task<IActionResult> GetClientsWithOrders()
        {
            var result = await linqService.GetClientOrdersAsync();
            return Ok(result);
        }
        [HttpGet("orders-with-details")]
        public async Task<IActionResult> GetOrdersWithDetails()
        {
            var result = await linqService.GetOrdersWithDetailsAsync();
            return Ok(result);
        }
        [HttpGet("clients-product-count")]
        public async Task<IActionResult> GetClientsWithProductCount()
        {
            var result = await linqService.GetClientsWithProductCountAsync();
            return Ok(result);
        }
        [HttpGet("sales-by-client")]
        public async Task<IActionResult> GetSalesByClient()
        {
            var result = await linqService.GetSalesByClientAsync();
            return Ok(result);
        }
        [HttpGet("reports/clients")]
        public async Task<IActionResult> GetClientsExcelReport()
        {
            // 1. Obtener los datos (usando el LinqService)
            // (Asegúrate de tener "GetAllClientsAsync" en tu ILinqService)
            var clients = await linqService.GetAllClientsAsync();

            // 2. Generar el reporte (usando el ReportService)
            byte[] fileBytes = reportService.GenerateClientsReport(clients);

            // 3. Devolver el archivo Excel
            string fileName = $"ReporteClientes_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("reports/products")]
        public async Task<IActionResult> GetProductsExcelReport()
        {
            // 1. Obtener los datos
            // (Asegúrate de tener "GetAllProductsAsync" en tu ILinqService)
            var products = await linqService.GetAllProductsAsync();

            // 2. Generar el reporte
            byte[] fileBytes = reportService.GenerateProductsReport(products);

            // 3. Devolver el archivo Excel
            string fileName = $"ReporteProductos_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}