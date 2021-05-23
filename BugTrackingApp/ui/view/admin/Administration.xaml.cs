using BugTrackingApp.service.model;
using System.Windows;
using System.Data.Entity;
using BugTrackingApp.ui.view.user;
using BugTrackingApp.ui.view.project;
using BugTrackingApp.ui.utils;

namespace BugTrackingApp
{
    /// <summary>
    /// Логика взаимодействия для Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        BugTrackingEntities db;
        public Administration()
        {
            InitializeComponent();
            UserName.Text = UserUtils.currentUser.name + " " + UserUtils.currentUser.surname;
            db = new BugTrackingEntities();
            db.Users.Load();
            db.Projects.Load();
            userGrid.ItemsSource = db.Users.Local.ToBindingList();
            projectGrid.ItemsSource = db.Projects.Local.ToBindingList();
            assignProjectGrid.ItemsSource = db.Projects.Local.ToBindingList();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            UserUtils.currentUser = null;
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void User_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.SelectedItems.Count != 0)
            {
                User user = userGrid.SelectedItems[0] as User;
                if (user != null)
                {
                    UserUtils.editUser = user;
                    EditUser editUser = new EditUser();
                    editUser.Show();
                    this.Close();
                }
            }
            else
            {
                UserWarningMessageLabel.Content = "Пользователь не выбран!";
            }
        }

        private void User_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.SelectedItems.Count != 0)
            {
                User user = userGrid.SelectedItems[0] as User;
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    UserWarningMessageLabel.Content = "Пользователь удалён!";
                }
            }
            else
            {
                UserWarningMessageLabel.Content = "Пользователь не выбран!";
            }
        }


        private void Project_Add_Click(object sender, RoutedEventArgs e)
        {
            AddProject addProject = new AddProject();
            addProject.Show();
            this.Close();
        }

        private void Project_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (projectGrid.SelectedItems.Count != 0)
            {
                Project project = projectGrid.SelectedItems[0] as Project;
                if (project != null)
                {
                    ProjectUtils.editProject = project;
                    EditProject editproject = new EditProject();
                    editproject.Show();
                    this.Close();
                }
            }
            else
            {
                ProjectWarningMessageLable.Content = "Проект для редактирования не выбран!!!";
            }
        }

        private void Project_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (projectGrid.SelectedItems.Count != 0)
            {
                Project project = projectGrid.SelectedItems[0] as Project;
                if (project != null)
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                    ProjectWarningMessageLable.Content = "Проект удалён!!!";
                }
            }
            else
            {
                ProjectWarningMessageLable.Content = "Проект не выбран!!!";
            }
        }

        private void assignProjectGrid_Selected(object sender, RoutedEventArgs e)
        {
            if (assignProjectGrid.SelectedItems.Count != 0)
            {
                Project project = assignProjectGrid.SelectedItems[0] as Project;
                if (project != null)
                {
                    assignUsersList.ItemsSource = UIUtils.getUsersForProjectAssignment(project);
                    AssignedWarningMessageLable.Content = "";
                }
            }
        }

        private void Users_Assign_Click(object sender, RoutedEventArgs e)
        {
            if (assignProjectGrid.SelectedItems.Count != 0)
            {
                Project project = assignProjectGrid.SelectedItems[0] as Project;
                if (project != null)
                {
                    UIUtils.assignUsersToProject(project, assignUsersList.Items);
                    ProjectUtils.updateProject(project);
                    AssignedWarningMessageLable.Content = "Пользователи для проекта обновлены!!!";
                }
            }
            else
            {
                AssignedWarningMessageLable.Content = "Проект не выбран!!!";
            }
        }
    }
}
