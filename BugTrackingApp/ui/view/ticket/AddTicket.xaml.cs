using BugTrackingApp.service.model;
using BugTrackingApp.ui.utils;
using System;
using System.Windows;

namespace BugTrackingApp.ui.view.ticket
{
    /// <summary>
    /// Логика взаимодействия для AddTicket.xaml
    /// </summary>
    public partial class AddTicket : Window
    {
        Ticket ticket;
        public AddTicket()
        {
            InitializeComponent();
            if(TicketUtils.currentTicket != null)
            {
                ticket = TicketUtils.currentTicket;
            }
            else
            {
                ticket = new Ticket();
            }
            ticket.created = UserUtils.currentUser.Id;
            cbType.ItemsSource = Enum.GetValues(typeof(TicketUtils.TicketType));
            cbPriority.ItemsSource = Enum.GetValues(typeof(TicketUtils.TicketPriority));
            SetFields();
        }

        private void btAssignTicket_Click(object sender, RoutedEventArgs e)
        {
            SaveState();
            UIUtils.backWindow = UIUtils.BackWindow.addTicket;
            AssignUser au = new AssignUser();
            au.Show();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveState();
            TicketUtils.creatTicket(ticket);
            backToHome();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            backToHome();
        }

        private void backToHome()
        {
            HomeWindow hw = new HomeWindow();
            hw.Show();
            this.Close();
        }

        private void SaveState()
        {
            if (ticket == null)
            {
                ticket = new Ticket();
            }
            ticket.type = (int)cbType.SelectedItem;
            ticket.priority = (int)cbPriority.SelectedItem;
            ticket.name = tbName.Text;
            ticket.description = tbDescription.Text;
            if(UserUtils.assigneeUser != null)
            {
                ticket.assigned = (int)UserUtils.assigneeUser.Id;
            }
            TicketUtils.currentTicket = ticket;
        }

        private void SetFields()
        {
            if (ticket == null)
            {
                ticket = new Ticket();
            }
            cbType.SelectedItem = (TicketUtils.TicketType)ticket.type;
            cbPriority.SelectedItem = (TicketUtils.TicketPriority)ticket.priority;
            tbName.Text = ticket.name;
            tbDescription.Text = ticket.description;
            if (UserUtils.assigneeUser != null)
            {
                ticket.assigned = (int)UserUtils.assigneeUser.Id;
            }
            if (ticket.assigned != null)
            {
                tbAssignee.Text = UIUtils.getUserDescrById((int)ticket.assigned);
            }
        }

    }
}
