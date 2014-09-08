using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BugTracker.Model.Entities;

namespace BugTracker.Model.Repositories
{
    public class DeveloperRepository
    {
        private readonly SqlConnection _connection;

        public DataManage DataManager { get; private set; }

        public DeveloperRepository(SqlConnection sqlConnection, DataManage dataManager)
        {
            _connection = sqlConnection;
            DataManager = dataManager;
        }

        public IEnumerable<DeveloperEntity> GetList()
        {
            return GetFromSql(new SqlCommand("select * from Developer", _connection));
        }

        public void AddOrEditDeveloper(DeveloperEntity developer)
        {
            if (developer.Id == 0)
                Add(developer);
            else
                Edit(developer);
        }

        public void Delete(DeveloperEntity developer)
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

        private void Add(DeveloperEntity developer)
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
        
        private void Edit(DeveloperEntity developer)
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

        private IEnumerable<DeveloperEntity> GetFromSql(SqlCommand cmd)
        {
            var developerList = new List<DeveloperEntity>();
            using (var dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    developerList.Add(
                        new DeveloperEntity
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
