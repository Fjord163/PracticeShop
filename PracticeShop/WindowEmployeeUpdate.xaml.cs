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
    /// <summary>
    /// Логика взаимодействия для WindowEmployeeUpdate.xaml
    /// </summary>
    public partial class WindowEmployeeUpdate : Window
    {
        public WindowEmployeeUpdate()
        {
            InitializeComponent();
            Connection.employees.Clear();
            BindingDivision();
            BindingPost();
            BindingGender();
            BindingLvEmployee();
        }

        public void BindingLvEmployee()
        {
            Binding binding = new Binding();
            binding.Source = Connection.employees;
            lvUpdateemployee.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectEmployee();
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvUpdateemployee.SelectedItem != null)
            {
                tbPassport.Text = (lvUpdateemployee.SelectedItem as Employee).PassportData.ToString();
                tbFirstName.Text = (lvUpdateemployee.SelectedItem as Employee).FirstName.ToString();
                tbLastName.Text = (lvUpdateemployee.SelectedItem as Employee).LastName.ToString();
                tbPatronymic.Text = (lvUpdateemployee.SelectedItem as Employee).Patronymic.ToString();
                tbDate.Text = (lvUpdateemployee.SelectedItem as Employee).DateBirth.ToString();
                cbGender.Text = (lvUpdateemployee.SelectedItem as Employee).Gender.ToString();
                tbINN.Text = (lvUpdateemployee.SelectedItem as Employee).INN.ToString();
                tbSNILS.Text = (lvUpdateemployee.SelectedItem as Employee).SNILS.ToString();
                cbDivision.Text = (lvUpdateemployee.SelectedItem as Employee).Division.ToString();
                cbPost.Text = (lvUpdateemployee.SelectedItem as Employee).Post.ToString();
                tbLogin.Text = (lvUpdateemployee.SelectedItem as Employee).Login.ToString();
                tbPassword.Text = (lvUpdateemployee.SelectedItem as Employee).Password.ToString();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string EmployeePass = (lvUpdateemployee.SelectedItem as Employee).PassportData;

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

            NpgsqlCommand cmd = Connection.GetCommand("UPDATE \"Employee\" SET \"FirstName\"= @firstName, \"LastName\"= @lastName, \"Patronymic\"= @patronymic," +
                " \"DateBirth\"= @dateBirth, \"Gender\"= @gender, \"INN\"= @iNN, \"SNILS\"= @sNILS, \"Division\"= @division, \"Post\"= @post, \"Login\"= @login, \"Password\"= @password  where \"PassportData\" = @passportData");
            cmd.Parameters.AddWithValue("@passportData", NpgsqlDbType.Varchar, Passport);
            cmd.Parameters.AddWithValue("@firstName", NpgsqlDbType.Varchar, FirstName);
            cmd.Parameters.AddWithValue("@lastName", NpgsqlDbType.Varchar, LastName);
            cmd.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Varchar, Patronymic);
            cmd.Parameters.AddWithValue("@dateBirth", NpgsqlDbType.Date, DateBirth);
            cmd.Parameters.AddWithValue("@gender", NpgsqlDbType.Varchar, Gender);
            cmd.Parameters.AddWithValue("@iNN", NpgsqlDbType.Varchar, INN);
            cmd.Parameters.AddWithValue("@sNILS", NpgsqlDbType.Varchar, SNILS);
            cmd.Parameters.AddWithValue("@division", NpgsqlDbType.Integer, Division);
            cmd.Parameters.AddWithValue("@post", NpgsqlDbType.Varchar, Post);
            cmd.Parameters.AddWithValue("@login", NpgsqlDbType.Varchar, Login);
            cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, Password);
            var result = cmd.ExecuteNonQuery();
            if (result != 0)
            {
                MessageBox.Show("Данные изменены");
            }
            else { MessageBox.Show("Данные не изменены"); }
            Connection.employees.Clear();
            Connection.divisions.Clear();
            Connection.posts.Clear();
            Connection.genders.Clear();
            BindingDivision();
            BindingPost();
            BindingGender();
            BindingLvEmployee();
        }
    }
}
