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
    class RequestDBRecentStatement : FactoryDB<RecentStatement>
    {
        /// <summary>
        /// Converty the SqlDataReader to RecentStatement
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override RecentStatement ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            string VehicleId = "";
            int Hour = 0;
            int Kilometer = 0;
            RecentStatement recentStatement = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["VehicleId"].ToString()))
                    VehicleId = Convert.ToString(dr["VehicleId"]);
                if (!string.IsNullOrEmpty(dr["Hour"].ToString()))
                    Hour = Convert.ToInt32(dr["Hour"]);
                if (!string.IsNullOrEmpty(dr["Kilometer"].ToString()))
                    Kilometer = Convert.ToInt32(dr["Kilometer"]);

                recentStatement = new RecentStatement(VehicleId, Hour, Kilometer);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return recentStatement;
        }

        /// <summary>
        /// Return all RecentStatement (not used)
        /// </summary>
        /// <returns></returns>
        public override List<RecentStatement> SelectAllElement()
        {
            return null;
        }

        /// <summary>
        /// Return recentstatement by id (not used)
        /// </summary>
        /// <param name="RecentStatementId"></param>
        /// <returns></returns>
        public override RecentStatement SelectElementById(int RecentStatementId)
        {
            return null;
        }

        /// <summary>
        /// Return the recent statement for the intervention on the vehicle
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public RecentStatement GetRecentStatement(Intervention intervention)
        {
            string query = "SELECT * FROM RecentStatement WHERE VehicleId = @VehicleId;";
            RecentStatement recentStatment = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", intervention.vehicleId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            recentStatment = ConvertSqlDataReaderToClass(reader);
                        }
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
            }

            return recentStatment;
        }

        /// <summary>
        /// Check if authorized the new recent statement for the vehicle
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private bool CheckIfAuthorizedNewRecentStatement(RecentStatement statement)
        {
            string query = "SELECT VehicleId, Hour, Kilometer FROM Vehicle WHERE VehicleId = @VehicleId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", statement.VehicleId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToString(reader["VehicleId"]) == statement.VehicleId)
                            {
                                if (Convert.ToInt32(reader["Kilometer"]) < statement.Kilometer)
                                    return true;
                                if (Convert.ToUInt32(reader["Hour"]) < statement.Hour)
                                    return true;
                            }
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
            return false;
        }

        /// <summary>
        /// Insert the new recent statement for the vehicle
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        public bool InsertNewRecentStatement(RecentStatement statement)
        {
            string query = "INSERT INTO RecentStatement (VehicleId, Hour, Kilometer) VALUES"
                + "(@VehicleId, @Hour, @Kilometer);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", statement.VehicleId);
                cmd.Parameters.AddWithValue("@Hour", statement.Hour);
                cmd.Parameters.AddWithValue("@Kilometer", statement.Kilometer);

                if (!CheckIfAuthorizedNewRecentStatement(statement))
                    return false;

                try
                {
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    return true;
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }
            }

            return false;
        }
    }
}
