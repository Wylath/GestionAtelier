using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.DB
{
    class RequestDBFuel : FactoryDB<Fuel>
    {
        /// <summary>
        /// Converty the SqlDataReader to fuel
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Fuel ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int RECID = 0;
            string VehicleId = "";
            int Kilometer = 0;
            int Hour = 0;
            DateTime DatePrise = DateTime.Now;
            Fuel fuel = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["RECID"].ToString()))
                    RECID = Convert.ToInt32(dr["RECID"]);
                if (!string.IsNullOrEmpty(dr["VehicleId"].ToString()))
                    VehicleId = Convert.ToString(dr["VehicleId"]);
                if (!string.IsNullOrEmpty(dr["Kilometer"].ToString()))
                    Kilometer = Convert.ToInt32(dr["Kilometer"]);
                if (!string.IsNullOrEmpty(dr["Hour"].ToString()))
                    Hour = Convert.ToInt32(dr["Hour"]);
                if (!string.IsNullOrEmpty(dr["DatePrise"].ToString()))
                    DatePrise = Convert.ToDateTime(dr["DatePrise"]);

                fuel = new Fuel(RECID, VehicleId, Kilometer, Hour, DatePrise);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return fuel;
        }

        /// <summary>
        /// Return all Fuel (not used)
        /// </summary>
        /// <returns></returns>
        public override List<Fuel> SelectAllElement()
        {
            return null;
        }

        /// <summary>
        /// Return Fuel by Id : type int (not used)
        /// </summary>
        /// <param name="RECID"></param>
        /// <returns></returns>
        public override Fuel SelectElementById(int RECID)
        {
            return null;
        }

        /// <summary>
        /// Return last fuel by vehicleid
        /// </summary>
        /// <param name="ParmSiteId"></param>
        /// <returns></returns>
        public Fuel GetLastFuelByVehicleId(string VehicleId)
        {
            string query = "SELECT * FROM Fuel WHERE VehicleId = @VehicleId;";
            Fuel fuel = null;
            Fuel tempFuel = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tempFuel = ConvertSqlDataReaderToClass(reader);
                            if (fuel == null)
                                fuel = tempFuel;
                            else if (tempFuel.Hour > fuel.Hour || tempFuel.Kilometer > fuel.Kilometer)
                                fuel = tempFuel;
                        }
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }
            }

            return fuel;
        }
    }
}
