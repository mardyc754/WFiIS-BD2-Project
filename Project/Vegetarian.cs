using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
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
