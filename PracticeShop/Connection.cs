using DBConnection;
using DBConnection.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeShop
{
    public class Connection
    {
        
        public static Employee employee;
        public static Client client;
        public static CreateLoyaltyCard loyaltyCard;
        public static LoyaltyCard card;


        public static NpgsqlConnection connection;

        public static void Connect(string host, string port, string user, string pass, string database)
        {
            string cs = string.Format("Server = {0}; Port = {1}; User Id = {2}; Password = {3}; Database = {4}", host, port, user, pass, database);

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }

        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = sql;
            return command;
        }
        public static ObservableCollection<Product> products { get; set; } = new ObservableCollection<Product>();
        public static ObservableCollection<Employee> employees { get; set; } = new ObservableCollection<Employee>();
        public static ObservableCollection<Client> clients { get; set; } = new ObservableCollection<Client>();
        public static ObservableCollection<CreateLoyaltyCard> loyaltyCards { get; set; } = new ObservableCollection<CreateLoyaltyCard>();
        public static ObservableCollection<LoyaltyCard> cards { get; set; } = new ObservableCollection<LoyaltyCard>();

        public static void SelectProduct()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Article\", \"Name\", \"Unit\" FROM \"Product\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    products.Add(new Product(
                            result.GetString(0),
                            result.GetString(1),
                            result.GetString(2)
                        ));
                }
            }
            result.Close();
        }

        public static void SelectLoyaltyCard()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"NumberCard\", \"Bonuses\" FROM \"LoyaltyCard\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    cards.Add(new LoyaltyCard(
                            result.GetInt32(0),
                            result.GetInt32(1)
                        ));
                }
            }
            result.Close();
        }
        public static void SelectLastNumberLoyaltyCard()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"NumberCard\", \"Bonuses\" FROM \"LoyaltyCard\" ORDER BY \"NumberCard\" DESC LIMIT 1");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                result.Read();
                loyaltyCard = new CreateLoyaltyCard()
                {
                    NumberCard = result.GetInt32(0),
                };
            }
            result.Close();
        }

        public static void SelectEmployee()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"PassportData\", \"FirstName\", \"LastName\", \"Patronymic\", \"DateBirth\", \"Gender\", \"INN\", \"SNILS\", \"Post\", \"Division\", \"Login\", \"Password\" FROM \"Employee\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    employees.Add(new Employee(
                            result.GetString(0),
                            result.GetString(1),
                            result.GetString(2),
                            result.GetString(3),
                            result.GetDateTime(4),
                            result.GetString(5),
                            result.GetString(6),
                            result.GetString(7),
                            result.GetString(8),
                            result.GetInt32(9),
                            result.GetString(10),
                            result.GetString(11)
                        ));
                }
            }
            result.Close();
        }
    }
}
