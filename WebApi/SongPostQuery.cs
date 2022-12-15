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

        //Sql query that gets all the latest song urls
        public async Task<List<Song>> LatestSongUrls()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Url` FROM `appsongs`;";
            return await ReadAllUrlAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets the latest playlist song urls for a specific playlist
        public async Task<List<Song>> LatestPlaylistSongUrls(int PlaylistID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT appsongs.Url FROM appsongs INNER JOIN playlist_songs ON appsongs.Song_id = playlist_songs.Song_id INNER JOIN playlists ON playlist_songs.Playlist_id = playlists.Playlist_id WHERE  playlists.Playlist_id = "+ PlaylistID +"; ";
            return await ReadAllUrlAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets the latest playlist album urls for a specific playlist
        public async Task<List<Song>> LatestPlaylistSongAlbumImages(int PlaylistID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT appsongs.Album FROM appsongs INNER JOIN playlist_songs ON appsongs.Song_id = playlist_songs.Song_id INNER JOIN playlists ON playlist_songs.Playlist_id = playlists.Playlist_id WHERE  playlists.Playlist_id = " + PlaylistID + "; ";
            return await ReadAllImageAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets all the latest album urls
        public async Task<List<Song>> LatestImages()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Album` FROM `appsongs`;";
            return await ReadAllImageAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets all the latest song titles
        public async Task<List<Song>> LatestTitles()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Title` FROM `appsongs`;";
            return await ReadAllTitleAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets all the latest song ids
        public async Task<List<Song>> LatestSongIds()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Song_id` FROM `appsongs`;";
            return await ReadAllIdAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets the latest playlist song ids for a specific playlist
        public async Task<List<Song>> LatestPlaylistSongIds(int PlaylistID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Song_id` FROM `playlist_songs` WHERE Playlist_id = " + PlaylistID + ";";
            return await ReadAllIdAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets the current song url for a specific song
        public async Task<List<Song>> LatestSong(int SongId)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Url` FROM `appsongs` WHERE Song_id = "+SongId+";";
            return await ReadAllUrlAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets the current album url for a specific song
        public async Task<List<Song>> LatestAlbum(int SongId)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Album` FROM `appsongs` WHERE Song_id = " + SongId + ";";
            return await ReadAllImageAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that gets all the latest song artists
        public async Task<List<Song>> LatestArtists()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Artist` FROM `appsongs`;";
            return await ReadAllArtistAsync(await cmd.ExecuteReaderAsync());
        }

        //Sql query that reads all song urls
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

        //Sql query that reads all album urls
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

        //Sql query that reads all song titles
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

        //Sql query that reads all ids
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

        //Sql query that reads all song artists
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
