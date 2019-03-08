using BusinessDomain.Models;
using DatabaseRepository.Context;

namespace DatabaseRepository.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(PizzeriaDbContext context)
            :base(context)
        {
        }
    }
}
