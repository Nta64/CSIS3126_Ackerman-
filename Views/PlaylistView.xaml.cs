using Audio.Services;
using Dropbox.Api.Team;
using Dropbox.Api.TeamLog;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Audio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistView : ContentPage
    {
        // Playlists playlist = new Playlists(); 

        public string[] CurrentPlaylistUrls { get; set; }

        public string[] CurrentAlbumUrls { get; set; }

        public string[] CurrentPlaylistSongs { get; set; }

        public string[] SongsList { get; set; }

        public string PlaylistName { get; set; }

        SongService songService = new SongService();



        public PlaylistView(string playlistName, string[] currentPlaylistUrls, string[] currentAlbumUrls, string[] currentPlaylistSongs, string[] songsList)
        {
            PlaylistName = playlistName;

            CurrentPlaylistUrls = currentPlaylistUrls;

            CurrentAlbumUrls = currentAlbumUrls;

            CurrentPlaylistSongs = currentPlaylistSongs;

            SongsList = songsList;



            InitializeComponent();

            PlaylistViewOnAppearing();
        }



        public async void PlaylistViewOnAppearing()
        {
            PlaylistTitle.Text = PlaylistName;

            await songService.GetPlaylistTitlesAsync(PlaylistName);

            await songService.GetSongTitlesAsync();

            await songService.GetSongUrlsAsync();

            SongsList = songService.Titlelist;

            CurrentPlaylistUrls = songService.Urllist;

            PlaylistTitle.Text = PlaylistName;

            CurrentPlaylistView.ItemsSource = songService.PlaylistTitlelist;
        }


        public async void TapToPlaylist(object Sender, EventArgs args)
        {
            await songService.GetPlaylistTitlesAsync(PlaylistName);

            await songService.GetSongTitlesAsync();

            string songName = CurrentPlaylistView.SelectedItem.ToString();

            int SongIndex = Array.IndexOf(songService.Titlelist, songName);

            string songTEst = songService.Titlelist[2];

            string CurrentAlbum = CurrentAlbumUrls[SongIndex];

            string CurrentSong = CurrentPlaylistUrls[SongIndex];

            int a = 1;

            int b = SongIndex;

            string[] CurrentPlaylist = CurrentPlaylistUrls;


            await Navigation.PushAsync(new Player(CurrentSong, CurrentAlbum, CurrentPlaylist, a, b), true);


            Player player = new Player(CurrentSong, CurrentAlbum, CurrentPlaylist, a, b);

            await CrossMediaManager.Current.Play(CurrentSong);


        }

        public void AddSong(object Sender, EventArgs args)
        {
            PlaylistTitle.Text = "Pick a Song to add";

            CurrentPlaylistView.IsVisible = false;

            addSongSearch.IsVisible = true;

            AddSongToPlaylistView.IsVisible = true;

            AddSongToPlaylistView.ItemsSource = SongsList;
        }

        public async void TapToAdd(object Sender, EventArgs args)
        {

            string songName = AddSongToPlaylistView.SelectedItem.ToString();

            await songService.AddSongAsync(PlaylistName, songName);

            await songService.GetPlaylistTitlesAsync(PlaylistName);

            addSongSearch.IsVisible = false;

            AddSongToPlaylistView.IsVisible = false;

            PlaylistTitle.Text = PlaylistName;

            CurrentPlaylistView.IsVisible = true;

            CurrentPlaylistView.ItemsSource = null;

            CurrentPlaylistView.ItemsSource = songService.PlaylistTitlelist;

        }

    }

}


