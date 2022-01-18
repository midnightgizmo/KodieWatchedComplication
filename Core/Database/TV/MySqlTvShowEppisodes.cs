using Core.Models.TV;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.TV
{
    public class MySqlTvShowEppisodes
    {
        public readonly string dbTableName = "episode_view";
        public List<TvShowEpisode> SelectAllTvEpisodes(int TvShowID)
        {
            List<TvShowEpisode> tvShowEppisodes = new List<TvShowEpisode>();
            dbMySqlConnection con = new dbMySqlConnection();
            bool wasLoginSucsefull = false;
            StringBuilder sb = new StringBuilder();
            MySqlDataReader rdr;

            wasLoginSucsefull = con.OpenConnection();

            // if we can't login to the database, return an empty list
            if (wasLoginSucsefull == false)
                return tvShowEppisodes;

            sb.Append("SELECT c12 as SeasonNumber, c13 as EpisodeNumber, strFileName as FileName, playCount FROM ");
            sb.Append(this.dbTableName + " ");
            sb.Append("WHERE idShow=?TvShowID ");
            sb.Append("ORDER BY SeasonNumber, EpisodeNumber");


            MySqlParameter param = new MySqlParameter("?TvShowID",MySqlDbType.Int32);
            param.Value = TvShowID;

            rdr = con.ExecuteParameterizedSelectCommand(sb.ToString(), new MySqlParameter[] { param });

            while (rdr.Read())
            {
                TvShowEpisode tvEpp = new TvShowEpisode();
                tvEpp.SeasonNumber = int.Parse(rdr.GetString("SeasonNumber"));
                tvEpp.SeasonEppNumber = int.Parse(rdr.GetString("EpisodeNumber"));
                tvEpp.FileName = rdr.GetString("FileName");

                tvShowEppisodes.Add(tvEpp);
            }

            rdr.Close();
            con.CloseConnection();

            return tvShowEppisodes;
        }
    }
}
