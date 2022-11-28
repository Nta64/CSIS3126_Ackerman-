using MySqlConnector;

namespace WebApplication5
{
    public class PlaylistDb : IDisposable
    {
        public MySqlConnection Connection2 { get; }

        public PlaylistDb(string connectionString)
        {
            Connection2 = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection2.Dispose();
    }
}
