using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AudioApplication
{
    public partial class MainPage : ContentPage
    {
        public string Salt = "1mjxFpnRHJ2aJSjPhJIgTw==";
        public MainPage(SongService songService)
        {
            SongService = songService;  
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }
        SongService SongService { get; set; }
        
        //takes user input and sends an http reuest to the web api in order to authenticate the user by hashing the plain text password
        public async void HandleLoginAttempt(object Sender, EventArgs args)
        {
            if (userName.Text != null && password.Text != null)
            {
                string userNameInput = userName.Text;
                string passwordInput = password.Text;

                var result = await SongService.AuthenticateUser(userNameInput, passwordInput + Salt);

                if (result == true)
                {              
                    await Navigation.PushAsync(new Home(userNameInput, SongService), true);
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

        //sets up the page for registering a user
        public void RegisterUser(object Sender, EventArgs args)
        {
            MainLogo.Text = "Register Account";
            login.IsVisible = false;
            SU.IsVisible = false;
            register.IsVisible = true;
            cancel.IsVisible = true;
        }

        //takes user input and creates a user by sending an http request to the api 
        public async void HandleRegisterAttempt(object Sender, EventArgs args)
        {
            string userNameInput = userName.Text;
            string passwordInput = password.Text;
            string saltedPass = passwordInput + Salt;

            var result = await SongService.CreateUser(userNameInput, saltedPass);

            if (result == true)
            {
                await DisplayAlert("Success", "You have successfully created an account and can now login.", "ok");
                ResetPage(Sender, args);
            }
            else
            {
                await DisplayAlert("Error", "That UserName has already been taken, think of a new one and try again.", "ok");
            }
        }

        //brings user back to the login page after a successful login attempt 
        public void ResetPage(object Sender, EventArgs args)
        {
            MainLogo.Text = "Audio";
            login.IsVisible = true;
            SU.IsVisible = true;
            register.IsVisible = false;
            cancel.IsVisible = false;
        }
    }
}
