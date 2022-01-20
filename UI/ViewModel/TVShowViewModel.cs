using Core.Models.TV;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Models.TVShows;

namespace UI.ViewModel
{
    public class TVShowViewModel : BaseViewModel
    {

        public TVShowViewModel()
        {
            


            
        }

        public void LoadDataFromDatabase()
        {
            List<TvShow> dbtvShows;
            dbtvShows = GetAllTVShows();
            ConvertDatabaseModelsToUIModels(dbtvShows);

        }

        public void LoadFromJsonData(string data)
        {
            List<TvShow> dbtvShows = JsonSerializer.Deserialize<List<TvShow>>(data);
            ConvertDatabaseModelsToUIModels(dbtvShows);
        }

        public string SaveToJsonString()
        {
            return JsonSerializer.Serialize<List<TvShow>>(this.GetAllTVShows());
        }







        #region public properties

        public ObservableCollection<TVShow> TVShows { get; set; } = new ObservableCollection<TVShow>();


        #endregion





        #region Private methods

        /// <summary>
        /// Get all tv shows and there episodes from database
        /// </summary>
        /// <returns>TV Shows and there episodes</returns>
        private List<TvShow> GetAllTVShows()
        {
            Core.Database.TV.MySqlTvShows dbTvShows = new Core.Database.TV.MySqlTvShows();
            Core.Database.TV.MySqlTvShowEppisodes dbEpisodes = new Core.Database.TV.MySqlTvShowEppisodes();

            List<TvShow> tvShows = dbTvShows.GetAllTVShows();

            foreach (TvShow tvShow in tvShows)
            {
                tvShow.Episodes = dbEpisodes.SelectAllTvEpisodes(tvShow.ID);
                tvShow.Episodes.Sort((a, b) => a.SeasonEppNumber.CompareTo(b.SeasonEppNumber)) ;
            }

            return tvShows;
        }


        /// <summary>
        /// Convert the database tv show models to UI Tv show models which are stored in <see cref="TVShows"/>
        /// </summary>
        /// <param name="dbtvShows">database tv show models</param>
        private void ConvertDatabaseModelsToUIModels(List<TvShow> dbtvShows)
        {
            
            foreach (TvShow dbTVShow in dbtvShows)
            {
                TVShow aTVShow = new TVShow();

                aTVShow.Title = dbTVShow.Title;
                
                // get the seasons that are in this tv show
                List<int> Seasons = new (dbTVShow.Episodes.DistinctBy(d => d.SeasonNumber).Select(s => s.SeasonNumber));
                Seasons.Sort();
                // create the seasons
                foreach(int aSeason in Seasons)
                {
                    aTVShow.Seasons.Add(new TvSeason { SeasonNumber = aSeason });
                    
                }

                // add the eppisodes to each season
                foreach (var dbEp in dbTVShow.Episodes)
                {
                    foreach(TvSeason aSeason in aTVShow.Seasons)
                    {
                        if(dbEp.SeasonNumber == aSeason.SeasonNumber)
                        {
                            aSeason.Episodes.Add(new Episode
                            {
                                EpisodeNumber = dbEp.SeasonEppNumber,
                                HasBeenWatched = dbEp.WatchedCount > 0 ? true : false,
                                SeasonNumber = aSeason.SeasonNumber
                            });
                            break;
                        }
                    }
                }

                foreach (TvSeason aSeason in aTVShow.Seasons)
                {
                    aSeason.CheckIfAllEpisodesHaveBeenWatched();
                }

                aTVShow.FindLastWatchedEpisode();
                TVShows.Add(aTVShow);
                
            }
        }
        #endregion



    }
}
