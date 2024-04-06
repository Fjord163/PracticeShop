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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountingEmployee.xaml
    /// </summary>
    public partial class AccountingEmployee : Page
    {
        public static WindowEmployeeAdd addEmployee;
        public static WindowEmployeeUpdate updateEmployee;

        public AccountingEmployee()
        {
            InitializeComponent();

            BindingLvEmployee();
        }

        public void BindingLvEmployee()
        {
            Binding binding = new Binding();
            binding.Source = Connection.employees;
            lvEmployee.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectEmployee();
        }

        private void Filter()
        {
            string searchString = Search.Text.Trim();

            var view = CollectionViewSource.GetDefaultView(lvEmployee.ItemsSource);
            view.Filter = new Predicate<object>(o =>
            {
                Employee employee = o as Employee;
                if (employee == null) { return false; }

                bool isVisible = true;

                if (searchString.Length > 0)
                {
                    isVisible = employee.FirstName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                        employee.LastName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                        employee.Patronymic.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                        employee.PassportData.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                        employee.Post.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1;
                }
                return isVisible;
            });
        }



        private void Search_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (addEmployee == null || !addEmployee.IsVisible)
            {
                addEmployee = new WindowEmployeeAdd();
                addEmployee.ShowDialog();
            }
            else addEmployee.Activate();
            Connection.employees.Clear();
            BindingLvEmployee();
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (updateEmployee == null || !updateEmployee.IsVisible)
            {
                updateEmployee = new WindowEmployeeUpdate();
                updateEmployee.ShowDialog();
            }
            else addEmployee.Activate();
            
        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = lvEmployee.SelectedItem as Employee;

            NpgsqlCommand cmd = Connection.GetCommand("DELETE FROM \"Employee\" WHERE \"PassportData\" = @passportData");
            cmd.Parameters.AddWithValue("@passportData", NpgsqlDbType.Varchar, selectedEmployee.PassportData);
            cmd.Parameters.AddWithValue("@firstName", NpgsqlDbType.Varchar, selectedEmployee.FirstName);
            cmd.Parameters.AddWithValue("@lastName", NpgsqlDbType.Varchar, selectedEmployee.LastName);
            cmd.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Varchar, selectedEmployee.Patronymic);
            cmd.Parameters.AddWithValue("@dateBirth", NpgsqlDbType.Date, selectedEmployee.DateBirth);
            cmd.Parameters.AddWithValue("@gender", NpgsqlDbType.Varchar, selectedEmployee.Gender);
            cmd.Parameters.AddWithValue("@iNN", NpgsqlDbType.Varchar, selectedEmployee.INN);
            cmd.Parameters.AddWithValue("@sNILS", NpgsqlDbType.Varchar, selectedEmployee.SNILS);
            cmd.Parameters.AddWithValue("@post", NpgsqlDbType.Varchar, selectedEmployee.Post);
            cmd.Parameters.AddWithValue("@division", NpgsqlDbType.Integer, selectedEmployee.Division);
            cmd.Parameters.AddWithValue("@login", NpgsqlDbType.Varchar, selectedEmployee.Login);
            cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, selectedEmployee.Password);
            var result = cmd.ExecuteScalar();
            if (result == null)
            {
                MessageBox.Show("Сотрудник убран");
            }
            if (result != null)
            {
                MessageBox.Show("Сотрудника не получилось удалить");
                return;
            }
            Connection.employees.Clear();
            BindingLvEmployee();
        }
    }
}
