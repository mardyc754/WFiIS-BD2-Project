using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Product
    {
        int id;
        string name;
        bool vegetarian;
        double priceSmall;
        double priceMedium;
        double priceLarge;
        int categoryID;
        string categoryName;

        public Product(
            int id, 
            string name, 
            bool vegetarian, 
            double priceSmall, 
            double priceMedium, 
            double priceLarge, 
            int categoryID, 
            string categoryName)
        {
            this.id = id;
            this.name = name;
            this.vegetarian = vegetarian;
            this.priceSmall = priceSmall;
            this.priceMedium = priceMedium;
            this.priceLarge = priceLarge;
            this.categoryID = categoryID;
            this.categoryName = categoryName;
        }

        public void Print()
        {
            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                this.id, this.name, this.vegetarian, this.priceSmall, 
                this.priceMedium, this.priceLarge, this.categoryID, this.categoryName);
        }
    }
}
