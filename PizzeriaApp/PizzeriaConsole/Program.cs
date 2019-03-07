using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PizzeriaConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task simulation = Simulation();
            simulation.Wait();

            Console.ReadLine();
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
                string clientName = "No." + (clientsCount + i);
                int productsNumber = random.Next(1, 6);

                Console.WriteLine("Client {0} order {1} products", clientName, productsNumber);
            }
        }
    }
}
