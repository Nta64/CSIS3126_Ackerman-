using MySqlConnector; 
namespace WebApplication5
{ 
    //connection to my application MySql databse
        public class AppDb : IDisposable
        {
            public MySqlConnection Connection { get; }

            public AppDb(string connectionString)
            {
                Connection = new MySqlConnection(connectionString);
            }

            public void Dispose() => Connection.Dispose();
        }  
}
