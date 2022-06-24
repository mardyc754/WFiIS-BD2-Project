using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Project
{
    public class Restaurant
    {
        private readonly SqlConnection connection = null;

        public Restaurant()
        {
            string connStr = @"Data Source=******;Initial Catalog = Project;Integrated Security = True;";
            connection = new SqlConnection(connStr);
            connection.Open();
        }

        private static List<Product> GetProductsFromDatabase(SqlCommand command)
        {
            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    List<Product> productList = new List<Product>();
                    while (result.Read())
                    {
                        productList.Add(Product.CreateProduct(result));
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

        public void InsertDefaultData()
        {
            SqlCommand command = new SqlCommand("insertDefaultData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.ExecuteNonQuery();
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
                    return Product.CreateProduct(result);
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

        public List<Category> GetAllCategories()
        {
            SqlCommand command = new SqlCommand("getAllCategories", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    List<Category> categoriesList = new List<Category>();
                    while (result.Read())
                    {
                        categoriesList.Add(new Category((int)result["categoryID"], (string)result["categoryName"]));
                    }
                    return categoriesList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

            };
        }

        public List<Product> GetAllProducts()
        {
            SqlCommand command = new SqlCommand("getAllProducts", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            return GetProductsFromDatabase(command);
        }


        public List<Product> GetProductsFromCategory(Category category)
        {
            SqlCommand command = new SqlCommand("getProductsFromCategory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = category.ID;

            return GetProductsFromDatabase(command);
        }

        public List<Product> GetProductsByName(string productName)
        {
            SqlCommand command = new SqlCommand("getProductByName", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = '%' + productName + '%';

            return GetProductsFromDatabase(command);
        }

        public List<Product> GetVegetarianProducts()
        {
            SqlCommand command = new SqlCommand("getVegetarianProducts", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            return GetProductsFromDatabase(command);
        }

        public List<Product> GetVegetarianProductsInCategory(Category category)
        {
            SqlCommand command = new SqlCommand("getVegetarianProductsInCategory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = category.ID;

            return GetProductsFromDatabase(command);
        }
        public List<Product> GetProductByPrice(decimal priceMin = 0,
            decimal priceMax = 922337203685477.5807m // SqlMoney.MaxValue
            )
        {
            SqlCommand command = new SqlCommand("getProductByPrice", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@priceMin", SqlDbType.Money).Value = priceMin;
            command.Parameters.Add("@priceMax", SqlDbType.Money).Value = priceMax;


            return GetProductsFromDatabase(command);
        }

        public List<Product> GetProductByPriceInCategory(Category category, decimal priceMin = 0,
            decimal priceMax = 922337203685477.5807m // SqlMoney.MaxValue
            )
        {
            SqlCommand command = new SqlCommand("getProductByPriceInCategory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = category.ID;
            command.Parameters.Add("@priceMin", SqlDbType.Money).Value = priceMin;
            command.Parameters.Add("@priceMax", SqlDbType.Money).Value = priceMax;

            return GetProductsFromDatabase(command);
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

        public void AddProduct(Product product)
        {
            SqlCommand command = new SqlCommand("addProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = product.CategoryID;
            command.Parameters.Add("@prodName", SqlDbType.NVarChar).Value = product.Name;
            command.Parameters.Add("@priceMedium", SqlDbType.Money).Value = product.PriceMedium.Value;
            if (product.Vegetarian != null)
            {
                command.Parameters.Add("@vegetarian", SqlDbType.Bit).Value = product.Vegetarian.IsVegetarian;
            } else {
                command.Parameters.Add("@vegetarian", SqlDbType.Bit).Value = DBNull.Value;
            }
            command.ExecuteNonQuery();
        }

        public void AddPrice(Product product, decimal newPrice, string priceType)
        {
            string addPriceMethodName = "";
            string addPriceParam = "";

            switch (priceType)
            {
                case "small":
                    addPriceMethodName = "addPriceSmall";
                    addPriceParam = "@priceSmall";
                    break;
                case "large":
                    addPriceMethodName = "addPriceLarge";
                    addPriceParam = "@priceLarge";
                    break;
            }

            SqlCommand command = new SqlCommand(addPriceMethodName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@productID", SqlDbType.Int).Value = product.ID;
            command.Parameters.Add(addPriceParam, SqlDbType.Money).Value = newPrice;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void ModifyCategoryName(Category category, string newName)
        {
            SqlCommand command = new SqlCommand("modifyCategoryName", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@newName", SqlDbType.NVarChar).Value = newName;
            command.Parameters.Add("@categoryID", SqlDbType.Int).Value = category.ID;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ModifyProductName(Product product, string newName)
        {
            SqlCommand command = new SqlCommand("modifyProductName", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@newName", SqlDbType.NVarChar).Value = newName;
            command.Parameters.Add("@productID", SqlDbType.Int).Value = product.ID;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ModifyProductPrice(Product product, decimal newPrice, string priceType)
        {
            string modifyPriceCommand = "", modifyPriceVariable = "";
            switch (priceType)
            {
                case "small":
                    modifyPriceCommand = "modifyProductPriceSmall";
                    modifyPriceVariable = "@newPriceSmall";
                    break;
                case "medium":
                    modifyPriceCommand = "modifyProductPriceMedium";
                    modifyPriceVariable = "@newPriceMedium";
                    break;
                case "large":
                    modifyPriceCommand = "modifyProductPriceLarge";
                    modifyPriceVariable = "@newPriceLarge";
                    break;
            }
            SqlCommand command = new SqlCommand(modifyPriceCommand, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(modifyPriceVariable, SqlDbType.Money).Value = newPrice;
            command.Parameters.Add("@productID", SqlDbType.Int).Value = product.ID;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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

        public void DeleteProduct(Product product)
        {
            DeleteById(product.ID, "deleteProduct", "@productID");
        }
        public void DeleteCategory(Category category)
        {
            DeleteById(category.ID, "deleteCategory", "@categoryID");
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
