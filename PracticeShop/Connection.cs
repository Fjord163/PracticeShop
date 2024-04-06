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
        public static Product product;
        public static Product product2;

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
        public static ObservableCollection<UnitMeasure> unitMeasures { get; set; } = new ObservableCollection<UnitMeasure>();
        public static ObservableCollection<Sales> sales { get; set; } = new ObservableCollection<Sales>();
        public static ObservableCollection<SalesExtendedVersion> salesV2 { get; set; } = new ObservableCollection<SalesExtendedVersion>();
        public static ObservableCollection<Post> posts { get; set; } = new ObservableCollection<Post>();
        public static ObservableCollection<Gender> genders { get; set; } = new ObservableCollection<Gender>();
        public static ObservableCollection<Division> divisions { get; set; } = new ObservableCollection<Division>();

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

        public static void SelectUnitMeasure()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Name\" FROM \"UnitMeasure\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    unitMeasures.Add(new UnitMeasure(
                            result.GetString(0)
                        ));
                }
            }
            result.Close();
        }
        public static void SelectGender()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Name\" FROM \"Gender\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    genders.Add(new Gender(
                            result.GetString(0)
                        ));
                }
            }
            result.Close();
        }
        public static void SelectDivision()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"ID\",\"StreetName\",\"BuildingNumber\" FROM \"Division\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    divisions.Add(new Division(
                            result.GetInt32(0),
                            result.GetString(1),
                            result.GetInt32(2)
                        ));
                }
            }
            result.Close();
        }
        public static void SelectPost()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Name\" FROM \"Position\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    posts.Add(new Post(
                            result.GetString(0)
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

        public static void SelectLastArticleProduct()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Article\", \"Name\", \"Unit\" FROM \"Product\" ORDER BY \"Article\" DESC LIMIT 1");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                result.Read();
                product = new Product()
                {
                    Article = result.GetString(0),
                    Name = result.GetString(1),
                    Unit = result.GetString(2)
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

        public static void SelectSales()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"ID\", \"Product\", \"Quantity\", \"DateTime\", \"Division\", \"Employee\", \"Price\", \"ЕotalСost\", \"Client\" FROM \"Sales\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    sales.Add(new Sales(
                            result.GetInt32(0),
                            result.GetString(1),
                            result.GetInt32(2),
                            result.GetDateTime(3),
                            result.GetInt32(4),
                            result.GetString(5),
                            result.GetInt32(6),
                            result.GetInt32(7),
                            result.GetString(8)
                        ));
                }
            }
            result.Close();
        }

        public static void SelectSalesV2()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Sales\".\"ID\", \"Sales\".\"Product\", \"ProductStock\".\"Product\", \"Sales\".\"Quantity\", \"Sales\".\"DateTime\", \"Sales\".\"Division\", " +
                "\"Division\".\"StreetName\",\"Division\".\"BuildingNumber\", \"Sales\".\"Employee\",\"Employee\".\"FirstName\",\"Employee\".\"LastName\", \"Employee\".\"Patronymic\", " +
                "\"Sales\".\"Price\", \"Sales\".\"ЕotalСost\", \"Sales\".\"Client\",\"Client\".\"FirstName\",\"Client\".\"LastName\",\"Client\".\"Patronymic\" FROM \"Sales\", \"ProductStock\", \"Division\", \"Employee\", \"Client\"");
                NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    salesV2.Add(new SalesExtendedVersion(
                            result.GetInt32(0),
                            result.GetString(1),
                            result.GetString(2),
                            result.GetInt32(3),
                            result.GetDateTime(4),
                            result.GetInt32(5),
                            result.GetString(6),
                            result.GetInt32(7),
                            result.GetString(8),
                            result.GetString(9),
                            result.GetString(10),
                            result.GetString(11),
                            result.GetInt32(12),
                            result.GetInt32(13),
                            result.GetString(14),
                            result.GetString(15),
                            result.GetString(16),
                            result.GetString(17)
                        ));
                }
            }
            result.Close();
        }
    }
}
