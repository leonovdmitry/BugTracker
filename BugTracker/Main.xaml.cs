using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using BugTracker.Model;
using BugTracker.Model.Entities;
using BugTracker.Properties;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataManage _dataManager = new DataManage(Settings.Default.ConnectionString);

        public MainWindow()
        {

            InitializeComponent();
        }


        private void DeveloperAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var developerWindow = new AddOrEditDeveloper(_dataManager, new DeveloperEntity());
            developerWindow.ShowDialog();
            FillDeveloperGrid();
            //SetDataGrids();
        }

        private void DeveloperEditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DeveloperDataGrid.SelectedItem != null)
            {
                var developerWindow = new AddOrEditDeveloper(_dataManager, (DeveloperEntity)DeveloperDataGrid.SelectedItem);
                developerWindow.ShowDialog();
            }

            FillDeveloperGrid();
            //SetDataGrids();

        }

        private void DeveloperDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DeveloperDataGrid.SelectedItem != null)
            {
                _dataManager.DeveloperRepository.Delete((DeveloperEntity)DeveloperDataGrid.SelectedItem);
                FillDeveloperGrid();
                //SetDataGrids();
            }


        }

        private void TaskAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new AddOrEditTask(_dataManager, new TaskEntitty());
            taskWindow.ShowDialog();
            FillTaskGrid();
            //SetDataGrids();
        }

        private void TaskEditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TaskDataGrid.SelectedItem != null)
            {
                var taskWindow = new AddOrEditTask(_dataManager, (TaskEntitty)TaskDataGrid.SelectedItem);
                taskWindow.ShowDialog();
            }

            FillTaskGrid();
            //SetDataGrids();

        }

        private void TaskDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TaskDataGrid.SelectedItem != null)
            {
                _dataManager.TaskRepository.Delete((TaskEntitty)TaskDataGrid.SelectedItem);
                FillTaskGrid();
                //SetDataGrids();
            }
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Выгрузка отчета по задачам

            CreateReport();
        }

        private void CreateReport()
        {
            var xwriter = new XmlTextWriter("Tasklist.xml", Encoding.UTF8);
            xwriter.WriteStartDocument();
            xwriter.WriteStartElement("Tasks");
            foreach (var task in _dataManager.TaskRepository.GetList())
            {
                xwriter.WriteStartElement("Task");
                xwriter.WriteStartElement("Date");
                xwriter.WriteString(task.Date.ToShortDateString());
                xwriter.WriteEndElement();
                xwriter.WriteStartElement("Name");
                xwriter.WriteString(task.Name);
                xwriter.WriteEndElement();
                xwriter.WriteStartElement("Type");
                xwriter.WriteString(task.Type.ToString(CultureInfo.InvariantCulture));
                xwriter.WriteEndElement();
                xwriter.WriteStartElement("Comment");
                xwriter.WriteString(task.Comment);
                xwriter.WriteEndElement();
                xwriter.WriteStartElement("Status");
                xwriter.WriteString(task.Status.ToString(CultureInfo.InvariantCulture));
                xwriter.WriteEndElement();
                xwriter.WriteStartElement("Developer");
                var developer = _dataManager.DeveloperRepository.GetList()
                    .SingleOrDefault(m => m.Id == task.DeveloperId);
                if (developer != null) xwriter.WriteString(developer.Name);
                else xwriter.WriteString("");
                xwriter.WriteEndElement();
                xwriter.WriteEndElement();
            }
            xwriter.WriteEndElement();
            xwriter.WriteEndDocument();
            xwriter.Close();
            MessageBox.Show("Отчет создан!!!");
        }


        private void FillDeveloperGrid()
        {
            //entity
            //DeveloperDataGrid.ItemsSource = _dataManager.GetDevelopers();
            var developerCollectionViewSource = (CollectionViewSource)(FindResource("DeveloperCollectionViewSource"));
            developerCollectionViewSource.Source = _dataManager.DeveloperRepository.GetList();
            //DeveloperDataGrid.ItemsSource = _dataManager.DeveloperRepository.GetList();
        }

        private void FillTaskGrid()
        {
            //entity
            //TaskDataGrid.ItemsSource = _dataManager.GetTasks();
            var taskCollectionViewSource = (CollectionViewSource)(FindResource("TaskCollectionViewSource"));
            taskCollectionViewSource.Source = _dataManager.TaskRepository.GetList();
            // TaskDataGrid.ItemsSource = _dataManager.TaskRepository.GetList();

        }


        private void BugTrackerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FillDeveloperGrid();
            FillTaskGrid();
            //SetDataGrids();
            //DeveloperDataGrid.ItemsSource = _dataManager.GetDevelopers();
            //TaskDataGrid.ItemsSource = _dataManager.GetTasks();
        }

        //private void SetDataGrids()
        //{
        //    //DeveloperDataGrid.Columns[0].Visibility = Visibility.Hidden;
        //    //DeveloperDataGrid.Columns[1].Header = "ФИО";
        //    //DeveloperDataGrid.Columns[2].Header = "Телефон";

        //    //TaskDataGrid.Columns[0].Visibility = Visibility.Hidden;
        //    //TaskDataGrid.Columns[1].Header = "Название";
        //    //TaskDataGrid.Columns[2].Header = "Тип";
        //    //TaskDataGrid.Columns[3].Header = "Дата";
        //    //TaskDataGrid.Columns[4].Header = "Статус";
        //    //TaskDataGrid.Columns[5].Header = "Комментарий";
        //    //TaskDataGrid.Columns[6].Visibility = Visibility.Hidden;
        //}


        ////Фильтрация(пример)////
        /*private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PagedCollectionView pcv = this.dataGrid1.ItemsSource as PagedCollectionView;
            if (pcv != null && pcv.CanFilter == true)
            {
                // Apply the filter.
                pcv.Filter = new Predicate<object>(FilterCompletedTasks);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PagedCollectionView pcv = this.dataGrid1.ItemsSource as PagedCollectionView;
            if (pcv != null)
            {
                // Remove the filter.
                pcv.Filter = null;
            }
        }

        public bool FilterCompletedTasks(object t)
        {
            Task task = t as Task;
            return (task.Complete == false);
        }
        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            PagedCollectionView pcv = dataGrid1.ItemsSource as PagedCollectionView;
            try
            {
                foreach (CollectionViewGroup group in pcv.Groups)
                {
                    dataGrid1.ExpandRowGroup(group, true);
                }
            }
            catch (Exception ex)
            {
                // Could not expand group.
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}
