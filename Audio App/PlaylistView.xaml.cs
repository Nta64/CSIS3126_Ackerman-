using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistView : ContentPage
    {
        public PlaylistView(int userId, int playlistId, string playlistName, string[] songsList, string[] currentAlbumImageUrls, string[] currentPlaylistSongUrls, string[] currentPlaylistSongTitles, int[] currentPlaylistSongIds, SongService songService)
        {
            UserId = userId;
            PlaylistId = playlistId;
            PlaylistName = playlistName;
            SongsList = songsList;
            CurrentAlbumImageUrls = currentAlbumImageUrls;
            CurrentPlaylistSongUrls = currentPlaylistSongUrls;
            CurrentPlaylistSongTitles = currentPlaylistSongTitles;
            CurrentPlaylistSongIds = currentPlaylistSongIds;
            SongService = songService;

            InitializeComponent();
            PlaylistViewOnAppearing();

            CurrentPlaylistView.ItemsSource = PlaylistSongs;
        }
        public SongService SongService { get; set; }

        //observable collections for both current playlist songs and all songs
        public ObservableCollection<Song> PlaylistSongs { get { return playlistSongs; } }
               ObservableCollection<Song> playlistSongs = new ObservableCollection<Song>();
        public ObservableCollection<Song> Songs { get { return songs; } } 
               ObservableCollection<Song> songs = new ObservableCollection<Song>();
 
        public int A { get; set; }
        public int B { get; set; }
        public int UserId { get; set; }
        public int PlaylistId { get; set; }
        public string CurrentSong { get; set; }
        public string UserName { get; set; }
        public string PlaylistName { get; set; }
        public string[] SongsList { get; set; }
        public string[] CurrentAlbumImageUrls { get; set; }
        public string[] CurrentPlaylistSongUrls { get; set; }
        public string[] CurrentPlaylistSongTitles { get; set; }
        public int[] CurrentPlaylistSongIds { get; set; }
        public string[] CurrentPlaylistArtists { get; set; }

        //sets page title and fills observable collections
        public void PlaylistViewOnAppearing()
        {
            PlaylistTitle.Text = PlaylistName;

            for (int i = 0; i < SongsList.Length; i++)
            {
                Songs.Add(new Song { ID = SongService.SongsIdlist[i], Title = SongService.SongsTitleList[i] });
            }

            for (int i = 0; i < CurrentPlaylistSongTitles.Length; i++)
            {
                PlaylistSongs.Add(new Song { ID = CurrentPlaylistSongIds[i], Title = CurrentPlaylistSongTitles[i] });
            }
        }

        //takes user input to add a song to the playlist by sending an http request to the web api
        public async void TapToAdd(object Sender, SelectedItemChangedEventArgs e)
        {
            var song = (Song)e.SelectedItem;
            var songId = song.ID;
            var songTitle = song.Title; 

            await SongService.AddSongAsync(songId, PlaylistId);
            await SongService.GetPlaylistSongTitlesAsync(PlaylistId);
            await SongService.GetPlaylistSongIds(PlaylistId);
            
            PlaylistSongs.Add(new Song { ID = songId, Title = songTitle });
            AddSongToPlaylistView.IsVisible = false;
            PlaylistTitle.Text = PlaylistName;
            CurrentPlaylistView.IsVisible = true;
            CurrentPlaylistView.ItemsSource = null;
            CurrentPlaylistView.ItemsSource = PlaylistSongs;
            AddSongButton.IsVisible = true;
            RemoveSongButton.IsVisible = true;
            cancle.IsVisible = false;
        }

        //takes user input to remove a song to the playlist by sending an http request to the web api
        public async void TapToRemove(object Sender, SelectedItemChangedEventArgs e)
        {
            var song = (Song)e.SelectedItem;
            var songId = song.ID;
            var songTitle = song.Title;

            await SongService.RemoveSongAsync(songId, PlaylistId);
            await SongService.GetPlaylistAristsAsync(PlaylistId);
            CurrentPlaylistArtists = SongService.PlaylistSongsArtistList;
            await SongService.GetPlaylistSongUrlsAsync(PlaylistId);
            CurrentPlaylistSongUrls = SongService.PlaylistSongUrlList;
            await SongService.GetPlaylistSongTitlesAsync(PlaylistId);
            CurrentPlaylistSongTitles = SongService.PlaylistTitleList;
            await SongService.GetPlaylistAlbumUrlsAsync(PlaylistId);
            CurrentAlbumImageUrls = SongService.PlaylistImageList;
            await SongService.GetPlaylistSongIds(PlaylistId); 
            PlaylistSongs.Remove((Song)e.SelectedItem); 
            RemoveSongFromPlaylistView.IsVisible = false;
            cancle.IsVisible = false;
            PlaylistTitle.Text = PlaylistName;
            CurrentPlaylistView.IsVisible = true;
            CurrentPlaylistView.ItemsSource = null;           
            CurrentPlaylistView.ItemsSource = PlaylistSongs;
            AddSongButton.IsVisible = true;
            RemoveSongButton.IsVisible = true;
        }

        //takes user input to play a song by passing a song url to the player 
        public async void TapToPlay(object Sender, SelectedItemChangedEventArgs e)
        {
               
            if (CurrentPlaylistView.SelectedItem != null)
            {
                var song = (Song)e.SelectedItem;
                var songId = song.ID;
                await SongService.GetCurrentSong(songId);
                CurrentSong = SongService.CurrentSong;
                await SongService.GetCurrentAlbum(songId);
                string CurrentAlbumImageUrl = SongService.CurrentAlbum;
                await SongService.GetPlaylistAristsAsync(PlaylistId);
                CurrentPlaylistArtists = SongService.PlaylistSongsArtistList; 
                string CurrentArtist = SongService.PlaylistSongsArtistList[e.SelectedItemIndex];
                await SongService.GetPlaylistSongUrlsAsync(PlaylistId);
                CurrentPlaylistSongUrls = SongService.PlaylistSongUrlList; 
                await SongService.GetPlaylistSongTitlesAsync(PlaylistId);
                CurrentPlaylistSongTitles = SongService.PlaylistTitleList;
                await SongService.GetPlaylistAlbumUrlsAsync(PlaylistId);
                CurrentAlbumImageUrls = SongService.PlaylistImageList;
                AddSongButton.IsVisible = true;
                RemoveSongButton.IsVisible = true;
                A = 1;
                B = e.SelectedItemIndex;
                await Navigation.PushAsync(new Player(CurrentSong, CurrentAlbumImageUrl, CurrentAlbumImageUrls, CurrentPlaylistSongTitles, CurrentPlaylistSongUrls, CurrentArtist, CurrentPlaylistArtists, A, B, SongService), true);
                await CrossMediaManager.Current.Play(CurrentSong);
            }
            CurrentPlaylistView.SelectedItem = null;
        }

        //sets up the page to add a song
        public void AddSong(object Sender, EventArgs args)
        {
            PlaylistTitle.Text = "Pick a Song to add";
            CurrentPlaylistView.IsVisible = false;
            AddSongToPlaylistView.IsVisible = true;
            cancle.IsVisible = true;
            AddSongToPlaylistView.ItemsSource = Songs;
            AddSongButton.IsVisible = false;
            RemoveSongButton.IsVisible = false;
        }

        //sets up the page to remove a song
        public void RemoveSong(object Sender, EventArgs args)
        {
            PlaylistTitle.Text = "Pick a Song to remove";
            AddSongButton.IsVisible = false;
            RemoveSongButton.IsVisible = false;
            cancle.IsVisible = true;
            CurrentPlaylistView.IsVisible = false;
            AddSongToPlaylistView.IsVisible = false; 
            RemoveSongFromPlaylistView.IsVisible = true;
            RemoveSongFromPlaylistView.ItemsSource = playlistSongs;         
        }

        //filters list view results for searching 
        public void SearchChanged(object Sender, TextChangedEventArgs e)
        {
            CurrentPlaylistView.ItemsSource = PlaylistSongs.Where(s => s.Title.StartsWith(e.NewTextValue, StringComparison.OrdinalIgnoreCase));
        }

        //resets the page for vewing the playlist
        public void Back(object Sender, EventArgs args)
        {

            RemoveSongFromPlaylistView.IsVisible = false;
            AddSongToPlaylistView.IsVisible = false; 
            PlaylistTitle.Text = PlaylistName;
            CurrentPlaylistView.IsVisible = true;
            CurrentPlaylistView.ItemsSource = null;
            CurrentPlaylistView.ItemsSource = PlaylistSongs;
            AddSongButton.IsVisible = true;
            RemoveSongButton.IsVisible = true;
            cancle.IsVisible = false;
        }
    }
}