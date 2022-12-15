using MySqlConnector;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication5
{
    public class User
    {
        //User object/model
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Registered { get; set; }
    
        internal AppDb Db { get; set; }
        internal User(AppDb db)
        {
            Db = db;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }
        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@userName",
                DbType = DbType.String,
                Value = UserName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@password",
                DbType = DbType.String,
                Value = Password,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@registered",
                DbType = DbType.String,
                Value = Registered,
            });

        }
    }
}
