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
using BugTracker.DB;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for AddOrEditTask.xaml
    /// </summary>
    public partial class AddOrEditTask : Window
    {
        private readonly DataManager _dataManager;
        private readonly Task _task;
        public AddOrEditTask(DataManager dataManager, Task task)
        {
            InitializeComponent();
            _dataManager = dataManager;
            _task = task;
            SetFeilds();
           
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var task = new Task();
                {
                    //Id = _developer.Id,
                    //Name = DeveloperNameTxtb.Text,
                    //Telefone = DeveloperContactTxtb.Text,
                    //Task = null
                };
                _dataManager.AddOrEditTask(task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void SetFeilds()
        {
            var taskTypes = new[]
            {
                "Bug", "New feature","Task"
            };
            var taskStatus = new[]
            {
                "In progress", "End", "Not assigned"
            };
            TaskDateDp.SelectedDate = _task.Date;
            TaskTypeCb.ItemsSource = taskTypes;
            TaskTypeCb.SelectedIndex = _task.Type;
            TaskNameTb.Text = _task.Name;
            TaskCommentTb.Text = _task.Comment;
            TaskStatusCb.ItemsSource = taskStatus;
            TaskTypeCb.SelectedIndex = _task.Status;
            var developersList = _dataManager.GetDevelopers();
            TaskDeveloperCb.ItemsSource = developersList;
            if (_task.DeveloperId !=0)
            {
                TaskDeveloperCb.SelectedItem = developersList.SingleOrDefault(dev => dev.Id == _task.DeveloperId);
            }
            else
            {
                TaskDeveloperCb.SelectedIndex = 0;
            }

        }
    }
}
