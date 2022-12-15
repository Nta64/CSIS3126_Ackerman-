using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.CSharp;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using System.Web;
using static System.Net.Mime.MediaTypeNames;


namespace AudioApplication
{
    public class SongService
    {
        public SongService() { }

        public int UserID;
        public int PlaylistID;
        public string CurrentSong;
        public string CurrentAlbum;
        public int[] SongsIdlist { get; set; }
        public int[] PlaylistSongsIdlist { get; set; }
        public int[] PlaylistIdList { get; set; }
        public string[] ImageList { get; set; }
        public string[] SongUrlList { get; set; }
        public string[] PlaylistSongUrlList { get; set; }
        public string[] PlaylistImageList { get; set; }
        public string[] SongsTitleList { get; set; }
        public string[] SongsArtistList { get; set; }
        public string[] PlaylistSongsArtistList { get; set; }
        public string[] PlaylistNameList { get; set; }
        public string[] PlaylistTitleList { get; set; }

        //http request to add a playlist to the database for a specific user
        public async Task<bool> CreatePlaylistAsync(int UserId, string PlaylistName)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://webapplication5.conveyor.cloud/CreatePlaylist/" + UserId + "/" + PlaylistName);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        //http request to delete a playlist from the database
        public async Task<bool> DeletePlaylistAsync(int playlistId)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://webapplication5.conveyor.cloud/DeletePlaylist/" + playlistId);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        //http request to add a song to a playlist on the database
        public async Task<bool> AddSongAsync(int SongId, int PlaylistId)
        {
            string uri = "https://webapplication5.conveyor.cloud/AddSongToPlaylist/" + SongId + "/" + PlaylistId;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        //http request to remove a song from the database
        public async Task<bool> RemoveSongAsync(int SongId, int PlaylistId)
        {
            string uri = "https://webapplication5.conveyor.cloud/RemoveSongFromPlaylist/" + SongId + "/" + PlaylistId;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        //http request to authenticate a user that has already been added to the database
        public async Task<bool> AuthenticateUser(string userName, string password)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/AuthenticateUser/" + userName + "/" + password;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                if (content == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        //http request to create and add a user to the databse
        public async Task<bool> CreateUser(string userName, string password)
        {
            HttpClient client = new HttpClient();
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://webapplication5.conveyor.cloud/CreateUser/" + userName + "/" + password + "/" + dateTime);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //http request to get all application song urls
        public async Task<string[]> GetSongUrlsAsync()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/SongUrls";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic songUrls = JArray.Parse(content);
                int length = songUrls.Count;

                SongUrlList = new string[length];

                foreach (JObject songUrl in songUrls)
                {
                    SongUrlList[i] = songUrl.GetValue("url").ToString();
                    i++;
                }
            }
            return await Task.FromResult(SongUrlList);
        }

        //http request to get the playlist song urls
        public async Task<string[]> GetPlaylistSongUrlsAsync(int PlaylistID)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/PlaylistSongUrls/" + PlaylistID;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic playlistSongUrls = JArray.Parse(content);
                int length = playlistSongUrls.Count;

                PlaylistSongUrlList = new string[length];

                foreach (JObject playlistSongUrl in playlistSongUrls)
                {
                    PlaylistSongUrlList[i] = playlistSongUrl.GetValue("url").ToString();
                    i++;
                }
            }
            return await Task.FromResult(PlaylistSongUrlList);
        }

        //http request to get the current song url
        public async Task<string> GetCurrentSong(int SongId)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetCurrentSong/" + SongId;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                dynamic playlistSongUrls = JArray.Parse(content);

                foreach (JObject playlistSongUrl in playlistSongUrls)
                {
                    CurrentSong = playlistSongUrl.GetValue("url").ToString();

                }

            }
            return await Task.FromResult(CurrentSong);
        }

        //http request to get the current album url
        public async Task<string> GetCurrentAlbum(int SongId)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetCurrentAlbum/" + SongId;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic albums = JArray.Parse(content);

                foreach (JObject album in albums)
                {
                    CurrentAlbum = album.GetValue("album").ToString();
                }
            }
            return await Task.FromResult(CurrentAlbum);
        }

        //http request to get all application album urls
        public async Task<string[]> GetAlbumUrlsAsync()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetSongAlbumImages";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic albums = JArray.Parse(content);
                int length = albums.Count;

                ImageList = new string[length];

                foreach (JObject album in albums)
                {
                    ImageList[i] = album.GetValue("album").ToString();
                    i++;
                }
            }
            return await Task.FromResult(ImageList);
        }

        //http request to get the current playlist album urls
        public async Task<string[]> GetPlaylistAlbumUrlsAsync(int PlaylistID)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetPlaylistAlbumImages/" + PlaylistID;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic playlistAlbums = JArray.Parse(content);
                int length = playlistAlbums.Count;

                PlaylistImageList = new string[length];

                foreach (JObject playlistAlbum in playlistAlbums)
                {
                    PlaylistImageList[i] = playlistAlbum.GetValue("album").ToString();
                    i++;
                }
            }
            return await Task.FromResult(PlaylistImageList);
        }

        //http request to get all application song titles
        public async Task<string[]> GetSongTitlesAsync()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetTitles";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic titles = JArray.Parse(content);
                int length = titles.Count;

                SongsTitleList = new string[length];

                foreach (JObject title in titles)
                {
                    SongsTitleList[i] = title.GetValue("title").ToString();
                    i++;
                }
            }
            return await Task.FromResult(SongsTitleList);
        }

        //http request to get all application song ids
        public async Task<int[]> GetSongIds()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetSongIds";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic ids = JArray.Parse(content);
                int length = ids.Count;

                SongsIdlist = new int[length];

                foreach (JObject id in ids)
                {
                    SongsIdlist[i] = Convert.ToInt32(id.GetValue("id"));
                    i++;
                }
            }
            return await Task.FromResult(SongsIdlist);
        }

        //http request to get the current playlist song ids
        public async Task<int[]> GetPlaylistSongIds(int PlaylistId)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetPlaylistSongIds/" + PlaylistId;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic ids = JArray.Parse(content);
                int length = ids.Count;

                PlaylistSongsIdlist = new int[length];

                foreach (JObject id in ids)
                {
                    PlaylistSongsIdlist[i] = Convert.ToInt32(id.GetValue("id"));
                    i++;
                }
            }
            return await Task.FromResult(PlaylistSongsIdlist);
        }

        //http request to get the playlist song artists 
        public async Task<string[]> GetPlaylistAristsAsync(int PlaylistId)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetPlaylistArtists/" + PlaylistId;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic artists = JArray.Parse(content);
                int length = artists.Count;

                PlaylistSongsArtistList = new string[length];

                foreach (JObject artist in artists)
                {
                    PlaylistSongsArtistList[i] = artist.GetValue("artist").ToString();
                    i++;
                }
            }
            return await Task.FromResult(SongsArtistList);
        }

        //http request to get all application artists
        public async Task<string[]> GetArtistsAysnc()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/GetArtists";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic artists = JArray.Parse(content);
                int length = artists.Count;

                SongsArtistList = new string[length];

                foreach (JObject artist in artists)
                {
                    SongsArtistList[i] = artist.GetValue("artist").ToString();
                    i++;
                }
            }
            return await Task.FromResult(SongsArtistList);
        }

        //http request to get the playlist names for a specific user
        public async Task<string[]> GetPlaylistsAsync(int UserId)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/playlistNames/" + UserId;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic names = JArray.Parse(content);
                int length = names.Count;

                PlaylistNameList = new string[length];

                foreach (JObject name in names)
                {
                    PlaylistNameList[i] = name.GetValue("name").ToString();
                    i++;
                }
            }
            return await Task.FromResult(PlaylistNameList);
        }

        //http request to get the user id for a specific user
        public async Task<int> GetUserIDAsync(string userName)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/getUserId/" + userName;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic ids = JArray.Parse(content);

                foreach (JObject id in ids)
                {
                    UserID = Convert.ToInt32(id.GetValue("user_id"));
                    i++;
                }
            }
            return await Task.FromResult(UserID);
        }

        //http request to get a specific playlist id for a specific user
        public async Task<int> GetPlaylistIdAsync(int UserId, string PlaylistName)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/getPlaylistId/" + UserId + "/" + PlaylistName;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                PlaylistID = Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
            }
            return await Task.FromResult(PlaylistID);
        }
        //http request to get all playlist ids for a specific user
        public async Task<int[]> GetAllPlaylistIdAsync(int UserId)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/getAllPlaylistId/" + UserId ;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;

                dynamic ids = JArray.Parse(content);

                int length = ids.Count;

                PlaylistIdList = new int[length];

                foreach (JObject id in ids)
                {
                    PlaylistIdList[i] = Convert.ToInt32(id.GetValue("id"));
                    i++;
                }
            }
            return await Task.FromResult(PlaylistIdList);
        }

        //http request to get all playlist song titles for a specific playlist 
        public async Task<string[]> GetPlaylistSongTitlesAsync(int PlaylistID)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/playlistTitles/" + PlaylistID;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic tests = JArray.Parse(content);
                int length = tests.Count;

                PlaylistTitleList = new string[length];

                foreach (JObject test in tests)
                {
                    PlaylistTitleList[i] = test.GetValue("title").ToString();
                    i++;
                }
            }
            return await Task.FromResult(PlaylistTitleList);
        }
    }
}
