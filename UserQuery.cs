using MySqlConnector;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication5
{
    public class UserQuery
    {
        public AppDb Db { get; }

        public UserQuery(AppDb db)
        {
            Db = db;
        }

        public async Task RegisterUserAsync(string name, string password, string dateTime)
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO users (userName, password, DateTime) values (@name, @password, @datetime)";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", HashString(password));
            cmd.Parameters.AddWithValue("@datetime", dateTime);
            cmd.Transaction = txn;
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();


        }

        public bool UserAutherization(string name, string password)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = ("Select * from users where (userName, Password) = (@name, @password)");
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", HashString(password));
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

        public string HashString(string passwordString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(passwordString))
                sb.Append(b.ToString("X3"));
            return sb.ToString();
        }
        public static byte[] GetHash(string passwordString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
        }

    }
}
