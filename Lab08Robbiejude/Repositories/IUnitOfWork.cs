using Lab08Robbiejude.Models;

namespace Lab08Robbiejude.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Orderdetail> OrderDetails { get; }
        LinqexampleContext Context { get; }
        Task<int> SaveAsync();
    }
}