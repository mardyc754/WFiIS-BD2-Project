using System;
using System.Collections;
using System.Data.SqlClient;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantAPI restaurant = new RestaurantAPI();


            //restaurant.DeleteCategory(3);
            //Console.WriteLine(restaurant.GetProductByID(34));
            restaurant.DeleteMenu(
                );
            ArrayList productsFromCategory = restaurant.GetProductByPriceInCategory(3);

            foreach (var product in productsFromCategory)
            {
                Console.WriteLine(product);
            }


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
