using Lab08Robbiejude.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Lab08Robbiejude.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinqController : ControllerBase
    {
        private readonly ILinqService _linqService;

        public LinqController(ILinqService linqService)
        {
            _linqService = linqService;
        }

        [HttpGet("search-linq")]
        public async Task<IActionResult> GetClientesByName([FromQuery] string name)
            => Ok(await _linqService.GetClientesByName(name));

        [HttpGet("by-price")]
        public async Task<IActionResult> GetProductosByPrecio([FromQuery] decimal minPrice)
            => Ok(await _linqService.GetProductosByPrecio(minPrice));

        [HttpGet("order-details")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] int orderId)
            => Ok(await _linqService.GetOrderDetails(orderId));

        [HttpGet("order-total")]
        public async Task<IActionResult> GetOrderTotal([FromQuery] int orderId)
            => Ok(new { OrderId = orderId, TotalProductos = await _linqService.GetOrderTotalQuantity(orderId) });

        [HttpGet("most-expensive-product")]
        public async Task<IActionResult> GetMostExpensiveProduct()
            => Ok(await _linqService.GetMostExpensiveProduct());

        [HttpGet("orders-after-date")]
        public async Task<IActionResult> GetOrdersAfterDate([FromQuery] DateTime fecha)
            => Ok(await _linqService.GetOrdersAfterDate(fecha));

        [HttpGet("average-product-price")]
        public async Task<IActionResult> GetAverageProductPrice()
            => Ok(new { PromedioPrecio = await _linqService.GetAverageProductPrice() });

        [HttpGet("products-without-description")]
        public async Task<IActionResult> GetProductsWithoutDescription()
            => Ok(await _linqService.GetProductsWithoutDescription());

        [HttpGet("client-with-most-orders")]
        public async Task<IActionResult> GetClientWithMostOrders()
            => Ok(await _linqService.GetClientWithMostOrders());

        [HttpGet("orders-and-details")]
        public async Task<IActionResult> GetOrdersAndDetails()
            => Ok(await _linqService.GetOrdersAndDetails());

        [HttpGet("products-by-client")]
        public async Task<IActionResult> GetProductsByClient([FromQuery] int clientId)
            => Ok(await _linqService.GetProductsByClient(clientId));

        [HttpGet("clients-by-product")]
        public async Task<IActionResult> GetClientsByProduct([FromQuery] int productId)
            => Ok(await _linqService.GetClientsByProduct(productId));
    }
}
