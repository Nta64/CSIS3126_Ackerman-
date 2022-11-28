using MySqlConnector;
using System.Data.Common;
using System.Data;

namespace WebApplication5
{
    public class SongPostQuery
    {
        public AppDb Db { get; }

        public SongPostQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Song> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Title`, `Artist`, 'Album', 'Url' FROM `songs` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
       
        public async Task<List<Song>> LatestSongs()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Title`, `Artist`, 'Album', 'Url' FROM `songs` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Song>> LatestUrls()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Url` FROM `songs`;";
            return await ReadAllUrlAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Song>> LatestImages()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Album` FROM `songs`;";
            return await ReadAllImageAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Song>> LatestTitles()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Title` FROM `songs`;";
            return await ReadAllTitleAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<Song>> SingleUrlByTitle(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT 'Url' FROM `songs` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.VarNumeric,
                Value = id,
            });
            return await ReadAllUrlAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `BlogPost`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Song>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Song>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Song(Db)
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author = reader.GetString(1),
                        Album = reader.GetString(1),
                        Url = reader.GetString(1),

                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        private async Task<List<Song>> ReadAllUrlAsync(DbDataReader reader)
        {
            var urls = new List<Song>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var url = new Song(Db)
                    {
                        Url = reader.GetString(0),
                    };
                    urls.Add(url);
                }
            }
            return urls;
        }

        private async Task<List<Song>> ReadAllImageAsync(DbDataReader reader)
        {
            var albums = new List<Song>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var album = new Song(Db)
                    {
                        Album = reader.GetString(0),
                    };
                    albums.Add(album);
                }
            }
            return albums;
        }

        private async Task<List<Song>> ReadAllTitleAsync(DbDataReader reader)
        {
            var titles = new List<Song>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var title = new Song(Db)
                    {
                        Title = reader.GetString(0),
                    };
                    titles.Add(title);
                }
            }
            return titles;
        }

        


    }
}
