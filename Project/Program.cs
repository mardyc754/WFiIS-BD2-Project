using System;
using System.Data.SqlClient;

namespace ConnectingToSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");

            string connectionString = @"Data Source=X240\SQLEXPRESS;Initial Catalog = AdventureWorks2016;Integrated Security = True;";
           
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
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
