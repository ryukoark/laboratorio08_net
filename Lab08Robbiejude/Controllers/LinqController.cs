using Lab08Robbiejude.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq; 

namespace Lab08Robbiejude.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinqController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LinqController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("search-linq")]
        public async Task<IActionResult> GetClientesByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Debe proporcionar un nombre o parte del nombre para buscar.");
            var clientes = await _unitOfWork.Clients.GetAllAsync();
            var resultado = (from c in clientes
                where c.Name.Contains(name)
                select c).ToList();
            return Ok(resultado);
        }
        // ✅ Ejercicio 2: Obtener productos con precio mayor a un valor
        [HttpGet("by-price")]
        public async Task<IActionResult> GetProductosByPrecio([FromQuery] decimal minPrice)
        {
            if (minPrice <= 0)
                return BadRequest("Debe proporcionar un valor de precio mayor a 0.");

            // Obtenemos todos los productos
            var productos = await _unitOfWork.Products.GetAllAsync();

            // LINQ con method syntax (Where + ToList)
            var resultado = productos
                .Where(p => p.Price > minPrice)
                .ToList();

            return Ok(resultado);
        }
        [HttpGet("order-details")]
        public async Task<IActionResult> GetOrderDetails([FromQuery] int orderId)
        {
            var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
            var products = await _unitOfWork.Products.GetAllAsync();
            var resultado = (from od in orderDetails
                join p in products on od.Productid equals p.Productid
                where od.Orderid == orderId
                select new
                {
                    Producto = p.Name,
                    Cantidad = od.Quantity
                }).ToList();

            return Ok(resultado);
        }

    }
}