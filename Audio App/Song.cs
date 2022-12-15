using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AudioApplication
{
    public class Song
    { 
        public Song() { }
        public ObservableCollection<Song> Songs { get { return Songs; } }

        public int ID { get; set; } 
        public string  Title { get; set; }
    }
}
