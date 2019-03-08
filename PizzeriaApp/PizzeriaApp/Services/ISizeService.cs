using BusinessDomain.Models;
using System.Collections.Generic;

namespace PizzeriaApp.Services
{
    public interface ISizeService
    {
        IEnumerable<Size> GetSizes();
    }
}
