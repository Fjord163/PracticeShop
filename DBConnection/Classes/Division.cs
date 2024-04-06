using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class Division
    {
        public Division(int iD, string streetName, int buildingNumber)
        {
            ID = iD;
            StreetName = streetName;
            BuildingNumber = buildingNumber;
        }

        public int ID { get; set; }
        public string StreetName { get; set; }
        public int BuildingNumber { get; set; }
    }
}
