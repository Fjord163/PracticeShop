using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class Employee
    {
        public Employee() { }
        public Employee(string passportData, string firstName, string lastName, string patronymic, DateTime dateBirth, string gender, string iNN, string sNILS, string post, int division, string login, string password)
        {
            PassportData = passportData;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            DateBirth = dateBirth;
            Gender = gender;
            INN = iNN;
            SNILS = sNILS;
            Post = post;
            Division = division;
            Login = login;
            Password = password;
        }

        public string PassportData { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }
        public string INN { get; set; }
        public string SNILS { get; set; }
        public string Post { get; set; }
        public int Division { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
