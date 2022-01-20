using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModel;

namespace UI.Models.TVShows
{
    public  class TvSeason : BaseViewModel
    {
        //public event PropertyChangedEventHandler? PropertyChanged;

        public TvSeason()
        {
            // toggle the IsTvShowSeasonEpisodesViewable bool value betwen true and false
            this.TVSeasonEpisodesToggleCommand = new RelayCommand(() => { this.IsTvShowSeasonEpisodesViewable = !this.IsTvShowSeasonEpisodesViewable; });
        }


        #region commands
        /// <summary>
        /// When want to toggle between showing tv show episodes for this season
        /// </summary>
        public ICommand TVSeasonEpisodesToggleCommand { get; set; }
        #endregion


        private bool _IsTvShowSeasonEpisodesViewable = false;
        /// <summary>
        /// When set to true all seasons for tv show should be shown
        /// </summary>
        public bool IsTvShowSeasonEpisodesViewable
        {
            get => _IsTvShowSeasonEpisodesViewable;
            set
            {
                this._IsTvShowSeasonEpisodesViewable = value;
                this.RaiseChangedEvent(nameof(this.IsTvShowSeasonEpisodesViewable));
            }
        }



        private int _SeasonNumber;
        public int SeasonNumber
        {
            get => this._SeasonNumber;
            set
            {
                this._SeasonNumber = value;
                this.RaiseChangedEvent(nameof(this.SeasonNumber));

            }
        }

        private bool _HaveAllEpisodesInSeasonBeenWatched = false;
        public bool HaveAllEpisodesInSeasonBeenWatched
        {
            get => this._HaveAllEpisodesInSeasonBeenWatched;
            set
            {
                this._HaveAllEpisodesInSeasonBeenWatched |= value;
                this.RaiseChangedEvent(nameof(this.HaveAllEpisodesInSeasonBeenWatched));
            }
        }


        // Call this after all episodes have been added, or there has been a change (added, deleted)
        public void CheckIfAllEpisodesHaveBeenWatched()
        {
            bool haveAllBeenWatched = true;
            foreach(Episode episode in this.Episodes)
            {
                if(episode.HasBeenWatched == false)
                {
                    haveAllBeenWatched = false;
                    break;
                }
            }

            this.HaveAllEpisodesInSeasonBeenWatched = haveAllBeenWatched;
        }

        public ObservableCollection<Episode> Episodes { get; set; } = new ObservableCollection<Episode>();


        public void RaiseChangedEvent(string PropertyName)
        {
            base.OnPropertyChanged(PropertyName);
        }



       

    }
}
