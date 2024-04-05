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
using System.Windows.Shapes;

namespace PracticeShop
{
    public partial class WindowAddProduct : Window
    {
        public WindowAddProduct()
        {
            InitializeComponent();

            Connection.SelectLastArticleProduct();

            BindingUnit();
        }

        public void BindingUnit()
        {
            Binding binding = new Binding();
            binding.Source = Connection.unitMeasures;
            cbAddUnit.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectUnitMeasure();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int article = Int32.Parse(Connection.product.Article);
            int nextArticle = article + 1;

            
            string name = tbNameAddProduct.Text.Trim();
            string unit = cbAddUnit.Text.Trim();

            NpgsqlCommand cmd = Connection.GetCommand("INSERT INTO \"Product\" (\"Article\",\"Name\", \"Unit\") VALUES (@article, @name, @unit)");
            cmd.Parameters.AddWithValue("@article", NpgsqlDbType.Varchar, nextArticle.ToString());
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, name);
            cmd.Parameters.AddWithValue("@unit", NpgsqlDbType.Varchar, unit);
            var result = cmd.ExecuteNonQuery();
            if (result == 0)
            {
                return;
            }
            if (result != 0)
            {
                MessageBox.Show("Продукт добавлен");
            }

        }
    }
}
