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
    public class TVShow : BaseViewModel
    {
        //public event PropertyChangedEventHandler? PropertyChanged;

        public TVShow()
        {
            // toggle between shwoing tv show seasons
            this.TVSeasonsToggleCommand = new RelayCommand(() => { this.IsTvShowSeasonsViewable = !this.IsTvShowSeasonsViewable; });
        }

        private string _Title = string.Empty;
        public string Title
        {
            get => this._Title;
            set
            {
                this._Title = value;
                this.RaiseChangedEvent(nameof(this.Title));
            }
        }


        #region commands
        /// <summary>
        /// When want to toggle between showing tv show seasons to this tv series
        /// </summary>
        public ICommand TVSeasonsToggleCommand { get; set; }
        #endregion


        private bool _IsTvShowSeasonsViewable = false;
        /// <summary>
        /// When set to true all seasons for tv show should be shown
        /// </summary>
        public bool IsTvShowSeasonsViewable
        {
            get => _IsTvShowSeasonsViewable;
            set
            {
                this._IsTvShowSeasonsViewable = value;
                this.RaiseChangedEvent(nameof(this.IsTvShowSeasonsViewable));
            }
        }

        private bool _IsThereALastWatchedEpisode = false;
        /// <summary>
        /// Lets you know if there is a last watched episode. Gets set by the <see cref="LastWatchedEpisodeInShow"/> property
        /// </summary>
        public bool IsThereALastWatchedEpisode
        {
            get => this._IsThereALastWatchedEpisode;
            set
            {
                this._IsThereALastWatchedEpisode = value;
                this.RaiseChangedEvent(nameof(this.IsThereALastWatchedEpisode));
            }
        }

        private Episode _LastWatchedEpisodeInShow = null;
        public Episode LastWatchedEpisodeInShow 
        {
            get => this._LastWatchedEpisodeInShow;
            set
            {
                this._LastWatchedEpisodeInShow= value;
                this.RaiseChangedEvent(nameof(this.LastWatchedEpisodeInShow));

                // if the value we are setting is null, we don't have a last watched eppisode
                if (this.LastWatchedEpisodeInShow == null)
                    this.IsThereALastWatchedEpisode = false;
                // we do have a last watched eppisode
                else
                    this.IsThereALastWatchedEpisode = true;
            }
        }

        /// <summary>
        /// gets a string friendly name of the last watched episode ("none" if there is not one)
        /// </summary>
        public string LastWatchedEpisodeName
        {
            get
            {
                if (this.IsThereALastWatchedEpisode)
                {
                    return "s" + this.LastWatchedEpisodeInShow.SeasonNumber.ToString("00") + "e" + this.LastWatchedEpisodeInShow.EpisodeNumber.ToString("00");
                }
                else
                    return "none";
            }
        }

        /// <summary>
        /// searchs through the tv episodes to look for the last epp that was watched and then sets
        /// <see cref="LastWatchedEpisodeInShow"/> to that value.
        /// </summary>
        public void FindLastWatchedEpisode()
        {
            for(int SeriesIndex = this.Seasons.Count - 1; SeriesIndex >= 0; SeriesIndex--)
            {
                TvSeason aSeason = this.Seasons[SeriesIndex];
                
                for(int EpisodeIndex = aSeason.Episodes.Count - 1; EpisodeIndex >= 0; EpisodeIndex--)
                {
                    if(aSeason.Episodes[EpisodeIndex].HasBeenWatched == true)
                    {
                        this.LastWatchedEpisodeInShow = aSeason.Episodes[EpisodeIndex];
                        return;
                    }
                }
            }
        }

        public ObservableCollection<TvSeason> Seasons { get; set; } = new ObservableCollection<TvSeason>();

        

        public void RaiseChangedEvent(string PropertyName)
        {
            base.OnPropertyChanged(PropertyName);
        }


    }
}
