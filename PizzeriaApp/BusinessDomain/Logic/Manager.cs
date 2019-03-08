using BusinessDomain.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessDomain.Logic
{
    public class Manager
    {
        public delegate void NewOrder(Order order);
        public event NewOrder NewOrderHandler;

        public delegate void NewProduct(OrderProduct product);
        public event NewProduct PreparingProductHandler;

        public void ProcessOrder(Order order)
        {
            NewOrderHandler(order);

            foreach (var product in order.OrderProducts)
            {
                new Task(delegate ()
                {
                    ProcessProduct(product);
                }).Start();
            }
        }

        public async Task ProcessProduct(OrderProduct product)
        {
            Random random = new Random();
            int randomTime = random.Next(5, 11); // Random times for steps process not defined in database
            Task delay = Task.Delay(randomTime * 1000);
            await delay;
            State newState = product.State;

            switch (product.State)
            {
                case State.INQUEUE:
                    newState = State.PREPARING;
                    break;
                case State.PREPARING:
                    newState = product.Product.ProductTypeId == 1 // Hardcoded maybe replace with hierarchy solution
                        ? State.COOKING
                        : State.DELIVERED;
                    break;
                case State.COOKING:
                    newState = State.CUTING;
                    break;
                case State.CUTING:
                    newState = State.PACKAGING;
                    break;
                case State.PACKAGING:
                    newState = State.SENDING;
                    break;
                case State.SENDING:
                    newState = State.DELIVERED;
                    break;
                default:
                    break;
            }

            product.State = newState;

            if (!product.State.HasFlag(State.DELIVERED))
            {
                PreparingProductHandler(product);
            }
        }
    }
}
