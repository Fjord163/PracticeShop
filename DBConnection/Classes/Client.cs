using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class Client
    {
        public Client() { }
        public Client(string phone, string firstName, string lastName, string patronymic, DateTime dateBirth, string password, int loyaltyCard)
        {
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            DateBirth = dateBirth;
            Password = password;
            LoyaltyCard = loyaltyCard;
        }

        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirth { get; set; }
        public string Password { get; set; }
        public int LoyaltyCard { get; set; }
    }
}
