using System.Collections.Generic;
using BusinessDomain.Models;
using DatabaseRepository.Repositories;

namespace PizzeriaApp.Services
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public IEnumerable<Size> GetSizes()
        {
            throw new System.NotImplementedException();
        }
    }
}
