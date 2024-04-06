using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class Sales
    {
        public Sales(int iD, string product, int quantity, DateTime dateTime, int division, string employee, int price, int еotalСost, string client)
        {
            ID = iD;
            Product = product;
            Quantity = quantity;
            DateTime = dateTime;
            Division = division;
            Employee = employee;
            Price = price;
            ЕotalСost = еotalСost;
            Client = client;
        }

        public int ID { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public int Division { get; set; }
        public string Employee { get; set; }
        public int Price { get; set; }
        public int ЕotalСost { get; set; }
        public string Client { get; set; }
    }
}
