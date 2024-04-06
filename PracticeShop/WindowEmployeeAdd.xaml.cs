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
using System.Xml.Linq;

namespace PracticeShop
{

    public partial class WindowEmployeeAdd : Window
    {
        public WindowEmployeeAdd()
        {
            InitializeComponent();

            BindingDivision();
            BindingPost();
            BindingGender();
        }

        public void BindingDivision()
        {
            Binding binding = new Binding();
            binding.Source = Connection.divisions;
            cbDivision.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectDivision();
        }
        public void BindingPost()
        {
            Binding binding = new Binding();
            binding.Source = Connection.posts;
            cbPost.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectPost();
        }
        public void BindingGender()
        {
            Binding binding = new Binding();
            binding.Source = Connection.genders;
            cbGender.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectGender();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Passport = tbPassport.Text.Trim();
            string FirstName = tbFirstName.Text.Trim();
            string LastName = tbLastName.Text.Trim();
            string Patronymic = tbPatronymic.Text.Trim();
            var DateBirth = tbDate.SelectedDate;
            string Gender = cbGender.Text.Trim();
            string INN = tbINN.Text.Trim();
            string SNILS = tbSNILS.Text.Trim();
            int Division = Int32.Parse(cbDivision.Text.Trim());
            string Post = cbPost.Text.Trim();
            string Login = tbLogin.Text.Trim();
            string Password = tbPassword.Text.Trim();

            NpgsqlCommand cmd = Connection.GetCommand("INSERT INTO \"Employee\" (\"PassportData\",\"FirstName\", \"LastName\", \"Patronymic\", \"DateBirth\", \"Gender\", \"INN\", \"SNILS\", \"Post\", \"Division\", \"Login\", \"Password\")" +
                " VALUES (@passportData, @firstName, @lastName, @patronymic, @dateBirth, @gender, @iNN, @sNILS, @post, @division, @login, @password)");
            cmd.Parameters.AddWithValue("@passportData", NpgsqlDbType.Varchar, Passport);
            cmd.Parameters.AddWithValue("@firstName", NpgsqlDbType.Varchar, FirstName);
            cmd.Parameters.AddWithValue("@lastName", NpgsqlDbType.Varchar, LastName);
            cmd.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Varchar, Patronymic);
            cmd.Parameters.AddWithValue("@dateBirth", NpgsqlDbType.Date, DateBirth);
            cmd.Parameters.AddWithValue("@gender", NpgsqlDbType.Varchar, Gender);
            cmd.Parameters.AddWithValue("@iNN", NpgsqlDbType.Varchar, INN);
            cmd.Parameters.AddWithValue("@sNILS", NpgsqlDbType.Varchar, SNILS);
            cmd.Parameters.AddWithValue("@post", NpgsqlDbType.Varchar, Post);
            cmd.Parameters.AddWithValue("@division", NpgsqlDbType.Integer, Division);
            cmd.Parameters.AddWithValue("@login", NpgsqlDbType.Varchar, Login);
            cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, Password);
            var result = cmd.ExecuteNonQuery();
            if (result == 0)
            {
                return;
            }
            if (result != 0)
            {
                MessageBox.Show("Продукт добавлен");
            }

            Connection.divisions.Clear();
            Connection.posts.Clear();
            Connection.genders.Clear();

            BindingDivision();
            BindingPost();
            BindingGender();
        }
    }
}
