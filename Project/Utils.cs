using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Price
    {
        string type;
        decimal value;

        public Price(string type, decimal value)
        {
            this.type = type;
            this.value = decimal.Round(value, 2);
        }

        override public string ToString()
        {
            return string.Format("{0}", this != null ? this.value.ToString() : "Brak");
        }
    }

    class Vegetarian
    {
        bool isVegetarian;

        public Vegetarian(bool vegetarian)
        {
            this.isVegetarian = vegetarian;
        }

        override public string ToString()
        {
            return string.Format("{0}", this != null ? (this.isVegetarian ? "Tak" : "Nie") : "Nie dotyczy");
        }
    }
}
