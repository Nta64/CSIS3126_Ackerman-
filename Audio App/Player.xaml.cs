using MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using MySqlConnector;
using System.Net.Http;
using Newtonsoft.Json;
using MediaManager.Library;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace AudioApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Player : ContentPage
    {
        SongService SongService { get; set; }

        public Player(string currentSongUrl, string currentAlbumImageUrl, string[] currentAlbumImageUrls, string[] currentPlaylistSongTitles, string[] currentPlaylistSongUrls, string currentArtist, string[] currentPlaylistArtists, int a, int b, SongService songService)
        {
            A = a;
            B = b;

            CurrentSongUrl = currentSongUrl;
            CurrentAlbumImageUrl = currentAlbumImageUrl;
            CurrentAlbumImageUrls = currentAlbumImageUrls;
            CurrentPlaylistSongTitles = currentPlaylistSongTitles;
            CurrentPlaylistSongUrls = currentPlaylistSongUrls;
            CurrentPlaylistArtists = currentPlaylistArtists;
            CurrentArtist = currentArtist;
            SongService = songService;

            InitializeComponent();
            PlaylistOnAppearing();
        }

        public int A { get; set; }
        public int B { get; set; }
        public string CurrentSongUrl { get; set; }
        public string CurrentAlbumImageUrl { get; set; }
        public string CurrentArtist { get; set; }
        public string[] CurrentAlbumImageUrls { get; set; }
        public string[] CurrentPlaylistSongTitles { get; set; }
        public string[] CurrentPlaylistSongUrls { get; set; }
        public string[] CurrentPlaylistArtists { get; set; }
       
        public async void PlaylistOnAppearing()
        {
            await SongService.GetAlbumUrlsAsync();
            Album.Source = CurrentAlbumImageUrl;
            Title.Text = CurrentPlaylistSongTitles[B];
            Artist.Text = CurrentArtist; 
        }
        public async void Playsong(object Sender, EventArgs args)
        {
            if (A == 0)
            {
                await CrossMediaManager.Current.Play(CurrentSongUrl);
                A++;
            }
            else if (A == 1)
            {
                await CrossMediaManager.Current.Pause();
                A--;            
            }
        }
        public async void RewindSong(object Sender, EventArgs args)
        {
            if (B != 0)
            {
                B--;           
                Album.Source = CurrentAlbumImageUrls[B].ToString();
                Title.Text = CurrentPlaylistSongTitles[B];
                Artist.Text = CurrentPlaylistArtists[B];
                CurrentSongUrl = CurrentPlaylistSongUrls[B];
                A = 1; 
                await CrossMediaManager.Current.Play(CurrentPlaylistSongUrls[B]);
            }
        }
        public async void ForwardSong(object Sender, EventArgs args)
        {
            if (B  != CurrentPlaylistSongUrls.Length-1 )
            {
                B++;         
                Album.Source = CurrentAlbumImageUrls[B].ToString();
                Title.Text = CurrentPlaylistSongTitles[B];
                Artist.Text = CurrentPlaylistArtists[B];
                CurrentSongUrl = CurrentPlaylistSongUrls[B];
                A = 1; 
                await CrossMediaManager.Current.Play(CurrentPlaylistSongUrls[B]);
            }
        }
    }
}