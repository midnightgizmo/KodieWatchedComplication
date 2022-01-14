using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.TV
{
    public class TvShowEpisode
    {

        /// <summary>
        /// Called "strFileName" in database. e.g. "To the end s01e04.mp4"
        /// </summary>
        public string FileName { get; set; } = string.Empty;


        /// <summary>
        /// Called "playCount" in the database.
        /// Number of times the episode has been watched. If zero, the episode has not been watched
        /// </summary>
        public int WatchedCount { get; set; } = 0;


        /// <summary>
        /// The season number this eppisode is from (c12 in database)
        /// </summary>
        public int SeasonNumber { get; set; } = -1;
        /// <summary>
        /// The eppisode number within the season (c13 in database)
        /// </summary>
        public int SeasonEppNumber { get; set; } = -1;
    }
}
