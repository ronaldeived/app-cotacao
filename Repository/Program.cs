using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Service;

namespace Repository
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<MongoDBService>()
                .BuildServiceProvider();
            
            Console.ReadKey();
        }
    }
}