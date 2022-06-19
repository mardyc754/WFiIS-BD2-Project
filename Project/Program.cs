using System;
using System.Data.SqlClient;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantAPI restaurant = new RestaurantAPI();
            
            Console.WriteLine(restaurant.GetProductByID(1));
            
            Console.WriteLine("Naciśnij q, aby zakończyć działanie programu...");
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
