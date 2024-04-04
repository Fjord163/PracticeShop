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
    public partial class AccountClient : Page
    {
        public AccountClient()
        {
            InitializeComponent();

            tbLastName.Text = Connection.client.LastName;
            tbFirstName.Text = Connection.client.FirstName;
            tbPatronymic.Text = Connection.client.Patronymic;
            tbDateBirth.Text = Connection.client.DateBirth.ToString();
            tbLoyaltyCard.Text = Connection.client.LoyaltyCard.ToString();
            tbPhone.Text = Connection.client.Phone;
            tbBonuses.Text = Connection.card.Bonuses.ToString();
        }
    }
}
