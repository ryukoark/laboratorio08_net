using Lab08Robbiejude.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab08Robbiejude.DTOs;
using Lab08Robbiejude.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab08Robbiejude.Services
{
    public class LinqService : ILinqService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LinqexampleContext _context;

        public LinqService(IUnitOfWork unitOfWork, LinqexampleContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<ClientOrderDto>> GetClientOrdersAsync()
        {
            var clientOrders = await _context.Clients
                .AsNoTracking()
                .Include(c => c.Orders)
                .Select(client => new ClientOrderDto
                {
                    ClientName = client.Name,
                    Orders = client.Orders
                        .Select(order => new OrderDto
                        {
                            OrderId = order.Orderid,
                            OrderDate = order.Orderdate
                        })
                        .ToList()
                })
                .ToListAsync();

            return clientOrders;
        }

        public async Task<IEnumerable<OrderDetailsDto>> GetOrdersWithDetailsAsync()
        {
            var ordersWithDetails = await _context.Orders
                .Include(o => o.Orderdetails)
                .ThenInclude(od => od.Product)
                .AsNoTracking()
                .Select(o => new OrderDetailsDto
                {
                    OrderId = o.Orderid,
                    OrderDate = o.Orderdate,
                    Products = o.Orderdetails.Select(od => new ProductDto
                    {
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity,
                        Price = od.Product.Price
                    }).ToList()
                })
                .ToListAsync();

            return ordersWithDetails;
        }
        public async Task<IEnumerable<ClientProductCountDto>> GetClientsWithProductCountAsync()
        {
            var result = await _context.Clients
                .AsNoTracking()
                .Select(client => new ClientProductCountDto
                {
                    ClientName = client.Name,
                    TotalProducts = client.Orders
                        .Sum(order => order.Orderdetails
                            .Sum(detail => detail.Quantity))
                })
                .ToListAsync();

            return result;
        }
        public async Task<IEnumerable<SalesByClientDto>> GetSalesByClientAsync()
        {
            var ordersQuery = _unitOfWork.Context.Orders;

            var result = await ordersQuery
                .Include(order => order.Orderdetails)
                .ThenInclude(detail => detail.Product)
                .AsNoTracking()
                .GroupBy(order => order.Clientid)
                .Select(group => new SalesByClientDto
                {
                    ClientName = _unitOfWork.Context.Clients
                        .FirstOrDefault(c => c.Clientid == group.Key)!.Name,
                    TotalSales = group.Sum(order =>
                        order.Orderdetails.Sum(detail =>
                            detail.Quantity * detail.Product.Price))
                })
                .OrderByDescending(x => x.TotalSales)
                .ToListAsync();

            return result;
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
            return products.Average(p => p.Price);
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
                group o by o.Clientid
                into g
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
        
        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}