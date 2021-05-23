using BugTrackingApp.service.model;
using BugTrackingApp.ui.view.ticket;
using System.Windows;
using System.Windows.Controls;

namespace BugTrackingApp.ui.view
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            UserName.Text = UserUtils.currentUser.name + " " + UserUtils.currentUser.surname;
            projectGrid.ItemsSource = ProjectUtils.getUserProjects(UserUtils.currentUser);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            UserUtils.currentUser = null;
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void projectGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectGrid.SelectedItems.Count != 0)
            {
                Project project = projectGrid.SelectedItems[0] as Project;
                if (project != null)
                {
                    ProjectUtils.assignProject = project;
                    ticketGrid.ItemsSource = TicketUtils.getTicketsByProjectId(project.Id);
                }
            }
        }

        private void View_Ticket_Click(object sender, RoutedEventArgs e)
        {
            if(ticketGrid.SelectedItems.Count != 0)
            {
                Ticket ticket = ticketGrid.SelectedItems[0] as Ticket;
                if (ticket != null)
                {
                    TicketUtils.currentTicket = ticket;
                    ViewTicket vt = new ViewTicket();
                    vt.Show();
                    this.Close();
                }
            }
            else
            {
                WarningMessageLable.Content = "Тикет не выбран!";
            }
        }

        private void Add_Ticket_Click(object sender, RoutedEventArgs e)
        {
            if (projectGrid.SelectedItems.Count != 0)
            {
                Project project = projectGrid.SelectedItems[0] as Project;
                if (ProjectUtils.assignProject != null)
                {
                    AddTicket at = new AddTicket();
                    at.Show();
                    this.Close();
                }
            }
            else
            {
                WarningMessageLable.Content = "Проект не выбран!";
            }
        }

    }
}
