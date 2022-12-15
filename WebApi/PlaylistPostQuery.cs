using MySqlConnector;
using System.Data.Common;
using System.Data;
using System.Transactions;
using Microsoft.OpenApi.Services;

namespace WebApplication5
{
    public class PlaylistPostQuery
    {
        public AppDb Db { get; }

        public PlaylistPostQuery(AppDb db)
        {
            Db = db;
        }

        //Sql query for creating playlist
        public async Task CreatePlaylist(int UserId, string PlaylistName)
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO playlists (User_Id,Name) VALUES ('"+UserId+"', '"+PlaylistName+"')";          
            cmd.Transaction = txn;
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        //Sql query for deleting specific playlist
        public async Task DeletePlaylist(int PlaylistId)
        {

            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"Delete FROM playlist_songs WHERE Playlist_id = " + PlaylistId + ";";
            cmd.Transaction = txn;
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();

            using var txn2 = await Db.Connection.BeginTransactionAsync();
            using var cmd2 = Db.Connection.CreateCommand();
            cmd2.CommandText = @"Delete FROM `playlists` WHERE Playlist_id = '" + PlaylistId + "';";
            cmd2.Transaction = txn2;
            await cmd2.ExecuteNonQueryAsync();
            await txn2.CommitAsync();

                   
        }

        //Sql query for adding song to a specific playlist
        public async Task AddSong(int SongID, int PlaylistID)
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = cmd.CommandText = @"INSERT INTO playlist_songs (Song_id,Playlist_id) VALUES (" + SongID + "," + PlaylistID + ")";
            cmd.Transaction = txn;
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        //Sql query for removing a song from a specific playlist
        public async Task RemoveSong(int SongID, int PlaylistID)
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = cmd.CommandText = @"DELETE FROM playlist_songs WHERE song_id = " + SongID + " AND  Playlist_id = "+ PlaylistID+";";
            cmd.Transaction = txn;
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        //Sql query for getting the latest playlist names for a specific user
        public async Task<List<Playlist>> LatestPlaylist(int UserId)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Name` FROM `playlists` WHERE User_id = '"+UserId +"' ;";
            return await ReadPlaylistsNamesAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query for getting the current playlist id 
        public async Task<int> CurrentPlaylistId(int UserID, string PlaylistName)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Playlist_id` FROM `playlists` WHERE User_id = '" + UserID + "' AND Name = '"+ PlaylistName +"' ;";
            return await GetPlaylistIdAsync(await cmd.ExecuteReaderAsync());
        }

        //sql query for getting the latest playlist ids for a specific user
        public async Task<List<Song>> LatestPlaylistsIds(int UserID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Playlist_id` FROM `Playlists`WHERE User_id = " + UserID + ";";
            return await ReadAllIdAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query for getting the current playlist artists 
        public async Task<List<Song>> CurrentPlaylistArtists(int PlaylistId)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT appsongs.Artist FROM appsongs INNER JOIN playlist_songs ON appsongs.Song_id = playlist_songs.Song_id INNER JOIN playlists ON playlist_songs.Playlist_id = playlists.Playlist_id WHERE  playlists.Playlist_id = " + PlaylistId + "; ";
            return await ReadAllArtistAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query for getting the latest playlist titles for a specific playlist
        public async Task<List<Song>> LatestPlaylistTitle(int PlaylistID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT appsongs.Title FROM appsongs INNER JOIN playlist_songs ON appsongs.Song_id = playlist_songs.Song_id INNER JOIN playlists ON playlist_songs.Playlist_id = playlists.Playlist_id WHERE playlists.Playlist_id = "+PlaylistID+";";
            return await ReadAllPlaylistTitleslAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query for reading playlist titles from the sql response
        private async Task<List<Song>> ReadAllPlaylistTitleslAsync(DbDataReader reader)
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

        //Sql query for reading playlist names from the sql response
        private async Task<List<Playlist>> ReadPlaylistsNamesAsync(DbDataReader reader)
        {
            var names = new List<Playlist>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var name = new Playlist(Db)
                    {
                        Name = reader.GetString(0),
                    };
                    names.Add(name);
                }
            }
            return names;
        }

        //Sql query for reading playlist ids from the sql response
        private async Task<int> GetPlaylistIdAsync(DbDataReader reader)
        {
            int playlistId; 

            using (reader)
            {
                reader.Read();
                
                playlistId = reader.GetInt32(0);

                         
            }
            return playlistId;
        }

        //Sql query for reading playlist song ids from the sql response
        private async Task<List<Song>> ReadAllIdAsync(DbDataReader reader)
        {
            var ids = new List<Song>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var id = new Song(Db)
                    {
                        Id = reader.GetInt32(0),
                    };
                    ids.Add(id);
                }
            }
            return ids;
        }

        //Sql query for reading playlist song artists from the sql response
        private async Task<List<Song>> ReadAllArtistAsync(DbDataReader reader)
        {
            var artists = new List<Song>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var artist = new Song(Db)
                    {
                        Artist = reader.GetString(0),
                    };
                    artists.Add(artist);
                }
            }
            return artists;
        }
    }
}
