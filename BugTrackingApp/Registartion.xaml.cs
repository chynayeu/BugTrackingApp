using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using BugTrackingApp.service.model;
using BugTrackingApp.ui.model;
using BugTrackingApp.ui;

namespace BugTrackingApp
{
    /// <summary>
    /// Interaction logic for Registartion.xaml
    /// </summary>
    public partial class Registartion : Window
    {
        RegisterUser regUser;
        public Registartion()
        {
            InitializeComponent();
            regUser = new RegisterUser();
            Register_Layout.DataContext = regUser;

        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public void Reset()
        {
            regUser.Reset();
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(!validateUser())
            {
                return;
            }
            if (passwordBox1.Password.Length == 0)
            {
                errormessage.Text = "Введите пароль.";
                passwordBox1.Focus();
            }
            else if (passwordBoxConfirm.Password.Length == 0)
            {
                errormessage.Text = "Введите пароль подтверждения";
                passwordBoxConfirm.Focus();
            }
            else if (passwordBox1.Password != passwordBoxConfirm.Password)
            {
                errormessage.Text = "Пароли отличаются";
                passwordBoxConfirm.Focus();
            }
            else
            {
                if(!UserUtils.addUser(regUser.Name, regUser.Surname, regUser.Email, passwordBox1.Password))
                {
                    errormessage.Text = "Произошла ошибка! Попробуйте позже!";
                }
                errormessage.Text = "Вы зарегистрированы! Можете войти со своим имейл/паролем.";
                Reset();
            }
        }

        private bool validateUser()
        {
            if (string.IsNullOrWhiteSpace(regUser.Name))
            {
                errormessage.Text = "Введите имя";
                tbName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(regUser.Surname))
            {
                errormessage.Text = "Введите фамилию";
                tbSurname.Focus();
                return false;
            }
            if (!ValidationUtils.isEmailValid(regUser.Email))
            {
                errormessage.Text = "Неверный формат или пустой имейл";
                tbEmail.Focus();
                return false;
            }
            return true;
        }
    }
}

