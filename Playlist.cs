using MySqlConnector;
using System.Data;
using System;

namespace WebApplication5
{
    public class Playlist
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string TableName { get; set; }

        internal PlaylistDb Db2 { get; set; }

        public Playlist()
        {

        }

        internal Playlist(PlaylistDb db2)
        {
            Db2 = db2;
        }

        public async Task InsertAsync(string tableName, string title, string url)
        {
            using var cmd = Db2.Connection2.CreateCommand();
            cmd.CommandText = @"INSERT INTO "+tableName+" (Title, Url) VALUES ('"+title+"', '"+url+"');";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
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
                ParameterName = "@url",
                DbType = DbType.String,
                Value = Url,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@tableName",
                DbType = DbType.String,
                Value = TableName,
            });
        }
    }
}
