using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Product
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
        public void Print()
        {
            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                this.productID, this.name, this.vegetarian, this.priceSmall, 
                this.priceMedium, this.priceLarge, this.categoryID, this.categoryName);
        }

        public override string ToString()
        {
            //string isVegetarian = this.vegetarian != null ? this.vegetarian.ToString() : "Nie dotyczy";
            //string priceSmallInfo = this.priceSmall != null ? this.priceSmall.ToString() : "Brak";
            //string priceLargeInfo = this.priceLarge != null ? this.priceLarge.ToString() : "Brak";


            return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                this.productID, this.name, 
                this.vegetarian, 
                this.priceSmall,
                this.priceMedium, 
                this.priceLarge, 
                this.categoryID, this.categoryName);
        }
    }

    class Category
    {
        private int categoryID;
        private string name;

        public Category(int categoryID, string name)
        {
            this.categoryID = categoryID;
            this.name = name;
        }

        public string Name { get => name; }
        public int CategoryID { get => categoryID; }


        public override string ToString()
        {
            return string.Format("{0} | {1}", this.categoryID, this.name);
        }
    }
}
