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
        private readonly DataManager _dataManager= new DataManager(); 

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

        private void BugTrackerWindow_Initialized(object sender, EventArgs e)
        {
            
            DeveloperDataGrid.ItemsSource = _dataManager.GetDevelopers();
            //foreach (var dataGridColumn in DeveloperDataGrid.Columns)
            //{
            //    if (dataGridColumn.Header.ToString() == "Id")
            //        Visibility = Visibility.Hidden;
            //}
            
            TaskDataGrid.ItemsSource = _dataManager.GetTasks();
            
        }

        

      
    }
}
