using MediaManager;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioApplication
{
    public partial class App : Application
    {
        SongService songService = new SongService();

        public App()
        {
            InitializeComponent();

            CrossMediaManager.Current.Init();

            MainPage = new NavigationPage(new AudioApplication.MainPage(songService));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
