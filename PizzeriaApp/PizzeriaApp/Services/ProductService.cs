using System.Collections.Generic;
using BusinessDomain.Models;
using DatabaseRepository.Repositories;

namespace PizzeriaApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetAll();
        }
    }
}
