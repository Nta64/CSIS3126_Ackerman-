using Audio.Services;
using MediaManager;
using MediaManager.Library;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;
using static Xamarin.Essentials.Permissions;

namespace Audio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Browse : ContentPage
    {    
       
        public SongService songService = new SongService();
      
        public Browse()
        {       
            BrowseOnAppearing(); 
            InitializeComponent();
        }

        public async void BrowseOnAppearing()
        {            
            await songService.GetSongUrlsAsync(); 

            await songService.GetSongTitlesAsync();

            await songService.GetAlbumUrlsAsync();

            SongView.ItemsSource = songService.Titlelist;
        }

        public async void TapToPlay(object Sender, EventArgs args)
       {           
            string songName = SongView.SelectedItem.ToString();
             
            int SongIndex = Array.IndexOf(songService.Titlelist, songName );

            string CurrentAlbum = songService.ImageList[SongIndex]; 

            string CurrentSong = songService.Urllist[SongIndex];

            int a = 1;

            int b = SongIndex;

            string[] CurrentPlaylist = songService.Urllist; 
          
            await Navigation.PushAsync(new Player(CurrentSong,CurrentAlbum,CurrentPlaylist, a, b), true);

            await CrossMediaManager.Current.Play(CurrentSong);

        }
    }
}