using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Filmuthyrning.Model.BLL;


namespace Filmuthyrning.Model.DAL
{
    public class RentalDAL : DALBase
    {
        //Hämtar alla uthyrningar från tabellen.
        public IEnumerable<Rental> getRentals()
        {
            try
            {
                //här ska alla uthyrningar från databasen sparas
                List<Rental> rentals = new List<Rental>(1000);

                //en anslutning till databasen hämtas.
                using (SqlConnection conn = CreateConnection())
                {
                    //den lagrade proceduren som ska användas
                    SqlCommand getRentals = new SqlCommand("appSchema.getUthyrningar", conn);
                    getRentals.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = new SqlDataReader())
                    {

                        //hämta alla index i tabeller
                        int rentalIDIndex = reader.GetOrdinal("UthyrningID");
                        int movieIDIndex = reader.GetOrdinal("FilmID");
                        int customerIDIndex = reader.GetOrdinal("KundID");
                        int rentalDateindex = reader.GetOrdinal("HyrDatum");

                        //hämtar varje tabellrad för sig
                        while (reader.Read())
                        {
                            //hämtar och lägger till uthyrningen i return-listan.
                            Rental rental = new Rental();
                            rental.RentalID = reader.GetInt32(rentalIDIndex);
                            rental.MovieID = reader.GetInt32(movieIDIndex);
                            rental.CustomerID = reader.GetInt32(customerIDIndex);
                            rental.RentalDate = reader.GetString(rentalDateindex);


                            rentals.Add(rental);
                        }
                    }
                }
                //tar bort ev. tomma poster från listan
                rentals.TrimExcess();
                return rentals.AsEnumerable();
            }

            catch
            {
                throw new ApplicationException("An error occurred when accessing the database.");
            }
        }

        //Hämta en uthyrning med specificerat ID
        public Rental getRentalsByID(int rentalID)
        {
            try
            {
                Rental rental = new Rental();

                //Anslutningen som ska användas
                using (SqlConnection conn = CreateConnection())
                {
                    //den lagrade proceduren som ska användas
                    SqlCommand getRentalByID = new SqlCommand("appSchema.getUthyrningByID", conn);
                    getRentalByID.CommandType = CommandType.StoredProcedure;

                    getRentalByID.Parameters.Add("@UthyrningID", SqlDbType.Int, 4).Value = rentalID;

                    //anslutningen öppnas
                    conn.Open();

                    using (SqlDataReader reader = getRentalByID.ExecuteReader())
                    {
                        //hämta alla index i tabeller
                        int rentalIDIndex = reader.GetOrdinal("UthyrningID");
                        int movieIDIndex = reader.GetOrdinal("FilmID");
                        int customerIDIndex = reader.GetOrdinal("KundID");
                        int rentalDateindex = reader.GetOrdinal("HyrDatum");

                        if (reader.Read())
                        {
                            rental.RentalID = reader.GetInt32(rentalIDIndex);
                            rental.MovieID = reader.GetInt32(movieIDIndex);
                            rental.CustomerID = reader.GetInt32(customerIDIndex);
                            rental.RentalDate = reader.GetString(rentalDateindex);

                            
                        }
                    }
                }
                return rental;
            }
            catch
            {
                throw new ApplicationException("Något gick fel när filmen skulle hämtas");
            }
        }
    }
}