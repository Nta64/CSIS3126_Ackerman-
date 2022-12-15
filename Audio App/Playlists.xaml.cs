using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Playlists : ContentPage
    {
        public Playlists(string userName, SongService songService)
        {
            UserName = userName;
            SongService = songService;
            PlaylistsOnAppearing();
            InitializeComponent();
            PlaylistView.ItemsSource = playlistTitles;
        }
        public SongService SongService { get; set; }

        //collection of all playlist titles
               ObservableCollection<Playlist> playlistTitles = new ObservableCollection<Playlist>();
        public ObservableCollection<Playlist> PlaylistTitles { get { return playlistTitles; } }  

        public int PlaylistID { get; set; }
        public int UserId { get; set; }
        public string result { get; set; }
        public string UserName { get; set; }
        public string PlaylistName { get; set; }
        public string[] CurrentPlaylistSongUrls { get; set; }
        public string[] CurrentAlbumImageUrls { get; set; }
        public string[] CurrentPlaylistSongTitles { get; set; }
        public int[] CurrentPlaylistSongIds { get; set; }
        public string[] SongsTitleList { get; set; }
        
        //gets userid and all playlist information associated with that id
        public async void PlaylistsOnAppearing()
        {
            UserId = await SongService.GetUserIDAsync(UserName);
            await SongService.GetPlaylistsAsync(UserId);
            await SongService.GetAllPlaylistIdAsync(UserId);

            for (int i = 0; i < SongService.PlaylistNameList.Length; i++)
            {
                PlaylistTitles.Add(new Playlist { ID = SongService.PlaylistIdList[i], Name = SongService.PlaylistNameList[i] });
            }
        }

        //takes user input for the playlist name and sends an http request to the web api to create a playlist
        public async void CreatePlaylist(object sender, EventArgs e)
        {
            result = await DisplayPromptAsync("Create a Playlist", "Give your playlist a name.");

            if (!string.IsNullOrWhiteSpace(result))
            {
                await SongService.CreatePlaylistAsync(UserId, result);

                int newId = await SongService.GetPlaylistIdAsync(UserId, result);

                PlaylistTitles.Add(new Playlist { ID = newId, Name = result });

                PlaylistView.ItemsSource = null;
                PlaylistView.ItemsSource = playlistTitles;
            }
        }

        //takes user input for the playlist name and sends an http request to the web api to delete a playlist
        public async void DeletePlaylist(object sender, SelectedItemChangedEventArgs e)
        {        
            var playlist = (Playlist)e.SelectedItem;
            int playlistId = playlist.ID;
            string playlistName = playlist.Name;         

            if (!string.IsNullOrWhiteSpace(playlistName))
            {
                await SongService.DeletePlaylistAsync(playlistId);
                await SongService.GetPlaylistsAsync(UserId);
                await SongService.GetAllPlaylistIdAsync(UserId);

                PlaylistTitles.Remove((Playlist)e.SelectedItem);

                PVTitle.Text = null;
                create.IsVisible = true;
                delete.IsVisible = true;
                cancle.IsVisible = false;
                DeleteView.IsVisible = false;
                PlaylistView.IsVisible = true;
                PlaylistView.ItemsSource = null;
                PlaylistView.ItemsSource = playlistTitles;
            }
        }

        //takes user input and navigates to a playlist view page for the selected playlist
        public async void TapTo(object Sender, SelectedItemChangedEventArgs e)
        {
                PlaylistView.SelectedItem = null; 

            if (PlaylistView.SelectedItem != null)
            {
                var playlist = (Playlist)e.SelectedItem;
                PlaylistName = playlist.Name;

                await SongService.GetSongTitlesAsync();
                SongsTitleList = SongService.SongsTitleList;

                await SongService.GetPlaylistIdAsync(UserId, PlaylistName);
                PlaylistID = SongService.PlaylistID;

                await SongService.GetPlaylistAlbumUrlsAsync(PlaylistID);
                CurrentAlbumImageUrls = SongService.PlaylistImageList;

                await SongService.GetPlaylistSongUrlsAsync(PlaylistID);
                CurrentPlaylistSongUrls = SongService.PlaylistSongUrlList;

                await SongService.GetPlaylistSongTitlesAsync(PlaylistID);
                CurrentPlaylistSongTitles = SongService.PlaylistTitleList;

                await SongService.GetPlaylistSongIds(PlaylistID);
                CurrentPlaylistSongIds = SongService.PlaylistSongsIdlist;

                await SongService.GetSongIds();

                await Navigation.PushAsync(new PlaylistView(UserId, PlaylistID, PlaylistName, SongsTitleList, CurrentAlbumImageUrls, CurrentPlaylistSongUrls, CurrentPlaylistSongTitles, CurrentPlaylistSongIds, SongService), true);
            }
        }

        //on click event handler for setting up the delete playlists page
        public void DeleteButton(object Sender, EventArgs e)
        {
            cancle.IsVisible = true;
            create.IsVisible = false;
            delete.IsVisible = false;
            DeleteView.IsVisible = true;
            PlaylistView.IsVisible = false;
            DeleteView.ItemsSource = playlistTitles;
            PVTitle.Text = "Choose a playlist to remove";
        }

        //on click event handler for reseting the playlists page 
        public void Back(object Sender, EventArgs args)
        {
            delete.IsVisible = true;
            create.IsVisible = true;
            cancle.IsVisible = false;
            DeleteView.IsVisible = false;
            PlaylistView.IsVisible = true;
            PlaylistView.ItemsSource = null;
            PVTitle.Text = PlaylistName;
            PlaylistView.ItemsSource = PlaylistTitles;
        }
    }
}