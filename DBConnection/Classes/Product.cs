using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class Product
    {
        public Product(string article, string name, string unit)
        {
            Article = article;
            Name = name;
            Unit = unit;
        }

        public string Article { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
