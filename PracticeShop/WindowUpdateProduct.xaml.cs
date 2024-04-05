using DBConnection.Classes;
using Npgsql;
using NpgsqlTypes;
using PracticeShop.Pages;
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
using System.Windows.Shapes;

namespace PracticeShop
{
    /// <summary>
    /// Логика взаимодействия для WindowUpdateProduct.xaml
    /// </summary>
    public partial class WindowUpdateProduct : Window
    {

        public WindowUpdateProduct()
        {
            InitializeComponent();
            Connection.products.Clear();
            Connection.unitMeasures.Clear();
            BindingUnit();
            BindingLvProduct();
        }

        public void BindingLvProduct()
        {
            Binding binding = new Binding();
            binding.Source = Connection.products;
            lvNameProduct.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectProduct();
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
            string productsArticle = (lvNameProduct.SelectedItem as Product).Article;

            string name = tbNameAddProduct.Text.Trim();
            string unit = cbAddUnit.Text.Trim();

            NpgsqlCommand cmd = Connection.GetCommand("UPDATE \"Product\" SET \"Name\"= @name, \"Unit\"= @unit  where \"Article\" = @article");
            cmd.Parameters.AddWithValue("@article", NpgsqlDbType.Varchar, productsArticle);
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, name);
            cmd.Parameters.AddWithValue("@unit", NpgsqlDbType.Varchar, unit);
            var result = cmd.ExecuteNonQuery();
            if (result != 0)
            {
                MessageBox.Show("Данные изменены");
            }
            else { MessageBox.Show("Данные не изменены"); }
            Connection.products.Clear();
            Connection.unitMeasures.Clear();
            BindingUnit();
            BindingLvProduct();
        }

        private void lvNameProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvNameProduct.SelectedItem != null)
            {
                tbNameAddProduct.Text = (lvNameProduct.SelectedItem as Product).Name.ToString();
                cbAddUnit.Text = (lvNameProduct.SelectedItem as Product).Unit.ToString();
            }
        }
    }
}
