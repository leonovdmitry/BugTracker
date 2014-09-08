using System;
using System.Windows;
using BugTracker.Model;
using BugTracker.Model.Entities;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for AddOrEditDeveloper.xaml
    /// </summary>
    public partial class AddOrEditDeveloper : Window
    {
        private readonly DataManage _dataManager;
        private readonly DeveloperEntity _developer; 
        public AddOrEditDeveloper(DataManage dataManager, DeveloperEntity developer)
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
            try
            {
                var develpoer = new DeveloperEntity
                {
                    Id = _developer.Id,
                    Name = DeveloperNameTxtb.Text,
                    Telefone = DeveloperContactTxtb.Text
                    
                };
                _dataManager.DeveloperRepository.AddOrEditDeveloper(develpoer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
            
        }

        
    }
}
