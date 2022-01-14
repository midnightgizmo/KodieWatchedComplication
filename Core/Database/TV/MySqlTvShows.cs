using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Database;
using Core.Data;
using Core.Models.TV;
using MySqlConnector;

namespace Core.Database.TV
{
    public class MySqlTvShows
    {
        public readonly string dbTableName = "tvshow_view";
        public List<TvShow> GetAllTVShows()
        {
            List<TvShow> tvShows = new List<TvShow>();
            dbMySqlConnection con = new dbMySqlConnection();
            bool wasLoginSucsefull = false;
            StringBuilder sb = new StringBuilder();
            MySqlDataReader rdr;

            wasLoginSucsefull = con.OpenConnection();

            // if we can't login to the database, return an empty list
            if (wasLoginSucsefull == false)
                return tvShows;

            sb.Append("SELECT idShow, c00 as ShowName FROM ");
            sb.Append(this.dbTableName + ";");

            rdr = con.ExecuteSelectCommand(sb.ToString());

            while(rdr.Read())
            {
                TvShow tvShow = new TvShow();
                tvShow.ID = rdr.GetInt32("idShow");
                tvShow.Title = rdr.GetString("ShowName");

                tvShows.Add(tvShow);
            }

            rdr.Close();
            con.CloseConnection();

            return tvShows;


        }
    }
}
