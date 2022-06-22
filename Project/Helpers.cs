using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Project
{
    class Helpers
    {
        public static string SizeInPolish(string productSize)
        {
            string sizeInPolish = "";
            switch (productSize)
            {
                case "small":
                    sizeInPolish = "mały";
                    break;
                case "medium":
                    sizeInPolish = "średni";
                    break;
                case "large":
                    sizeInPolish = "duży";
                    break;
            }
            return sizeInPolish;
        }

        public static Dictionary<string, Price> PricesDict(Product product)
        {
            var prices = new Dictionary<string, Price>();
            prices["small"] = product.PriceSmall;
            prices["medium"] = product.PriceMedium;
            prices["large"] = product.PriceLarge;
            return prices;
        }

        public static (bool hasPriceProperValue, string errorMessage) IsNewPriceValueValid(decimal newPrice, Product product, string priceType)
        {
            var productPrices = PricesDict(product);
            bool isValidPrice = false;
            string errorMessage = "";
            switch (priceType)
            {
                case "small":
                    isValidPrice = newPrice < product.PriceMedium.Value;
                    errorMessage = "Cena za mały rozmiar musi być mniejsza od ceny za średni rozmiar";
                    break;
                case "medium":
                    bool isGreaterThanPriceSmall = product.PriceSmall != null ? 
                        newPrice > product.PriceSmall.Value : true;
                    bool isLessThanPriceLarge = product.PriceLarge != null ? 
                        newPrice < product.PriceLarge.Value : true;
                    isValidPrice = isGreaterThanPriceSmall && isLessThanPriceLarge;
                    errorMessage = @"Cena za średni rozmiar musi być większa od ceny za mały rozmiar
                                     oraz mniejsza od ceny za duży rozmiar";
                    break;
                case "large":
                    isValidPrice = newPrice > product.PriceMedium.Value;
                    errorMessage = "Cena za duży rozmiar musi być większa od ceny za średni rozmiar";
                    break;
            }

            return (isValidPrice, errorMessage);
        }
        public static Product CreateProduct(SqlDataReader result)
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

        public static List<Product> GetProductsFromDatabase(SqlCommand command)
        {
            using (SqlDataReader result = command.ExecuteReader())
            {

                try
                {
                    List<Product> productList = new List<Product>();
                    while (result.Read())
                    {
                        productList.Add(CreateProduct(result));
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

        public static void PrintProducts(List<Product> products)
        {
            Console.WriteLine(ConsoleTable.From(products));
        }

        public static void PrintProductList(List<Product> products)
        {
            for (int i = 1; i <= products.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i, products[i - 1].Name));
            }
            Console.WriteLine();
        }
        public static void PrintProductWithPriceRange(Product product, decimal priceMin = 0, decimal priceMax = 1e10m)
        {
            Console.Write("Produkt: {0} -", product.Name);
            var prices = PricesDict(product);
            foreach (KeyValuePair<string, Price> price in prices)
            {
                if (price.Value != null && price.Value.Value >= priceMin && price.Value.Value <= priceMax)
                {
                    Console.Write(string.Format(" {0}: {1}, ", SizeInPolish(price.Key), price.Value.Value));
                }
            }
            Console.WriteLine();
        }

        public static void PrintProductsWithPrices(List<Product> products, decimal priceMin, decimal priceMax)
        {
            foreach (var product in products)
            {
                PrintProductWithPriceRange(product, priceMin, priceMax);
            }
        }

        public static void PrintCategoryList(List<Category> categories)
        {
            for (int i = 1; i <= categories.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i, categories[i - 1].Name));
            }
            Console.WriteLine();
        }

        public static void WaitForReturnToPreviousMenu()
        {
            Console.WriteLine("Naciśnij dowolny klawisz, aby powrócić do poprzedniego menu...");
            Console.ReadKey();
        }
    }
}
