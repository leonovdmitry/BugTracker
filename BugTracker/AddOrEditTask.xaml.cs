using System;
using System.Windows;
using BugTracker.Model;
using BugTracker.Model.Entities;

namespace BugTracker
{
    public partial class AddOrEditTask
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
            var developer = (Developer)TaskDeveloperCb.SelectedItem;
            try
            {
                if (TaskDateDp.SelectedDate != null)
                {
                    var task = new Task
                    {
                        Id = _task.Id,
                        Name = TaskNameTb.Text,
                        Status = TaskStatusCb.SelectedIndex,
                        Type = TaskTypeCb.SelectedIndex,
                        Date = (DateTime)TaskDateDp.SelectedDate,
                        Comment = TaskCommentTb.Text,
                        DeveloperId = developer.Id
                    };
                    _dataManager.TaskRepository.AddOrEditTask(task);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        //заполнение полей если объект редактируется
        private void SetFeilds()
        {
            var taskTypes = new[]
            {
                "Bug", "New feature","Task"
            };
            var taskStatus = new[]
            {
                "Не назначена", "В работе", "Закончена"
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
            TaskDeveloperCb.SelectedItem = _task.DeveloperId;

            //}
            //else
            //{
            //    TaskDeveloperCb.SelectedIndex = 0;
            //}

        }
    }
}
