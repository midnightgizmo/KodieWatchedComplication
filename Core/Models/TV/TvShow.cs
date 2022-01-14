using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.TV
{
    public class TvShow
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public List<TvShowEpisode> Episodes { get; set; } = new List<TvShowEpisode>();

    }
}
