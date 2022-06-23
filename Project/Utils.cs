using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Price
    {
        string type;
        decimal value;

        public Price(decimal value, string type = "")
        {
            this.type = type;
            this.value = decimal.Round(value, 2);
        }

        override public string ToString()
        {
            return string.Format("{0}", this != null ? this.value.ToString() : "Brak");
        }

        public decimal Value
        {
            get => this.value;
            set { this.value = value;  }
        }
    }

    public class Vegetarian
    {
        bool isVegetarian;

        public Vegetarian(bool vegetarian)
        {
            isVegetarian = vegetarian;
        }

        public bool IsVegetarian
        {
            get => isVegetarian;
            set
            {
                isVegetarian = value;
            }
        }
        override public string ToString()
        {
            return string.Format("{0}", this != null ? (this.isVegetarian ? "Tak" : "Nie") : "Nie dotyczy");
        }
    }
}
