using System.Windows;
using BugTrackingApp.ui;
using BugTrackingApp.service.model;
using BugTrackingApp.utils;
using BugTrackingApp.ui.view;

namespace BugTrackingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Logger.InitLogger();
        }
        Registartion registration = new Registartion();
     //   Welcome welcome = new Welcome();
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Введите имейл.";
                textBoxEmail.Focus();
            }
            else if (!ValidationUtils.isEmailValid(textBoxEmail.Text))
            {
                errormessage.Text = "Введённый имейл неверного формата.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                User user = UserUtils.checkUserLogin(textBoxEmail.Text, passwordBox1.Password);
                if (user == null)
                {
                    errormessage.Text = "Неверный имейл и/или пароль!";
                }
                else
                {
                    UserUtils.currentUser = user;
                    switch (user.role)
                    {
                        
                        case (int) UserUtils.UserRole.admin:
                            Logger.Log.Info("Вошел админ : " + user.email);
                            Administration admin = new Administration();
                            admin.Show();
                            break;
                        case (int)UserUtils.UserRole.def:
                        default:
                            HomeWindow hw = new HomeWindow();
                            hw.Show();
                            break;
                    }
                    this.Close();
                }
                
            }
        }
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            this.Close();
        }
    }
}
