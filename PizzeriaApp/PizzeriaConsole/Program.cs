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

namespace PizzeriaConsole
{
    public class Program
    {
        static readonly ServiceProvider serviceProvider = LoadDependencies();
        static IEnumerable<Product> products;
        static IEnumerable<Size> sizes;

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
                .AddSingleton<IProductService, ProductService>()
                .BuildServiceProvider();
        }

        static void LoadData()
        {
            products = serviceProvider.GetService<IProductService>().GetProducts();
            sizes = serviceProvider.GetService<ISizeService>().GetSizes();
        }

        static async Task Simulation()
        {
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
                clientsCount += clients;

                ProcessOrders(clients, clientsCount);
            }

            Console.WriteLine("Pizzeria is now closed");
        }

        static void ProcessOrders(int clients, int clientsCount)
        {
            Random random = new Random();
            for (int i = 0; i < clients; i++)
            {
                Order order = new Order()
                {
                    Client = "No." + (clientsCount + i),
                    Status = OrderState.RECEIVED,
                    Products = new List<OrderProduct>()
                };

                int productsNumber = random.Next(1, 6);

                Console.WriteLine("Client {0} order {1} products", order.Client, productsNumber);

                for (int j = 0; j< productsNumber; j++)
                {
                    int randomProductIndex = random.Next(products.Count());
                    var product = products.ElementAt(randomProductIndex);
                    int randomSizeIndex = random.Next(sizes.Count());
                    var size = sizes.ElementAt(randomSizeIndex);

                    order.Products.Add(new OrderProduct()
                    {
                        Product = product,
                        State = State.INQUEUE,
                        UnitPrice = 40, // For the moment the price is hardcoded
                        Size = size,
                        Portions = size.Portions, // By the moment portions will be fixed from size
                        Quantity = 1,
                        Order = order
                    });
                }
            }
        }
    }
}
