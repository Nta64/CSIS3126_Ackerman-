using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Audio.Services;
using Microsoft.CSharp;
using Dropbox.Api.TeamLog;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Audio.Services
{
    public class SongService
    {

        public string testurl;

        public string[] Urllist = new string[9];

        public string[] ImageList = new string[9];

        public string[] Titlelist = new string[9];

        public string[] PlaylistNamelist = new string[9];

        public string[] PlaylistTitlelist = new string[9];

        public SongService()
        {

        }

        public async Task<bool> CreatePlaylistAsync(string tableName)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://webapplication5.conveyor.cloud/CreatePlaylist/" + tableName);
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

        public async Task<bool> AddSongAsync(string tableName, string songName)
        {
            string uri = "https://webapplication5.conveyor.cloud/AddSongToPlaylist/" + tableName + "/" + songName;
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


        public async Task<bool> DeleteSongAsync(int id)
        {
            HttpClient client = new HttpClient();
            string url = "http://192.168.1.152:5294/swagg";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.DeleteAsync("http://10.129.80.238:45457/api/Blog/" + id);

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> AuthenticateUser(string userName, string password)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/AuthenticateUser/" + userName + "/" + password;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
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


        public async Task<string[]> GetSongUrlsAsync()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/api/Song/1";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;

                dynamic tests = JArray.Parse(content);

                foreach (JObject test in tests)
                {
                    Urllist[i] = test.GetValue("url").ToString();
                    i++;
                }

            }
            return await Task.FromResult(Urllist);
        }

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

                dynamic tests = JArray.Parse(content);

                foreach (JObject test in tests)
                {
                    ImageList[i] = test.GetValue("album").ToString();
                    i++;
                }

            }
            return await Task.FromResult(ImageList);
        }

        public async Task<string[]> GetSongTitlesAsync()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/api/Song/Title";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;

                dynamic tests = JArray.Parse(content);

                foreach (JObject test in tests)
                {
                    Titlelist[i] = test.GetValue("title").ToString();
                    i++;
                }

            }
            return await Task.FromResult(Titlelist);
        }

        public async Task<string[]> GetPlaylistsAsync()
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/playlistNames";
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;

                dynamic tests = JArray.Parse(content);

                foreach (JObject test in tests)
                {
                    PlaylistNamelist[i] = test.GetValue("tableName").ToString();
                    i++;
                }

            }
            return await Task.FromResult(PlaylistNamelist);
        }



        public async Task<string[]> GetPlaylistTitlesAsync(string playlistName)
        {
            HttpClient client = new HttpClient();
            string url = "https://webapplication5.conveyor.cloud/playlistTitles/" + playlistName;
            Uri uri = new Uri(string.Format(url, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                int i = 0;
                var content = response.Content.ReadAsStringAsync().Result;

                dynamic tests = JArray.Parse(content);

                foreach (JObject test in tests)
                {
                    PlaylistTitlelist[i] = test.GetValue("title").ToString();
                    i++;
                }

            }
            return await Task.FromResult(PlaylistTitlelist);
        }
    }

}
