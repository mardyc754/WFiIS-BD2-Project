using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Price
    {
        decimal value;

        public Price(decimal value)
        {
            this.value = decimal.Round(value, 2);
        }

        override public string ToString()
        {
            return string.Format("{0}", this != null ? this.value.ToString() : "Brak");
        }

        public decimal Value
        {
            get => this.value;
            set { this.value = value; }
        }
    }
}
