using System;
using System.Collections;
using System.Data.SqlClient;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantView restaurantHandler = new RestaurantView();

            restaurantHandler.MainMenu();
        }
    }
}
