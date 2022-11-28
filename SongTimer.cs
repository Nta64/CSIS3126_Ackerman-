using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Timers;


namespace Audio
{
    internal class SongTimer : INotifyPropertyChanged
    {
        public Stopwatch stopWatch = new Stopwatch();

        private System.Timers.Timer time = new System.Timers.Timer();

        private bool timerRunning;

        private string _stopWatchMinutes;

        public string StopWatchMinutes
        {
        get {return _stopWatchMinutes;}
         set { _stopWatchMinutes = value;
                OnPropertyChanged("StopWatchMinutes"); 
            }
        }

        private string _stopWatchSeconds;

        public string StopWatchSeconds
        {
            get { return _stopWatchSeconds; }
            set
            {
                _stopWatchMinutes = value;
                OnPropertyChanged("StopWatchMinutes");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            var changed = PropertyChanged;
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
