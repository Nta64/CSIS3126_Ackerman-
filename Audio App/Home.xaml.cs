using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home(string userName, SongService songService)
        {
            UserName = userName;
            SongService = songService;

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        public string UserName { get; set; }
        SongService SongService {get; set;} 

        //click events for menu buttons
        public async void NavBrowse(object Sender, EventArgs args)
        {
            await Navigation.PushAsync(new Browse(SongService)); 
        }
        public async void NavYourPlaylists(object Sender, EventArgs args)
        {
            string user = UserName; 
            await Navigation.PushAsync(new Playlists(user, SongService));
        }
        public async void NavFAQ(object Sender, EventArgs args)
        {
            await Navigation.PushAsync(new FAQ());
        }
        public async void NavLogout(object Sender, EventArgs args)
        {
            await Navigation.PushAsync(new MainPage(SongService));
        }
    }
}