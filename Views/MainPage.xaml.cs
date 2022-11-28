
using Audio.Services;
using Dropbox.Api.TeamLog;
using MediaManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Audio
{
    public partial class MainPage : ContentPage
    {
        SongService songService = new SongService();
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        public async void HandleLoginAttempt(object Sender, EventArgs args)
        {
            if (userName.Text != null && password.Text != null)
            {

                string userNameInput = userName.Text;

                string passwordInput = password.Text;


                var result = await songService.AuthenticateUser(userNameInput, passwordInput);

                if (result == true)
                {
                    await Navigation.PushAsync(new Home(), true);
                }
                else
                {
                    await DisplayAlert("Error", "Incorrect username or password", "ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please enter a username and password.", "ok");
            }

        }
        public void RegisterUser(object Sender, EventArgs args)
        {
            MainLogo.Text = "Register Account";

            login.IsVisible = false;
            FP.IsVisible = false;
            SU.IsVisible = false;
            register.IsVisible = true;
            cancel.IsVisible = true;
        }

        public async void HandleRegisterAttempt(object Sender, EventArgs args)
        {
            string userNameInput = userName.Text;

            string passwordInput = password.Text;

            var result = await songService.CreateUser(userNameInput, passwordInput);

            if (result == true)
            {
                await DisplayAlert("Success", "You have successfully created an account and can now login.", "ok");

                ResetPage(Sender, args);
            }
            else
            {
                await DisplayAlert("Error", "There has been an issue while trying to create your account, please contact customer support.", "ok");
            }
        }

        public void ResetPage(object Sender, EventArgs args)
        {
            MainLogo.Text = "Audio";

            login.IsVisible = true;
            FP.IsVisible = true;
            SU.IsVisible = true;
            register.IsVisible = false;
            cancel.IsVisible = false;
        }
    }
}
