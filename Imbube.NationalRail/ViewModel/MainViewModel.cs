using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using GalaSoft.MvvmLight;
using Imbube.NationalRail.Domain.Models;
using Imbube.NationalRail.Domain.ViewModels;

namespace Imbube.NationalRail.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private const string UserSettings = "userSettings";

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                DesignerLoadSavedRoutes();
                DesignerLoadStations();
            }
            else
            {
                // Code runs "for real"
            }
        }

        public void LoadUserData()
        {
            using (var filesystem = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (filesystem.FileExists(UserSettings))
                {
                    using (var fs = new IsolatedStorageFileStream(UserSettings, FileMode.Open, filesystem))
                    {
                        //this.Picture = PictureDecoder.DecodeJpeg(fs);
                    }
                }
            }
        }


        private void DesignerLoadStations()
        {
            RailStations = new ObservableCollection<Station>(){new Station(){Name = "Stoneleigh", Code = "SNL"}, new Station(){Name = "Waterloo", Code = "WAT"}};
        }

        private void DesignerLoadSavedRoutes()
        {
            SavedRoutes = new ObservableCollection<Route>(){new Route(){RouteName = "Ruaan To Work Route", ToStation = new Station(){Name = "Stoneleigh", Code = "SNL"}}};
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Station> RailStations { get; private set; }

        public ObservableCollection<Route> SavedRoutes { get; private set; }

    }
}