using MySqlConnector;
using System.Data;

namespace WebApplication5
{
    public class Song
    {
        //Song object/model
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }   
        public string Url { get; set; }

        internal AppDb Db { get; set; }
        public Song()
        {

        }
        internal Song(AppDb db)
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
                ParameterName = "@title",
                DbType = DbType.String,
                Value = Title,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@artist",
                DbType = DbType.String,
                Value = Artist,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@url",
                DbType = DbType.String,
                Value = Url,
            });

        }
    }
}
