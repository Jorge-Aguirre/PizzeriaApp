using BusinessDomain.Models;
using DatabaseRepository.Context;

namespace DatabaseRepository.Repositories
{
    public class SizeRepository : BaseRepository<Size>, ISizeRepository
    {
        public SizeRepository(PizzeriaDbContext context)
            :base(context)
        {
        }
    }
}
