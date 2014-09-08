using System.Data.SqlClient;
using BugTracker.Model.Repositories;

namespace BugTracker.Model
{
    public class DataManage
    {
            public DeveloperRepository DeveloperRepository { get; private set; }

            public TaskRepository TaskRepository { get; private set; }

            public DataManage(string connectionString)
            {
                var sqlConnect = new SqlConnection(connectionString);
                sqlConnect.Open();

                DeveloperRepository = new DeveloperRepository(sqlConnect, this);
                TaskRepository = new TaskRepository(sqlConnect, this);
            }
        
    }
}
