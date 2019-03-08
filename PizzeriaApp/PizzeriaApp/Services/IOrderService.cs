using BusinessDomain.Models;

namespace PizzeriaApp.Services
{
    public interface IOrderService
    {
        void Save(Order order);
    }
}
