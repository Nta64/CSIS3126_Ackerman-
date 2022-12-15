using MySqlConnector;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace WebApplication5
{
    public class UserQuery
    {
        public AppDb Db { get; }

        public UserQuery(AppDb db)
        {
            Db = db;
        }
        
        //sql query that creates a user and adds them to the database
        // this is a two part query that first checks if that user already exists and executes a fake query to throw an error if there is 
        //if there is not already a user under that name, then it creates the user
        public async Task RegisterUserAsync(string name, string password, string dateTime)
        {        
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = ("Select * from users where (userName) = (@name)");
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Transaction = txn;
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();
                cmd.CommandText = "INSERT INTO blank ()";
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();
            }
            else
            {
                reader.Close();
                cmd.CommandText = "INSERT INTO users (userName, password, DateTime) values (@name, @password, @datetime)";
                cmd.Parameters.AddWithValue("@password", HashString(password));
                cmd.Parameters.AddWithValue("@datetime", dateTime);
                await cmd.ExecuteNonQueryAsync();
                await txn.CommitAsync();  
            }
        }

        //sql query that authenticates the user with a hashed/salted password
        public bool UserAutherization(string UserName, string Password)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = ("Select * from users where (userName, password) = (@UserName, @Password)");
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", HashString(Password));

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //retruns a string of the byte array containing the hash
        public string HashString(string passwordString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(passwordString))
                sb.Append(b.ToString("X3"));
            return sb.ToString();
        }

        //creates a byte array of a hashed string
        public static byte[] GetHash(string passwordString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
        }

        //sql query that gets the current users id using their username
        public async Task<List<Playlist>> CurrentUserId(string UserName)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id` FROM `users` WHERE (userName) = (@UserName)";
            cmd.Parameters.AddWithValue("@UserName", UserName);
            return await GetUserIdAsync(await cmd.ExecuteReaderAsync());
        }

        //sql query that reads the users id
        private async Task<List<Playlist>> GetUserIdAsync(DbDataReader reader)
        {
            var names = new List<Playlist>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var name = new Playlist(Db)
                    {
                        User_id = reader.GetInt32(0),
                    };
                    names.Add(name);
                }
            }
            return names;
        }
    }
}
