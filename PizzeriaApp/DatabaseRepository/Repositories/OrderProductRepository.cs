using BusinessDomain.Models;
using DatabaseRepository.Context;

namespace DatabaseRepository.Repositories
{
    public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(PizzeriaDbContext context)
            :base(context)
        {
        }
    }
}
