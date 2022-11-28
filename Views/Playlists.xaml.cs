using Audio.Services;
using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace Audio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Playlists : ContentPage
    {
        SongService service = new SongService();
        public string PlaylistName { get; set; }
        public string[] CurrentPlaylistUrls { get; set; }
        public string[] CurrentAlbumUrls { get; set; }
        public string[] CurrentPlaylistSongs { get; set; }
        public string[] SongsList { get; set; }

        public string result;


        public Playlists()
        {
            PlaylistsOnAppearing();
            InitializeComponent();
        }

        public async void PlaylistsOnAppearing()
        {
            await service.GetPlaylistsAsync();
            PlaylistView.ItemsSource = service.PlaylistNamelist;
        }



        async void CreatePlaylist(object sender, EventArgs e)
        {
            result = await DisplayPromptAsync("Create a Playlist", "Give your playlist a name.");

            if (!string.IsNullOrWhiteSpace(result))
            {
                await service.CreatePlaylistAsync(result);

                await service.GetPlaylistsAsync();

                PlaylistView.ItemsSource = null;
                PlaylistView.ItemsSource = service.PlaylistNamelist;
            }
        }

        public async void TapTo(object Sender, EventArgs args)
        {
            if (PlaylistView.SelectedItem != null)
            {
                PlaylistName = PlaylistView.SelectedItem.ToString();

                await service.GetAlbumUrlsAsync();
                CurrentAlbumUrls = service.ImageList;

                await service.GetSongUrlsAsync();

                CurrentPlaylistUrls = service.Urllist;

                await service.GetPlaylistTitlesAsync(PlaylistName);

                CurrentPlaylistSongs = service.PlaylistTitlelist;

                await service.GetSongTitlesAsync();

                SongsList = service.Titlelist;



                await Navigation.PushAsync(new PlaylistView(PlaylistName, CurrentPlaylistUrls, CurrentAlbumUrls, CurrentPlaylistSongs, SongsList), true);
            }
        }

    }


}


