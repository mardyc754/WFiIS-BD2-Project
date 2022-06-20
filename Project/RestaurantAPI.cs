﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

        Product createProduct(SqlDataReader result)
        {
            Vegetarian isVegetarian = !result["vegetarian"].Equals(DBNull.Value) ?
                new Vegetarian((bool)result["vegetarian"]) : null;
            
            Price priceSmall = !result["priceSmall"].Equals(DBNull.Value) ?
                new Price((decimal)result["priceSmall"], "small") : null;
            
            Price priceLarge = !result["priceLarge"].Equals(DBNull.Value) ?
                new Price((decimal)result["priceLarge"], "large") : null;
            
            Price priceMedium = new Price((decimal)result["priceMedium"], "medium");

            return new Product(
                        (int)result["productID"],
                        (string)result["name"],
                        isVegetarian,
                        priceSmall,
                        priceMedium,
                        priceLarge,
                        (int)result["categoryID"],
                        (string)result["categoryName"]
                    );
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
                    return createProduct(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public Category GetCategoryByID(int categoryId)
        {
            SqlCommand getCategoryByIDCommand = new SqlCommand("getCategoryByID", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            getCategoryByIDCommand.Parameters.Add("@id", SqlDbType.Int).Value = categoryId;

            using (SqlDataReader result = getCategoryByIDCommand.ExecuteReader())
            {

                try
                {
                    result.Read();
                    Category category = new Category((int)result["categoryID"], (string)result["Name"]);
                    return category;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public ArrayList GetProductsFromCategoryByCategoryID(int categoryId)
        {
            SqlCommand command = new SqlCommand("getProductsFromCategoryByCategoryID", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@id", SqlDbType.Int).Value = categoryId;

            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    ArrayList productList = new ArrayList();
                    while (result.Read())
                    {
                        productList.Add(createProduct(result));
                    }
                    return productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public ArrayList GetProductsFromCategoryByCategoryName(string categoryName)
        {
            SqlCommand command = new SqlCommand("getProductsFromCategoryByCategoryName", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = categoryName + '%';

            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    ArrayList productList = new ArrayList();
                    while (result.Read())
                    {
                        productList.Add(createProduct(result));
                    }
                    return productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public ArrayList GetProductByName(string productName)
        {
            SqlCommand command = new SqlCommand("getProductByName", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = productName + '%';

            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    ArrayList productList = new ArrayList();
                    while (result.Read())
                    {
                        productList.Add(createProduct(result));
                    }
                    return productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public ArrayList GetVegetarianProducts()
        {
            SqlCommand command = new SqlCommand("getVegetarianProducts", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    ArrayList productList = new ArrayList();
                    while (result.Read())
                    {
                        productList.Add(createProduct(result));
                    }
                    return productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public ArrayList GetVegetarianProductsInCategory(int categoryId)
        {
            SqlCommand command = new SqlCommand("getVegetarianProductsInCategory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@categoryID", SqlDbType.Int).Value =  categoryId;

            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    ArrayList productList = new ArrayList();
                    while (result.Read())
                    {
                        productList.Add(createProduct(result));
                    }
                    return productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }
        public ArrayList GetProductByPrice(decimal priceMin = 0, 
            decimal priceMax = 922337203685477.5807m // SqlMoney.MaxValue
            ) 
        {
            SqlCommand command = new SqlCommand("getProductByPrice", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@priceMin", SqlDbType.Money).Value = priceMin;
            command.Parameters.Add("@priceMax", SqlDbType.Money).Value = priceMax;


            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    ArrayList productList = new ArrayList();
                    while (result.Read())
                    {
                        productList.Add(createProduct(result));
                    }
                    return productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public ArrayList GetProductByPriceInCategory(int categoryId, decimal priceMin = 0,
            decimal priceMax = 922337203685477.5807m // SqlMoney.MaxValue
            )
        {
            SqlCommand command = new SqlCommand("getProductByPriceInCategory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryId;
            command.Parameters.Add("@priceMin", SqlDbType.Money).Value = priceMin;
            command.Parameters.Add("@priceMax", SqlDbType.Money).Value = priceMax;


            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    ArrayList productList = new ArrayList();
                    while (result.Read())
                    {
                        productList.Add(createProduct(result));
                    }
                    return productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public void AddCategory(string categoryName)
        {
            SqlCommand command = new SqlCommand("addCategory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@categoryName", SqlDbType.NVarChar).Value = categoryName;
            command.ExecuteNonQuery();
        }

        public void AddProduct(int categoryID, string prodName, decimal priceMedium, Vegetarian vegetarian = null)
        {
            SqlCommand command = new SqlCommand("addProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID;
            command.Parameters.Add("@prodName", SqlDbType.NVarChar).Value = prodName;
            command.Parameters.Add("@priceMedium", SqlDbType.Money).Value = priceMedium;
            if (vegetarian != null)
            {
                command.Parameters.Add("@vegetarian", SqlDbType.Bit).Value = vegetarian.IsVegetarian;
            } else {
                command.Parameters.Add("@vegetarian", SqlDbType.Bit).Value = DBNull.Value;
            }
            command.ExecuteNonQuery();
        }

        public void AddPriceSmall(int productID, decimal priceSmall)
        {
            SqlCommand command = new SqlCommand("addPriceSmall", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@productID", SqlDbType.Int).Value = productID;
            command.Parameters.Add("@priceSmall", SqlDbType.Money).Value = priceSmall;
            try
            {
                command.ExecuteNonQuery();
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
}

        public void AddPriceLarge(int productID, decimal priceLarge)
        {
            SqlCommand command = new SqlCommand("addPriceLarge", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@productID", SqlDbType.Int).Value = productID;
            command.Parameters.Add("@priceLarge", SqlDbType.Money).Value = priceLarge;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ModifyCategoryName(int categoryID, string newName)
        {
            SqlCommand command = new SqlCommand("modifyCategoryName", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@newName", SqlDbType.NVarChar).Value = newName;
            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ModifyProductName(int productID, string newName)
        {
            SqlCommand command = new SqlCommand("modifyProductName", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@newName", SqlDbType.NVarChar).Value = newName;
            command.Parameters.Add("@productID", SqlDbType.Int).Value = productID;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ModifyProductPrice(int productID, decimal newPrice, string modifyPriceCommand, string modifyPriceVariable)
        {
            SqlCommand command = new SqlCommand(modifyPriceCommand, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(modifyPriceVariable, SqlDbType.Money).Value = newPrice;
            command.Parameters.Add("@productID", SqlDbType.Int).Value = productID;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ModifyProductPriceSmall(int productID, decimal newPriceSmall)
        {
            ModifyProductPrice(productID, newPriceSmall, "modifyProductPriceSmall", "@newPriceSmall");
        }
        public void ModifyProductPriceMedium(int productID, decimal newPriceMedium)
        {
            ModifyProductPrice(productID, newPriceMedium, "modifyProductPriceMedium", "@newPriceMedium");
        }
        public void ModifyProductPriceLarge(int productID, decimal newPriceLarge)
        {
            ModifyProductPrice(productID, newPriceLarge, "modifyProductPriceLarge", "@newPriceLarge");
        }

        private void DeleteById(int id, string deleteCommand, string idParam)
        {
            SqlCommand command = new SqlCommand(deleteCommand, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(idParam, SqlDbType.Int).Value = id;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void DeleteProductPriceLarge(int productID)
        {
            DeleteById(productID, "deleteProductPriceLarge", "@productID");
        }

        public void DeleteProductPriceSmall(int productID)
        {
            DeleteById(productID, "deleteProductPriceSmall", "@productID");
        }

        public void DeleteProduct(int productID)
        {
            DeleteById(productID, "deleteProduct", "@productID");
        }
        public void DeleteCategory(int categoryID)
        {
            DeleteById(categoryID, "deleteCategory", "@categoryID");
        }

        public void DeleteMenu()
        {
            SqlCommand command = new SqlCommand("deleteMenu", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
