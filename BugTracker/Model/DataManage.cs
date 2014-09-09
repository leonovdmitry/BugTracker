using System.Data.SqlClient;
using BugTracker.Model.Repositories;

namespace BugTracker.Model
{
    //класс управления и доступа к данным хранит и инициализирует репозитории, создает подключение.
    public class DataManager
    {
            public DeveloperRepository DeveloperRepository { get; private set; }

            public TaskRepository TaskRepository { get; private set; }

            public DataManager(string connectionString)
            {
                var sqlConnect = new SqlConnection(connectionString);
                sqlConnect.Open();

                DeveloperRepository = new DeveloperRepository(sqlConnect, this);
                TaskRepository = new TaskRepository(sqlConnect, this);
            }
        
    }
}
