using DBConnection;
using DBConnection.Classes;
using Npgsql;
using NpgsqlTypes;
using PracticeShop.Pages;
using PracticeShop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeShop.Pages
{

    public partial class EntryPage : Page
    {

        public EntryPage()
        {
            InitializeComponent();

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var tblogin = tbLogin.Text.Trim();
            
            if (!tblogin.StartsWith("+7 "))
            {
                NpgsqlCommand cmd = Connection.GetCommand("SELECT \"PassportData\", \"FirstName\", \"LastName\", \"Patronymic\",\"DateBirth\",\"Gender\"," +
            "\"INN\",\"SNILS\", \"Post\", \"Division\", \"Login\", \"Password\" FROM \"Employee\"" +
            "WHERE \"Login\" = @log AND \"Password\" = @pass");
                cmd.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, tbLogin.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, pbPassword.Password.Trim());
                NpgsqlDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    result.Read();

                    Connection.employee = new Employee()
                    {
                        PassportData = result.GetString(0),
                        FirstName = result.GetString(1),
                        LastName = result.GetString(2),
                        Patronymic = result.GetString(3),
                        DateBirth = result.GetDateTime(4),
                        Gender = result.GetString(5),
                        INN = result.GetString(6),
                        SNILS = result.GetString(7),
                        Post = result.GetString(8),
                        Division = result.GetInt32(9),
                        Login = result.GetString(10),
                        Password = result.GetString(11)
                    };
                    result.Close();

                    switch (Connection.employee.Post)
                    {
                        case "Админ":
                            NavigationService.Navigate(new AccountingProduct());
                            break;
                        case "Кадровый инспектор":
                            NavigationService.Navigate(new AccountingProduct());
                            break;
                        case "Кладовщик":
                            NavigationService.Navigate(new AccountingProduct());
                            break;
                        case "Продавец":
                            NavigationService.Navigate(new AccountingProduct());
                            break;
                    }
                }
            }
            if (tblogin.StartsWith("+7 "))
            {
                NpgsqlCommand cmd = Connection.GetCommand("SELECT \"Client\".\"Phone\", \"Client\".\"FirstName\", \"Client\".\"LastName\", \"Client\".\"Patronymic\",\"Client\".\"DateBirth\"," +
                    "\"Client\".\"Password\", \"Client\".\"LoyaltyCard\", \"LoyaltyCard\".\"Bonuses\" FROM \"Client\", \"LoyaltyCard\"" +
                "WHERE \"Client\".\"LoyaltyCard\" = \"LoyaltyCard\".\"NumberCard\" AND \"Phone\" = @log AND \"Password\" = @pass");
                cmd.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, tbLogin.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, pbPassword.Password.Trim());
                NpgsqlDataReader result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    result.Read();

                    Connection.client = new Client()
                    {
                        Phone = result.GetString(0),
                        FirstName = result.GetString(1),
                        LastName = result.GetString(2),
                        Patronymic = result.GetString(3),
                        DateBirth = result.GetDateTime(4),
                        Password = result.GetString(5),
                        LoyaltyCard = result.GetInt32(6),
                        Bonuses = result.GetInt32(7),
                    };
                    result.Close();
                    NavigationService.Navigate(new AccountClient());
                }
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
    }
}


//NpgsqlCommand cmd = Connection.GetCommand("SELECT \"Phone\", \"FirstName\", \"LastName\", \"Patronymic\",\"DateBirth\",\"Password\", \"LoyaltyCard\",  FROM \"Client\", \"LoyaltyCard\"" +
//               "WHERE  \"Phone\" = @log AND \"Password\" = @pass");
//cmd.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, tbLogin.Text.Trim());
//cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, pbPassword.Password.Trim());
//NpgsqlDataReader result = cmd.ExecuteReader();

//if (result.HasRows)
//{
//    result.Read();

//    Connection.client = new Client()
//    {
//        Phone = result.GetString(0),
//        FirstName = result.GetString(1),
//        LastName = result.GetString(2),
//        Patronymic = result.GetString(3),
//        DateBirth = result.GetDateTime(4),
//        Password = result.GetString(5),
//        LoyaltyCard = result.GetInt32(6),
//    };
//    result.Close();
//    NavigationService.Navigate(new AccountClient());