using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab08Robbiejude.DTOs;
using Lab08Robbiejude.Models;

namespace Lab08Robbiejude.Services
{
    public interface ILinqService
    {
        // Ejercicio 1
        Task<IEnumerable<object>> GetClientesByName(string name);

        // Ejercicio 2
        Task<IEnumerable<object>> GetProductosByPrecio(decimal minPrice);

        // Ejercicio 3
        Task<IEnumerable<object>> GetOrderDetails(int orderId);

        // Ejercicio 4
        Task<int> GetOrderTotalQuantity(int orderId);

        // Ejercicio 5
        Task<object?> GetMostExpensiveProduct();

        // Ejercicio 6
        Task<IEnumerable<object>> GetOrdersAfterDate(DateTime fecha);

        // Ejercicio 7
        Task<decimal> GetAverageProductPrice();

        // Ejercicio 8
        Task<IEnumerable<object>> GetProductsWithoutDescription();

        // Ejercicio 9
        Task<object?> GetClientWithMostOrders();

        // Ejercicio 10
        Task<IEnumerable<object>> GetOrdersAndDetails();

        // Ejercicio 11
        Task<IEnumerable<object>> GetProductsByClient(int clientId);

        // Ejercicio 12
        Task<IEnumerable<object>> GetClientsByProduct(int productId);
        
        Task<IEnumerable<ClientOrderDto>> GetClientOrdersAsync();
        Task<IEnumerable<OrderDetailsDto>> GetOrdersWithDetailsAsync();
        Task<IEnumerable<ClientProductCountDto>> GetClientsWithProductCountAsync();
        Task<IEnumerable<SalesByClientDto>> GetSalesByClientAsync();
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}