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

namespace BugTrackingApp.ui.view.project
{
    /// <summary>
    /// Interaction logic for EditProject.xaml
    /// </summary>
    public partial class EditProject : Window
    {
        public EditProject()
        {
            InitializeComponent();
            if(ProjectUtils.editProject is null)
            {
                backToAdmin();
            }
            ProjectNameLable.Content = ProjectUtils.editProject.name;
            ProjectDescription.Text = ProjectUtils.editProject.description;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ProjectUtils.editProject.description = ProjectDescription.Text;
            ProjectUtils.updateProject(ProjectUtils.editProject);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            backToAdmin();
        }

        private void backToAdmin()
        {
            ProjectUtils.editProject = null;
            Administration admin = new Administration();
            admin.Show();
            this.Close();
        }
    }
}
