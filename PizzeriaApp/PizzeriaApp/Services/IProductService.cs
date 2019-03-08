using BusinessDomain.Models;
using System.Collections.Generic;

namespace PizzeriaApp.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}
