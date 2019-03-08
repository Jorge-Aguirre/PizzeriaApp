using BusinessDomain.Models;
using DatabaseRepository.Repositories;

namespace PizzeriaApp.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly IOrderProductRepository _orderProductRepository;

        public OrderProductService(IOrderProductRepository orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }

        public void UpdateOrderProduct(OrderProduct orderProduct)
        {
            _orderProductRepository.Update(orderProduct);
        }
    }
}
