using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Printer
    {
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
            var prices = PriceHandler.PricesDict(product);
            foreach (KeyValuePair<string, Price> price in prices)
            {
                if (price.Value != null && price.Value.Value >= priceMin && price.Value.Value <= priceMax)
                {
                    Console.Write(string.Format(" {0}: {1}, ", PriceHandler.SizeInPolish(price.Key), price.Value.Value));
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
    }
}
