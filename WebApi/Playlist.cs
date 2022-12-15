using MySqlConnector;
using System.Data;
using System;

namespace WebApplication5
{
    public class Playlist
    {
        //playlist object/model
        public int Playlist_id { get; set; }
        public int User_id { get; set; }
        public string Name { get; set; }

        public Playlist()
        {

        }
        internal AppDb Db{ get; set; } 
        internal Playlist(AppDb db)
        {
            Db = db;
        }

        private void BindPlaylistId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@ulaylist_id",
                DbType = DbType.Int32,
                Value = Playlist_id,
            });
        }
        private void BindUserId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_id",
                DbType = DbType.Int32,
                Value = User_id,
            });
        }
        private void BindParams(MySqlCommand cmd)
        {   
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = Name,
            });
        }
    }
}
