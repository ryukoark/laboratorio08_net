using Lab08Robbiejude.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab08Robbiejude.Services
{
    public class LinqService : ILinqService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LinqService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =====================
        // EJERCICIO 1
        // =====================
        public async Task<IEnumerable<object>> GetClientesByName(string name)
        {
            var clientes = await _unitOfWork.Clients.GetAllAsync();
            return (from c in clientes
                    where c.Name.Contains(name)
                    select c).ToList();
        }

        // =====================
        // EJERCICIO 2
        // =====================
        public async Task<IEnumerable<object>> GetProductosByPrecio(decimal minPrice)
        {
            var productos = await _unitOfWork.Products.GetAllAsync();
            return productos.Where(p => p.Price > minPrice).ToList();
        }

        // =====================
        // EJERCICIO 3
        // =====================
        public async Task<IEnumerable<object>> GetOrderDetails(int orderId)
        {
            var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
            var products = await _unitOfWork.Products.GetAllAsync();

            return (from od in orderDetails
                    join p in products on od.Productid equals p.Productid
                    where od.Orderid == orderId
                    select new
                    {
                        Producto = p.Name,
                        Cantidad = od.Quantity
                    }).ToList();
        }

        // =====================
        // EJERCICIO 4
        // =====================
        public async Task<int> GetOrderTotalQuantity(int orderId)
        {
            var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
            return orderDetails.Where(od => od.Orderid == orderId)
                               .Select(od => od.Quantity)
                               .Sum();
        }

        // =====================
        // EJERCICIO 5
        // =====================
        public async Task<object?> GetMostExpensiveProduct()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.OrderByDescending(p => p.Price).FirstOrDefault();
        }

        // =====================
        // EJERCICIO 6
        // =====================
        public async Task<IEnumerable<object>> GetOrdersAfterDate(DateTime fecha)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            return orders.Where(o => o.Orderdate > fecha).ToList();
        }

        // =====================
        // EJERCICIO 7
        // =====================
        public async Task<decimal> GetAverageProductPrice()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.Select(p => p.Price).Average();
        }

        // =====================
        // EJERCICIO 8
        // =====================
        public async Task<IEnumerable<object>> GetProductsWithoutDescription()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.Where(p => string.IsNullOrEmpty(p.Description)).ToList();
        }

        // =====================
        // EJERCICIO 9
        // =====================
        public async Task<object?> GetClientWithMostOrders()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var clients = await _unitOfWork.Clients.GetAllAsync();

            var resultado = (from o in orders
                             group o by o.Clientid into g
                             orderby g.Count() descending
                             select new
                             {
                                 ClientId = g.Key,
                                 NumeroPedidos = g.Count()
                             }).FirstOrDefault();

            if (resultado == null) return null;

            var cliente = clients.FirstOrDefault(c => c.Clientid == resultado.ClientId);
            return new { Cliente = cliente?.Name, resultado.NumeroPedidos };
        }

        // =====================
        // EJERCICIO 10
        // =====================
        public async Task<IEnumerable<object>> GetOrdersAndDetails()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
            var products = await _unitOfWork.Products.GetAllAsync();

            return (from o in orders
                    join od in orderDetails on o.Orderid equals od.Orderid
                    join p in products on od.Productid equals p.Productid
                    select new
                    {
                        o.Orderid,
                        Producto = p.Name,
                        od.Quantity
                    }).ToList();
        }

        // =====================
        // EJERCICIO 11
        // =====================
        public async Task<IEnumerable<object>> GetProductsByClient(int clientId)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
            var products = await _unitOfWork.Products.GetAllAsync();

            return (from o in orders
                    join od in orderDetails on o.Orderid equals od.Orderid
                    join p in products on od.Productid equals p.Productid
                    where o.Clientid == clientId
                    select p.Name).Distinct().ToList();
        }

        // =====================
        // EJERCICIO 12
        // =====================
        public async Task<IEnumerable<object>> GetClientsByProduct(int productId)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
            var clients = await _unitOfWork.Clients.GetAllAsync();

            return (from od in orderDetails
                    join o in orders on od.Orderid equals o.Orderid
                    join c in clients on o.Clientid equals c.Clientid
                    where od.Productid == productId
                    select c.Name).Distinct().ToList();
        }
    }
}
