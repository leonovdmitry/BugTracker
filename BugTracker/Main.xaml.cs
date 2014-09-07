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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BugTracker.DB;
using Microsoft.Practices.Prism;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataManager _dataManager = new DataManager();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void DeveloperAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var developerWindow = new AddOrEditDeveloper(_dataManager, new Developer());
            developerWindow.ShowDialog();
        }

        private void DeveloperEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var developerWindow = new AddOrEditDeveloper(_dataManager, (Developer)DeveloperDataGrid.SelectedItem);
            developerWindow.ShowDialog();
        }

        private void DeveloperDeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TaskAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new AddOrEditTask(_dataManager, new Task());
            taskWindow.ShowDialog();
        }

        private void TaskEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new AddOrEditTask(_dataManager, (Task)TaskDataGrid.SelectedItem);
            taskWindow.ShowDialog();
        }

        private void TaskDeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BugTrackerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FillDeveloperGrid();
            FillTaskGrid();
            //DeveloperDataGrid.ItemsSource = _dataManager.GetDevelopers();
            //TaskDataGrid.ItemsSource = _dataManager.GetTasks();
        }

        private void FillDeveloperGrid()
        {
            var developersToShow = from dev in _dataManager.GetDevelopers()
                                   select new { dev.Name, dev.Telefone };
            DeveloperDataGrid.ItemsSource = developersToShow;
        }

        private void FillTaskGrid()
        {
            var taskStatus = new[]
            {
                "In progress", "End", "Not assigned"
            };

            var taskTypes = new[]
            {
                "Bug", "New feature","Task"
            };

            var tasksToShow = from task in _dataManager.GetTasks()
                              select new { task.Date, task.Name, task.Type, task.Comment, task.Status };
            TaskDataGrid.ItemsSource = tasksToShow;


        }

        private void DeveloperDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var headerName = e.Column.Header.ToString();

            switch (headerName)
            {
                case "Name":
                    e.Column.Header = "ФИО";
                    break;
                case "Telefone":
                    e.Column.Header = "Телефон";
                    break;
            }
        }

        private void TaskDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            var headerName = e.Column.Header.ToString();
            switch (headerName)
            {
                case "Date":
                    e.Column.Header = "Дата";
                    break;
                case "Name":
                    e.Column.Header = "Название";
                    break;
                case "Type":
                    e.Column.Header = "Тип";
                    break;
                case "Comment":
                    e.Column.Header = "Комментарий";
                    break;
                case "Status":
                    e.Column.Header = "Статус";
                    break;

            }

        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Выгрузка отчета по задачам
        }


    }
}
