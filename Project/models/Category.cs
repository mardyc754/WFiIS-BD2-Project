using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Category
    {
        private int categoryID;
        private string name;

        public Category(int categoryID, string name)
        {
            this.categoryID = categoryID;
            this.name = name;
        }

        public string Name { get => name; }
        public int ID { get => categoryID; }


        public override string ToString()
        {
            return string.Format("{0} | {1}", this.categoryID, this.name);
        }
    }
}
