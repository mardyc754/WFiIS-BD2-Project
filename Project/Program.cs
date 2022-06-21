using System;
using System.Collections;
using System.Data.SqlClient;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantHandler restaurantHandler = new RestaurantHandler();

            restaurantHandler.MainMenu();

            //ArrayList productsFromCategory = restaurant.GetProductByPriceInCategory(3);

            //foreach (var product in productsFromCategory)
            //{
            //    Console.WriteLine(product);
            //}
        }
    }
}
