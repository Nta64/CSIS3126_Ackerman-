using MediaManager;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Audio
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            CrossMediaManager.Current.Init();
            MainPage = new NavigationPage(new MainPage());
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
