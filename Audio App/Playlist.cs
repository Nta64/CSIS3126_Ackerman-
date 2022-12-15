using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AudioApplication
{
    public class Playlist
    {
        public Playlist(){ }
        public ObservableCollection<Playlist> Playlists { get { return Playlists; } }
        
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
