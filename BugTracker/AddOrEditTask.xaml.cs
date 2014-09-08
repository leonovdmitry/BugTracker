using System;
using System.Windows;
using BugTracker.Model;
using BugTracker.Model.Entities;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for AddOrEditTask.xaml
    /// </summary>
    public partial class AddOrEditTask : Window
    {
        private readonly DataManage _dataManager;
        private readonly TaskEntitty _task;
        public AddOrEditTask(DataManage dataManager, TaskEntitty task)
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
            var developer = (DeveloperEntity)TaskDeveloperCb.SelectedItem;
            try
            {
                var task = new TaskEntitty
                {
                    Name = TaskNameTb.Text,
                    Status = TaskStatusCb.SelectedIndex,
                    Type = TaskTypeCb.SelectedIndex,
                    Date = (DateTime)TaskDateDp.SelectedDate,
                    Comment = TaskCommentTb.Text,
                    DeveloperId = developer.Id
                };
                _dataManager.TaskRepository.AddOrEditTask(task);
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
            TaskStatusCb.SelectedIndex = _task.Status;

            TaskDeveloperCb.ItemsSource = _dataManager.DeveloperRepository.GetList();
            
            //if (_task.DeveloperId != 0)
            //{
            TaskDeveloperCb.SelectedItem = _task.DeveloperId - 1;

            //}
            //else
            //{
            //    TaskDeveloperCb.SelectedIndex = 0;
            //}

        }
    }
}
