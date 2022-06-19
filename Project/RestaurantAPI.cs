using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Project
{
    class RestaurantAPI
    {
        private SqlConnection connection = null;

        public RestaurantAPI()
        {
            string connStr = @"Data Source=X240\SQLEXPRESS;Initial Catalog = Project;Integrated Security = True;";
            connection = new SqlConnection(connStr);
            connection.Open();
        }

        public Product GetProductByID(int productId)
        {
            SqlCommand getProductByIDCommand = new SqlCommand("getProductByID", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            getProductByIDCommand.Parameters.Add("@id", SqlDbType.Int).Value = productId;

            using (SqlDataReader result = getProductByIDCommand.ExecuteReader())
            {

                try
                {
                    result.Read();

                    Vegetarian isVegetarian = !result["vegetarian"].Equals(DBNull.Value) ? 
                        new Vegetarian((bool)result["vegetarian"]) : null;
                    Price priceSmall = !result["priceSmall"].Equals(DBNull.Value) ? 
                        new Price("small", (decimal)result["priceSmall"]) : null;
                    Price priceMedium = new Price("medium", (decimal)result["priceMedium"]);
                    Price priceLarge = !result["priceLarge"].Equals(DBNull.Value) ? 
                        new Price("large", (decimal)result["priceLarge"]) : null;

                    Product product = new Product(
                        (int)result["productID"], 
                        (string)result["name"], 
                        isVegetarian, 
                        priceSmall,
                        priceMedium,
                        priceLarge,
                        (int)result["categoryID"], 
                        (string)result["categoryName"]
                    );
                    return product;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

    }
}
