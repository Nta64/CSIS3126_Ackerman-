using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;
using MediaManager.Library;

namespace AudioApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Browse : ContentPage
    {
        public Browse(SongService songService)
        {
            SongService = songService;
            BrowseOnAppearing();
            InitializeComponent();
            SongView.ItemsSource = Songs;
        }
        public SongService SongService { get; set; }

        //collection of all application songs
        public ObservableCollection<Song> Songs { get { return songs; } }
               ObservableCollection<Song> songs = new ObservableCollection<Song>();

        //get song urls, titles, ids and album urls
        public async void BrowseOnAppearing()
        {
            await SongService.GetSongUrlsAsync();
            await SongService.GetSongTitlesAsync();
            await SongService.GetSongIds();
            await SongService.GetAlbumUrlsAsync();

            for (int i = 0; i < SongService.SongsTitleList.Length; i++)
            {
                Songs.Add(new Song { ID = SongService.SongsIdlist[i], Title = SongService.SongsTitleList[i] });
            }        
        }

        //passes song, album and playlist information to the music player activity and then starts the media player
        public async void TapToPlay(object Sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                string[] CurrentPlaylistUrls = SongService.SongUrlList;
                string[] CurrentPlaylistSongTitles = SongService.SongsTitleList;
                string[] CurrentAlbumImageUrls = SongService.ImageList;

                var song = (Song)e.SelectedItem;
                var songId = song.ID;

                await SongService.GetCurrentSong(songId);
                string CurrentSongUrl = SongService.CurrentSong;

                int a = 1;
                int b = Array.IndexOf(CurrentPlaylistUrls, CurrentSongUrl);

                await SongService.GetCurrentAlbum(songId);
                string CurrentAlbumImageUrl = SongService.CurrentAlbum;

                await SongService.GetArtistsAysnc();
                string[] CurrentPlaylistArtists = SongService.SongsArtistList;
                string CurrentArtist = CurrentPlaylistArtists[e.SelectedItemIndex];

                await Navigation.PushAsync(new Player(CurrentSongUrl, CurrentAlbumImageUrl, CurrentAlbumImageUrls, CurrentPlaylistSongTitles, CurrentPlaylistUrls, CurrentArtist, CurrentPlaylistArtists, a, b, SongService), true);
                await CrossMediaManager.Current.Play(CurrentSongUrl);
            }
            SongView.SelectedItem = null; 
        }

        //filters listview results for searching
        public void SearchChanged(object Sender, TextChangedEventArgs e)
        {
            SongView.ItemsSource = Songs.Where(s => s.Title.StartsWith(e.NewTextValue, StringComparison.OrdinalIgnoreCase));
        }

    }
}