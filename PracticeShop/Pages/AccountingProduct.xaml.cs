using DBConnection;
using DBConnection.Classes;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
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
using static System.Net.Mime.MediaTypeNames;

namespace PracticeShop.Pages
{
    public partial class AccountingProduct : Page
    {
        public static WindowAddProduct addProduct;
        public static WindowUpdateProduct updateProduct;


        public AccountingProduct()
        {
            InitializeComponent();

            BindingLvProduct();


        }

        public void BindingLvProduct()
        {
            Binding binding = new Binding();
            binding.Source = Connection.products;
            lvProduct.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectProduct();
        }

        private void Filter()
        {
            string searchString = Search.Text.Trim();

            var view = CollectionViewSource.GetDefaultView(lvProduct.ItemsSource);
            view.Filter = new Predicate<object>(o =>
            {
                Product prod = o as Product;
                if (prod == null) { return false; }

                bool isVisible = true;

                if (searchString.Length > 0)
                {
                    isVisible = prod.Article.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                        prod.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                        prod.Unit.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1;
                }
                return isVisible;
            });
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if(addProduct == null || !addProduct.IsVisible)
            {
                addProduct = new WindowAddProduct();
                addProduct.ShowDialog();
            }
            else addProduct.Activate();
            Connection.products.Clear();
            BindingLvProduct();
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (updateProduct == null || !updateProduct.IsVisible)
            {
                updateProduct = new WindowUpdateProduct();
                updateProduct.ShowDialog();
            }
            else updateProduct.Activate();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = lvProduct.SelectedItem as Product;

            NpgsqlCommand cmd = Connection.GetCommand("DELETE FROM \"Product\" WHERE \"Article\" = @article");
            cmd.Parameters.AddWithValue("@article", NpgsqlDbType.Varchar, selectedProduct.Article);
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, selectedProduct.Name);
            cmd.Parameters.AddWithValue("@unit", NpgsqlDbType.Varchar, selectedProduct.Unit);
            var result = cmd.ExecuteScalar();
            if (result == null)
            {
                MessageBox.Show("Товар удален");
            }
            if (result != null)
            {
                MessageBox.Show("Товар не получилось удалить");
                return;
            }
            Connection.products.Clear();
            BindingLvProduct();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sales());
        }
    }
}
