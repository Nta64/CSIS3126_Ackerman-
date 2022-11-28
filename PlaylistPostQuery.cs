using MySqlConnector;
using System.Data.Common;
using System.Data;
using System.Transactions;

namespace WebApplication5
{
    public class PlaylistPostQuery
    {
        public PlaylistDb Db2 { get; }

        public PlaylistPostQuery(PlaylistDb db2)
        {
            Db2 = db2;
        }


        public async Task CreatePlaylist(string tableName)
        {
            using var txn = await Db2.Connection2.BeginTransactionAsync();
            using var cmd = Db2.Connection2.CreateCommand();
            cmd.CommandText = @"CREATE TABLE " + tableName + " (id int AUTO_INCREMENT NOT null PRIMARY KEY, Title varchar(200) NOT null); ";          
            cmd.Transaction = txn;
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        public async Task AddSong(string tableName, string songName)
        {
            using var txn = await Db2.Connection2.BeginTransactionAsync();
            using var cmd = Db2.Connection2.CreateCommand();
            cmd.CommandText = cmd.CommandText = @"INSERT INTO "+@tableName+" (Title) VALUES ('"+@songName+"')";
            cmd.Transaction = txn;
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }


        public async Task<List<Playlist>> LatestPlaylist()
        {
            using var cmd = Db2.Connection2.CreateCommand();
            cmd.CommandText = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA='playlistslist'";
            return await ReadPlaylistsAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Playlist>> LatestPlaylistUrl(string tableName)
        {
            using var cmd = Db2.Connection2.CreateCommand();
            cmd.CommandText = @"SELECT `Url` FROM `"+tableName+"`;";
            return await ReadAllPlaylistUrlAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Playlist>> LatestPlaylistTitle(string tableName)
        {
            using var cmd = Db2.Connection2.CreateCommand();
            cmd.CommandText = @"SELECT `Title` FROM `" + tableName + "`;";
            return await ReadAllPlaylistTitleslAsync(await cmd.ExecuteReaderAsync());
        }


        private async Task<List<Playlist>> ReadAllPlaylistUrlAsync(DbDataReader reader)
        {
            var urls = new List<Playlist>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var url = new Playlist(Db2)
                    {
                        Url = reader.GetString(0),
                    };
                    urls.Add(url);
                }
            }
            return urls;
        }

        private async Task<List<Playlist>> ReadAllPlaylistTitleslAsync(DbDataReader reader)
        {
            var urls = new List<Playlist>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var url = new Playlist(Db2)
                    {
                        Title = reader.GetString(0),
                    };
                    urls.Add(url);
                }
            }
            return urls;
        }


        private async Task<List<Playlist>> ReadPlaylistsAsync(DbDataReader reader)
        {
            var names = new List<Playlist>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var name = new Playlist(Db2)
                    {
                        TableName = reader.GetString(0),
                    };
                    names.Add(name);
                }
            }
            return names;
        }


    }
}
