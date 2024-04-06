using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class SalesExtendedVersion
    {
        public SalesExtendedVersion(int iD, string product, string nameProduct, int quantity, DateTime dateTime, int division, string streetName, int buildingNumber, string employee, string firstNameEmployee, string lastNameEmployee, string patronymicEmployee, int price, int еotalСost, string client, string firstNameClient, string lastNameClient, string patronymicClient)
        {
            ID = iD;
            Product = product;
            NameProduct = nameProduct;
            Quantity = quantity;
            DateTime = dateTime;
            Division = division;
            StreetName = streetName;
            BuildingNumber = buildingNumber;
            Employee = employee;
            FirstNameEmployee = firstNameEmployee;
            LastNameEmployee = lastNameEmployee;
            PatronymicEmployee = patronymicEmployee;
            Price = price;
            ЕotalСost = еotalСost;
            Client = client;
            //FirstNameClient = firstNameClient;
            //LastNameClient = lastNameClient;
            //PatronymicClient = patronymicClient;
        }

        public int ID { get; set; }
        public string Product { get; set; }
        public string NameProduct { get; set; } //продукт
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public int Division { get; set; }
        public string StreetName { get; set; } // улица
        public int BuildingNumber { get; set; } // номер улицы
        public string Employee { get; set; } 
        public string FirstNameEmployee { get; set; } 
        public string LastNameEmployee { get; set; } 
        public string PatronymicEmployee { get; set; } 
        public int Price { get; set; }
        public int ЕotalСost { get; set; }
        public string Client { get; set; }
        //public string FirstNameClient { get; set; }
        //public string LastNameClient { get; set; }
        //public string PatronymicClient { get; set; }
    }
}
