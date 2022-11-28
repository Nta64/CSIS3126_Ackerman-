
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Audio.FlyoutMenuItemCS;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Audio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeFlyout : ContentPage
    {
        public ListView ListView;

       

        public HomeFlyout()
        {
            InitializeComponent();

            BindingContext = new HomeFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class HomeFlyoutViewModel : INotifyPropertyChanged
        { 
            public ObservableCollection<HomeFlyoutMenuItem> MenuItems { get; set; }

            public HomeFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<HomeFlyoutMenuItem>(new[]
                {
                    new HomeFlyoutMenuItem { Id = 0, Title = "Browse", TargetType = typeof(Browse)},
                    new HomeFlyoutMenuItem { Id = 1, Title = "Your Playlists", TargetType = typeof(Playlists) },
                    new HomeFlyoutMenuItem { Id = 2, Title = "Settings", TargetType = typeof(Settings)},
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}