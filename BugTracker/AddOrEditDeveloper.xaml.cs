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
using BugTracker.DB;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for AddOrEditDeveloper.xaml
    /// </summary>
    public partial class AddOrEditDeveloper : Window
    {
        private readonly DataManager _dataManager;
        private readonly Developer _developer; 
        public AddOrEditDeveloper(DataManager dataManager, Developer developer)
        {
           InitializeComponent();
            _dataManager = dataManager;
            _developer = developer;
            DeveloperContactTxtb.Text = _developer.Telefone;
            DeveloperNameTxtb.Text = _developer.Name;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
             
        }

        
    }
}
