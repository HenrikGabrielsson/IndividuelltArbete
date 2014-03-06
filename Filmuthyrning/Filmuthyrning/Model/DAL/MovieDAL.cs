using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Filmuthyrning.Model.BLL;


namespace Filmuthyrning.Model.DAL
{
    public class MovieDAL: DALBase
    {
        //Hämtar alla filmer från tabellen.
        public IEnumerable<Movie> getFilmer()
        {
            try
            {
                //här ska alla filmer från databasen sparas
                List<Movie> movies = new List<Movie>(500);

                //en anslutning till databasen hämtas.
                using (SqlConnection conn = CreateConnection())
                {
                    //den lagrade proceduren som ska användas
                    SqlCommand getFilmer = new SqlCommand("AppSchema.getFilmer", conn);
                    getFilmer.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = new SqlDataReader())
                    {

                        //hämta alla index i tabeller
                        int filmIDIndex = reader.GetOrdinal("FilmID");
                        int titelIndex = reader.GetOrdinal("Titel");
                        int årIndex = reader.GetOrdinal("År");
                        int genreIndex = reader.GetOrdinal("Genre");
                        int prisgruppIDIndex = reader.GetOrdinal("Prisgrupp");
                        int hyrtidIndex = reader.GetOrdinal("Hyrtid");
                        int antalIndex = reader.GetOrdinal("Antal");

                        //hämtar varje tabellrad för sig
                        while (reader.Read())
                        {
                            //hämtar och lägger till filmen i return-listan.
                            Movie movie = new Movie();
                            movie.FilmID = reader.GetInt32(filmIDIndex);
                            movie.Titel = reader.GetString(titelIndex);
                            movie.År = reader.GetInt32(årIndex);
                            movie.Genre = reader.GetString(genreIndex);
                            movie.PrisgruppID = reader.GetInt32(prisgruppIDIndex);
                            movie.Hyrtid = reader.GetInt32(hyrtidIndex);
                            movie.Antal = reader.GetInt32(antalIndex);

                            movies.Add(movie);
                        }
                    }
                }
                return movies.AsEnumerable();
            }

            catch
            {
                throw new ApplicationException("Ett fel uppstod när filmerna skulle hämtas");
            }
        }

        //Hämta en film med specificerat ID
        public Movie getFilmByID(int FilmID)
        {
            try
            {
                Movie movie = new Movie();

                //Anslutningen som ska användas
                using (SqlConnection conn = CreateConnection())
                {
                    //den lagrade proceduren som ska användas
                    SqlCommand getFilmByID = new SqlCommand("getFilmByID", conn);
                    getFilmByID.CommandType = CommandType.StoredProcedure;

                    getFilmByID.Parameters.Add("FilmID", SqlDbType.Int, 4).Value = FilmID;

                    //anslutningen öppnas
                    conn.Open();

                    using(SqlDataReader reader = getFilmByID.ExecuteReader())
                    {
                        //hämta alla index i tabeller
                        int filmIDIndex = reader.GetOrdinal("FilmID");
                        int titelIndex = reader.GetOrdinal("Titel");
                        int årIndex = reader.GetOrdinal("År");
                        int genreIndex = reader.GetOrdinal("Genre");
                        int prisgruppIDIndex = reader.GetOrdinal("Prisgrupp");
                        int hyrtidIndex = reader.GetOrdinal("Hyrtid");
                        int antalIndex = reader.GetOrdinal("Antal");

                        if(reader.Read())
                        {
                            movie.FilmID = reader.GetInt32(filmIDIndex);
                            movie.Titel = reader.GetString(titelIndex);
                            movie.År = reader.GetInt32(årIndex);
                            movie.Genre = reader.GetString(genreIndex);
                            movie.PrisgruppID = reader.GetInt32(prisgruppIDIndex);
                            movie.Hyrtid = reader.GetInt32(hyrtidIndex);
                            movie.Antal = reader.GetInt32(antalIndex);

                            return movie;
                        }
                    }

                }

                return movie;
            }
            catch
            {
                throw new ApplicationException("Något gick fel när filmen skulle hämtas");
            }
        }


    }
}