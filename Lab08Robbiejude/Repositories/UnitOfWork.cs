using Lab08Robbiejude.Models;

namespace Lab08Robbiejude.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LinqexampleContext _context;

        public IGenericRepository<Client> Clients { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<Orderdetail> OrderDetails { get; }
        public LinqexampleContext Context => _context; // ✅ Exponemos el DbContext

        public UnitOfWork(LinqexampleContext context)
        {
            _context = context;
            Clients = new GenericRepository<Client>(context);
            Products = new GenericRepository<Product>(context);
            Orders = new GenericRepository<Order>(context);
            OrderDetails = new GenericRepository<Orderdetail>(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}