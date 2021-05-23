using BugTrackingApp.service.model;
using BugTrackingApp.ui.utils;
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

namespace BugTrackingApp.ui.view.ticket
{
    /// <summary>
    /// Логика взаимодействия для AssignUser.xaml
    /// </summary>
    public partial class AssignUser : Window
    {
        BugTrackingEntities db;
        public AssignUser()
        {
            InitializeComponent();
            db = new BugTrackingEntities();
            userGrid.ItemsSource = UserUtils.getUsersForProject(ProjectUtils.assignProject.Id);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.SelectedItems.Count != 0)
            {
                User user = userGrid.SelectedItems[0] as User;
                if (user != null)
                {
                    UserUtils.assigneeUser = user;
                    switch (UIUtils.backWindow)
                    {
                        case UIUtils.BackWindow.addTicket:
                            {
                                AddTicket at = new AddTicket();
                                at.Show();
                            }break;
                        case UIUtils.BackWindow.viewTicket:
                            {
                                ViewTicket vt = new ViewTicket();
                                vt.Show();
                            }break;
                    }
                    this.Close();
                }
            }
            else
            {
                WarningMessageLabel.Content = "Пользователь не выбран!";
            }
        }
    }
}
