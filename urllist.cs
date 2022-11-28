using MySqlConnector;
using System.Data;

namespace WebApplication5
{
    public class urllist
    {
        public string Url { get; set; }
        internal AppDb Db { get; set; }


        public urllist()
        {

        }

        internal urllist(AppDb db)
        {
            Db = db;
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@url",
                DbType = DbType.String,
                Value = Url
            });
        
        }

    }
}
