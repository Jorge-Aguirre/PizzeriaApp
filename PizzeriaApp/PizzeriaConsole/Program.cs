using DatabaseRepository.Context;
using DatabaseRepository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzeriaApp.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using BusinessDomain.Models;
using System.Collections.Generic;
using BusinessDomain.Logic;

namespace PizzeriaConsole
{
    public class Program
    {
        static object Lock = new object();

        static readonly ServiceProvider serviceProvider = LoadDependencies();
        static IEnumerable<Product> products;
        static IEnumerable<Size> sizes;

        static Manager manager = new Manager();

        static void Main(string[] args)
        {
            LoadDependencies();
            LoadData();
            Task simulation = Simulation();
            simulation.Wait();

            Console.ReadLine();
        }

        static ServiceProvider LoadDependencies()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            return new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton<PizzeriaDbContext>()
                .AddSingleton<IProductRepository, ProductRepository>()
                .AddSingleton<ISizeRepository, SizeRepository>()
                .AddSingleton<IOrderRepository, OrderRepository>()
                .AddSingleton<IOrderProductRepository, OrderProductRepository>()
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<ISizeService, SizeService>()
                .AddSingleton<IOrderService, OrderService>()
                .AddSingleton<IOrderProductService, OrderProductService>()
                .BuildServiceProvider();
        }

        static void LoadData()
        {
            lock (Lock)
            {
                products = serviceProvider.GetService<IProductService>().GetProducts();
                sizes = serviceProvider.GetService<ISizeService>().GetSizes();
            }
        }

        static async Task Simulation()
        {
            manager.NewOrderHandler += NewOrderArrived;
            manager.PreparingProductHandler += ChangingProductState;
            Random random = new Random();
            Console.WriteLine("Pizzeria is now open: waiting for clients");
            int clientsCount = 0;

            while(clientsCount < 12)
            {
                int seconds = random.Next(10, 21);
                int clients = random.Next(1, 4);

                Task delay = Task.Delay(seconds * 1000);
                await delay;
                Console.WriteLine("After {0} seconds {1} clients arrived, processing their orders.....",  seconds, clients);

                ProcessOrders(manager, clients, clientsCount);
                clientsCount += clients;
            }

            Console.WriteLine("Pizzeria is now closed");
        }

        static void ProcessOrders(Manager manager, int clients, int clientsCount)
        {
            Random random = new Random();
            for (int i = 0; i < clients; i++)
            {
                Order order = new Order()
                {
                    Client = "No." + (clientsCount + i + 1),
                    State = OrderState.RECEIVED,
                    OrderProducts = new List<OrderProduct>()
                };

                int productsNumber = random.Next(1, 6);

                for (int j = 0; j< productsNumber; j++)
                {
                    int randomProductIndex = random.Next(products.Count());
                    var product = products.ElementAt(randomProductIndex);
                    int randomSizeIndex = random.Next(sizes.Count());
                    var size = sizes.ElementAt(randomSizeIndex);

                    order.OrderProducts.Add(new OrderProduct()
                    {
                        Product = product,
                        State = State.INQUEUE,
                        UnitPrice = 40, // For the moment the price is hardcoded
                        Size = size,
                        Portions = size.DefaultPortions, // By the moment portions will be fixed from size
                        Quantity = 1
                    });
                }

                lock(Lock)
                {
                    serviceProvider.GetService<IOrderService>().Save(order);
                }

                manager.ProcessOrder(order);
            }
        }

        private static void NewOrderArrived(Order order)
        {
            Console.WriteLine("Client {0} orders: ", order.Client);

            order.OrderProducts.ToList().ForEach(p =>
            {
                Console.WriteLine("Product {0} is {1} from client {2}", p.Product.Name, p.State, order.Client);
            });
        }

        private static void ChangingProductState(OrderProduct product)
        {
            lock(Lock)
            {
                serviceProvider.GetService<IOrderProductService>().UpdateOrderProduct(product);
            }

            Console.WriteLine("Product {0} is now in {1} state", product.Product.Name, product.State);
            manager.ProcessProduct(product);
        }
    }
}
