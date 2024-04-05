using DBConnection;
using DBConnection.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
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
    public partial class AccountingProduct : Page
    {
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

        public void ApplyFilterForm()
        {

            ICollectionView view = CollectionViewSource.GetDefaultView(lvProduct.ItemsSource);
            if (view == null) { return; }

            if (view.CanFilter == true)
            {
                view.Filter = new Predicate<object>(o =>
                {
                    Product  prod = o as Product;
                    if (prod == null) return false;

                    return prod.Article == Connection.product.Article;
                });
            }
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
    }
}
