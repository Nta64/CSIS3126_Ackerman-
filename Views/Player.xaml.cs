using MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audio.Models;
using Audio.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using MySqlConnector;
using System.Net.Http;
using Newtonsoft.Json;
using MediaManager.Library;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Collections.ObjectModel;
using Org.BouncyCastle.Asn1.Mozilla;
using static System.Net.Mime.MediaTypeNames;
using static Dropbox.Api.TeamLog.ActorLogInfo;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace Audio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public  partial class Player
    {
      
        SongService songService = new SongService();

       // SongTimer songTimer = new SongTimer();
        public SongService[] Playlists { get; set; }
      
        public string CurrentSong { get; set; }
        public string CurrentAlbum { get; set; }

        public int currenttime { get { return currenttime; } set { Convert.ToDouble(CrossMediaManager.Current.Position.TotalSeconds); } }

        public string[] CurrentPlaylist { get; set; }

        

        public int A { get; set; }
        public int B { get; set; }

        public Player(string currentSong,string currentAlbum, string[] currentPlaylist, int a, int b)
        {
        
            CurrentSong = currentSong;

            CurrentPlaylist = currentPlaylist;

            CurrentAlbum = currentAlbum; 

            A = a;

            B = b; 

            PlaylistOnAppearing();

            InitializeComponent();
            
        }

        public void PlaylistOnAppearing()
        {
            
           // _timer = new Timer(TimeSpan.FromSeconds(1));
            Album.Source = CurrentAlbum;
           // string test = "";       
        }

        public async void startSlider(CancellationToken token)
        {
             await Task.Run(() =>
                    {
                         var s = CrossMediaManager.Current.Position.Duration().TotalSeconds;

                         int t = Convert.ToInt32(CrossMediaManager.Current.Duration.TotalSeconds);

                         for (int i = 0; i < t; i++)
                         {
                             Thread.Sleep(1000);

                             songProgress.Progress = Convert.ToDouble("00." + i.ToString());

                             SongSlider.Maximum = t;

                             SongSlider.Value = i;

                             if (token.IsCancellationRequested)
                             {
                                return; 
                             }
                         }

                    });
        }

        
        public async void Playsong(object Sender, EventArgs args)
        {

            int i = 0;

        //    int songLength = Convert.ToInt32(CrossMediaManager.Current.Duration.TotalSeconds);
          
           
            if (A == 0)
            {
                await CrossMediaManager.Current.Play(CurrentSong);
                
                A++;

              //  while (songLength > i)
             //   {
                //    i++;
              //      SongSlider.Value = currenttime;
            //    }
      
            }
            else if (A == 1)
            {
                await CrossMediaManager.Current.Pause();
                A--;
                SongSlider.Value = 0; 

            }
        }

        public async void RewindSong(object Sender, EventArgs args)
        {
            if (B != 0)
            {
                B--;

                CurrentSong = CurrentPlaylist[B].ToString();
                Album.Source = songService.ImageList[B].ToString();
                await CrossMediaManager.Current.Play(CurrentPlaylist[B]);
            }
        }


        public async void ForwardSong(object Sender, EventArgs args)
        {
            if (B < 8)
            {
                B++;
                CurrentSong = CurrentPlaylist[B].ToString();
                Album.Source = songService.ImageList[B].ToString(); 
                await CrossMediaManager.Current.Play(CurrentPlaylist[B]);
            }
        }

    }



}
