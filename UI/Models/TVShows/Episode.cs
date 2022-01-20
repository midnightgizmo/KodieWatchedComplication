using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models.TVShows
{
    public class Episode : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _HasBeenWatched = false;
        public bool HasBeenWatched
        {
            get => this._HasBeenWatched;
            set
            {
                this._HasBeenWatched = value;
                this.RaiseChangedEvent(nameof(this.HasBeenWatched));
            }
        }

        private int _EpisodeNumber;

        public int EpisodeNumber
        {
            get => _EpisodeNumber;
            set 
            { 
                this._EpisodeNumber = value;
                this.RaiseChangedEvent(nameof(this.EpisodeNumber));
            }
        }

        private int _SeasonNumber;

        public int SeasonNumber
        {
            get => _SeasonNumber; 
            set 
            {
                this._SeasonNumber = value;
                this.RaiseChangedEvent(nameof(this.SeasonNumber));
            }
        }




        public void RaiseChangedEvent(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }


    }
}
