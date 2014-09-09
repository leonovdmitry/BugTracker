using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BugTracker.Model.Entities;

namespace BugTracker.Model.Repositories
{
    //репозиторий для объетов developer 
    public class DeveloperRepository
    {
        private readonly SqlConnection _connection;

        public DataManager DataManager { get; private set; }

        public DeveloperRepository(SqlConnection sqlConnection, DataManager dataManager)
        {
            _connection = sqlConnection;
            DataManager = dataManager;
        }

        //получить список всех разаработчиков
        public IEnumerable<Developer> GetList()
        {
            return GetFromSql(new SqlCommand("select * from Developer", _connection));
        }

        //определение: добавить или изменить разарботчика
        public void AddOrEditDeveloper(Developer developer)
        {
            if (developer.Id == 0)
                Add(developer);
            else
                Edit(developer);
        }

        //удалить разарботчика
        public void Delete(Developer developer)
        {
            var query = string.Format("delete from Developer where Id = '{0}'", developer.Id);
            var cmd = new SqlCommand(query, _connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка удаления!", ex);
            }
        }

        //добавление в базе
        private void Add(Developer developer)
        {
            var query =
                string.Format(
                    "insert into Developer (Name, Telefone) values" + "('{0}', '{1}')",
                    developer.Name,
                    developer.Telefone);
            try
            {
                var cmd = new SqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка добавления!", ex);
            }
        }

        //изменение в базе
        private void Edit(Developer developer)
        {
            var query =
                string.Format(
                    "update Developer set Name = '{0}', Telefone = '{1}' where id = '{2}'",
                    developer.Name,
                    developer.Telefone,
                    developer.Id);
            var cmd = new SqlCommand(query, _connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка изменения!", ex);
            }
        }

        //обработчик sql команды
        private IEnumerable<Developer> GetFromSql(SqlCommand cmd)
        {
            var developerList = new List<Developer>();
            using (var dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    developerList.Add(
                        new Developer
                        {
                            Id = (int)dataReader["Id"],
                            Name = dataReader["Name"].ToString(),
                            Telefone = dataReader["Telefone"].ToString()
                        });
                }
            }

            return developerList;
        }
    }
}
