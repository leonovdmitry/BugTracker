using System;
using System.Windows;
using BugTracker.Model;
using BugTracker.Model.Entities;

namespace BugTracker
{
    public partial class AddOrEditDeveloper
    {
        private readonly DataManager _dataManager;
        private readonly Developer _developer;
        public AddOrEditDeveloper(DataManager dataManager, Developer developer)
        {
            InitializeComponent();
            _dataManager = dataManager;
            _developer = developer;
            SetFields();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var develpoer = new Developer
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

        //Заполнение полей
        private void SetFields()
        {
            DeveloperContactTxtb.Text = _developer.Telefone;
            DeveloperNameTxtb.Text = _developer.Name;
        }
    }
}
