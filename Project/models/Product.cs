using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Project
{
    public class Product
    {
        int productID;
        string name;
        Vegetarian vegetarian;
        Price priceSmall;
        Price priceMedium;
        Price priceLarge;
        int categoryID;
        string categoryName;

        public Product(
            int productID, 
            string name, 
            Vegetarian vegetarian, 
            Price priceSmall, 
            Price priceMedium, 
            Price priceLarge, 
            int categoryID, 
            string categoryName)
        {
            this.productID = productID;
            this.name = name;
            this.vegetarian = vegetarian;
            this.priceSmall = priceSmall;
            this.priceMedium = priceMedium;
            this.priceLarge = priceLarge;
            this.categoryID = categoryID;
            this.categoryName = categoryName;
        }

        public int ID { get => productID; }
        public string Name { get => name; }
        public Vegetarian Vegetarian { get => vegetarian; }
        public Price PriceSmall { get => priceSmall; }
        public Price PriceMedium { get => priceMedium; }
        public Price PriceLarge { get => priceLarge; }
        public string Category { get => categoryName; }
        public int CategoryID { get => categoryID; }


        public void Print()
        {
            string result = string.Format("Nazwa: {0}\n", this.name);
            result += string.Format("Kategoria: {0}\n", this.categoryName);
            result += string.Format("Wegetariański: {0}\n", this.vegetarian);
            result += string.Format("Cena: {0}\n", this.vegetarian);

            Console.WriteLine(result);
        }

        public override string ToString()
        {
            return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                this.productID, this.name, 
                this.vegetarian, 
                this.priceSmall,
                this.priceMedium, 
                this.priceLarge, 
                this.categoryID, this.categoryName);
        }

        public static Product CreateProduct(SqlDataReader result)
        {
            Vegetarian isVegetarian = !result["vegetarian"].Equals(DBNull.Value) ?
                new Vegetarian((bool)result["vegetarian"]) : null;

            Price priceSmall = !result["priceSmall"].Equals(DBNull.Value) ?
                new Price((decimal)result["priceSmall"]) : null;

            Price priceLarge = !result["priceLarge"].Equals(DBNull.Value) ?
                new Price((decimal)result["priceLarge"]) : null;

            Price priceMedium = new Price((decimal)result["priceMedium"]);

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
    }
}
