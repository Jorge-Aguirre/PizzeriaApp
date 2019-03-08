using BusinessDomain.Models;
using DatabaseRepository.Context;

namespace DatabaseRepository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(PizzeriaDbContext context)
            :base(context)
        {
        }
    }
}
