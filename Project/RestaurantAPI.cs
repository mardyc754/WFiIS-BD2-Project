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

                    Product product = new Product((int)result["id"], (string)result["name"], (bool)result["vegetarian"], 
                        (double)result["priceSmall"], (double)result["priceMedium"], (double)result["priceLarge"], 
                        (int)result["categoryID"], (string)result["categoryName"]);

                    return product;
                }
                catch (Exception e)
                {
                    return null;
                }

            };
        }

    }
}
