using BusinessDomain.Models;
using DatabaseRepository.Repositories;

namespace PizzeriaApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Save(Order order)
        {
            _orderRepository.Save(order);
        }
    }
}
