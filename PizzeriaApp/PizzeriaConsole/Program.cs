using DatabaseRepository.Context;
using DatabaseRepository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzeriaApp.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace PizzeriaConsole
{
    public class Program
    {
        static ServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            LoadDependencies();
            Task simulation = Simulation();
            simulation.Wait();

            Console.ReadLine();
        }

        static void LoadDependencies()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            serviceProvider = new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton<PizzeriaDbContext>()
                .AddSingleton<IProductRepository, ProductRepository>()
                .AddSingleton<IProductService, ProductService>()
                .BuildServiceProvider();
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
            var products = serviceProvider.GetService<IProductService>().GetProducts();

            Random random = new Random();
            for (int i = 0; i < clients; i++)
            {
                string clientName = "No." + (clientsCount + i);
                int productsNumber = random.Next(1, 6);

                Console.WriteLine("Client {0} order {1} products", clientName, productsNumber);

                for (int j = 0; j< productsNumber; j++)
                {
                    int r = random.Next(products.Count());
                    var product = products.ElementAt(r);
                }
            }
        }
    }
}
