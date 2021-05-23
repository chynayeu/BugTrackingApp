using BugTrackingApp.service.model;
using BugTrackingApp.ui.utils;
using System;
using System.Windows;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace BugTrackingApp.ui.view.ticket
{
    /// <summary>
    /// Логика взаимодействия для ViewTicket.xaml
    /// </summary>
    public partial class ViewTicket : Window
    {
        Ticket ticket;
        List<Comment> comments;
        public ViewTicket()
        {
            InitializeComponent();
            ticket = TicketUtils.currentTicket;
            cbType.ItemsSource = Enum.GetValues(typeof(TicketUtils.TicketType));
            cbPriority.ItemsSource = Enum.GetValues(typeof(TicketUtils.TicketPriority));
            cbStatus.ItemsSource = Enum.GetValues(typeof(TicketUtils.TicketStatus));
            comments = TicketUtils.getTicketComments(ticket);
            CommentList.ItemsSource = comments;
            SetFields();
        }

        private void btAssignTicket_Click(object sender, RoutedEventArgs e)
        {
            UIUtils.backWindow = UIUtils.BackWindow.viewTicket;
            AssignUser au = new AssignUser();
            au.Show();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveState();
            TicketUtils.updateTicket(ticket);
            backToHome();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            backToHome();
        }

        private void backToHome()
        {
            TicketUtils.currentTicket = null;
            HomeWindow hw = new HomeWindow();
            hw.Show();
            this.Close();
        }

        private void Add_Comment_Click(object sender, RoutedEventArgs e)
        {
            String commentText = Interaction.InputBox("Введите комметнарий", "Комментарий", "");
            if (!string.IsNullOrEmpty(commentText))
            {
                TicketUtils.addComment(commentText);
            }
            CommentList.ItemsSource = comments = TicketUtils.getTicketComments(ticket);
        }

        private void SaveState()
        {
            if(ticket == null)
            {
                ticket = new Ticket();
            }
            ticket.status = (int)cbStatus.SelectedItem;
            ticket.type = (int)cbType.SelectedItem;
            ticket.priority = (int)cbPriority.SelectedItem;
            ticket.name = tbName.Text;
            ticket.description = tbDescription.Text;
            if (UserUtils.assigneeUser != null)
            {
                ticket.assigned = (int)UserUtils.assigneeUser.Id;
                UserUtils.assigneeUser = null;
            }
            
        }

        private void SetFields()
        {
            if (ticket == null)
            {
                ticket = new Ticket();
            }
            cbStatus.SelectedItem = (TicketUtils.TicketStatus)ticket.status;
            cbType.SelectedItem = (TicketUtils.TicketType)ticket.type;
            cbPriority.SelectedItem = (TicketUtils.TicketPriority)ticket.priority;
            tbName.Text = ticket.name;
            tbDescription.Text = ticket.description;
            if (UserUtils.assigneeUser != null)
            {
                ticket.assigned = (int)UserUtils.assigneeUser.Id;
            }
            tbAssignee.Text = UIUtils.getUserDescrById((int)ticket.assigned);
        }

    }
}
