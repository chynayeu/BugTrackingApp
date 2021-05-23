using BugTrackingApp.service.model;
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

namespace BugTrackingApp.ui.view.user
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser()
        {
            InitializeComponent();
            if (UserUtils.editUser is null)
            {
                backToAdmin();
            }
            lbUserTitle.Content ="Редактирование пользователя " + UserUtils.editUser.name + " " + UserUtils.editUser.surname;
            Email.Text = UserUtils.editUser.email;
            cbRole.ItemsSource = Enum.GetValues(typeof(UserUtils.UserRole));
            cbRole.SelectedItem = (UserUtils.UserRole)UserUtils.editUser.role;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            backToAdmin();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(cbRole.SelectedIndex != -1)
            {
                UserUtils.editUser.role = (int)cbRole.SelectedItem;
                UserUtils.updateUser(UserUtils.editUser);
                WarningMessageLable.Content = "Пользователь обновлён!";
                return;
            }
            WarningMessageLable.Content = "Роль не должна быть пустой!";
        }

        private void backToAdmin()
        {
            UserUtils.editUser = null;
            Administration admin = new Administration();
            admin.Show();
            this.Close();
        }
    }
}
