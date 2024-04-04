using DBConnection.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()

        {
            InitializeComponent();

            Connection.SelectLoyaltyCard();
            Connection.SelectLastNumberLoyaltyCard();
         }
         
        public void CreateCard()
        {
            var lastNumberCard = Connection.loyaltyCard.NumberCard + 1;

            NpgsqlCommand cmd = Connection.GetCommand("INSERT INTO \"LoyaltyCard\" (\"NumberCard\",\"Bonuses\") VALUES (@numberCard, @bonuses)");
            cmd.Parameters.AddWithValue("@numberCard", NpgsqlDbType.Integer, lastNumberCard);
            cmd.Parameters.AddWithValue("@bonuses", NpgsqlDbType.Integer, 0);
            cmd.ExecuteNonQuery();
        }

        public void DeleteLastNumberCard()
        {
            var lastNumberCard = Connection.loyaltyCard.NumberCard + 1;

            NpgsqlCommand cmd = Connection.GetCommand("DELETE FROM \"LoyaltyCard\" WHERE \"NumberCard\" = @id ");
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, lastNumberCard);
            cmd.Parameters.AddWithValue("@group", NpgsqlDbType.Integer, 0);
            var result = cmd.ExecuteScalar();
        }
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateCard();

                var lastNumberCard = Connection.loyaltyCard.NumberCard + 1;
                var phone = tbPhone.Text.Trim();
                var firstName = tbFirstName.Text.Trim();
                var lastName = tbLastName.Text.Trim();
                var patronymic = tbPatronymic.Text.Trim();
                var dateBirth = dpDateBirth.SelectedDate;
                var password = tbPassword.Text.Trim();
            
                NpgsqlCommand cmd = Connection.GetCommand("INSERT INTO \"Client\" (\"FirstName\",\"LastName\",\"Patronymic\",\"DateBirth\",\"Password\",\"LoyaltyCard\",\"Phone\") " +
                    "VALUES (@firstName, @lastName, @patronymic, @dateBirth, @password, @loyaltyCard, @phone)");
                cmd.Parameters.AddWithValue("@firstName", NpgsqlDbType.Varchar, firstName);
                cmd.Parameters.AddWithValue("@lastName", NpgsqlDbType.Varchar, lastName);
                cmd.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Varchar, patronymic);
                cmd.Parameters.AddWithValue("@dateBirth", NpgsqlDbType.Date, dateBirth);
                cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, password);
                cmd.Parameters.AddWithValue("@loyaltyCard", NpgsqlDbType.Integer, lastNumberCard);
                cmd.Parameters.AddWithValue("@phone", NpgsqlDbType.Varchar, phone);
                var result = cmd.ExecuteNonQuery();
                if (result != 0)
                {
                    MessageBox.Show("Вы зарегестрировались");

                    cmd = Connection.GetCommand("SELECT \"Phone\", \"FirstName\", \"LastName\", \"Patronymic\",\"DateBirth\",\"Password\", \"LoyaltyCard\" FROM \"Client\"" +
                    "WHERE \"Phone\" = @log AND \"Password\" = @pass");
                    cmd.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, phone);
                    cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, password);
                    var res = cmd.ExecuteReader();

                    if (res.HasRows)
                    {
                        res.Read();

                        Connection.client = new Client()
                        {
                            Phone = res.GetString(1),
                            FirstName = res.GetString(1),
                            LastName = res.GetString(2),
                            Patronymic = res.GetString(3),
                            DateBirth = res.GetDateTime(4),
                            Password = res.GetString(5),
                            LoyaltyCard = res.GetInt32(6),
                        };
                        res.Close();
                        NavigationService.Navigate(new AccountingProduct());
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Проверьте поля");
                DeleteLastNumberCard();
            }
        }
    }
}
