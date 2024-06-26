﻿using DBConnection.Classes;
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
    public partial class Sales : Page
    {
        public Sales()
        {
            InitializeComponent();
            BindingLvSales();
        }

        public void BindingLvSales()
        {
            Binding binding = new Binding();
            binding.Source = Connection.sales;
            lvSales.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectSales();
        }
    }
}
