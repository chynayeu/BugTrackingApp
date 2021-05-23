using BugTrackingApp.service.model;
using System.Windows;


namespace BugTrackingApp.ui.view.project
{
    /// <summary>
    /// Interaction logic for AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        public AddProject()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
           if(!string.IsNullOrWhiteSpace(NameTb.Text) && !string.IsNullOrWhiteSpace(ProjectDescription.Text))
            {
                if(ProjectUtils.addProject(NameTb.Text, ProjectDescription.Text))
                {
                    WarningMessageLable.Content = "Проект сохранён. Можете добавить ещё один!";
                    NameTb.Text = "";
                    ProjectDescription.Text = "";
                    return;
                }
                WarningMessageLable.Content = "Проект не добавлен. Внутренняя ошибка!";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(NameTb.Text))
                {
                    WarningMessageLable.Content = "Название проекта не должно быть пустым!!!";
                }
                else
                {
                    WarningMessageLable.Content = "Описание проекта не должно быть пустым!!!";
                }
                
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Administration admin = new Administration();
            admin.Show();
            this.Close();
        }
    }
}
