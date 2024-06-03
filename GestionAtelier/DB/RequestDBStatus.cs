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
    class RequestDBStatus : FactoryDB<Status>
    {
        /// <summary>
        /// Converty the SqlDataReader to Status
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Status ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int StatusId = 0;
            string Name = "";

            Status status = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["StatusId"].ToString()))
                    StatusId = Convert.ToInt32(dr["StatusId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    Name = Convert.ToString(dr["Name"]);

                if (ColumnExists(dr, "BroadcastTextID"))
                    if (localeLanguage != defaultLanguage)
                        if (Convert.ToInt32(dr["BroadcastTextID"]) > 0)
                            Name = GetLocalesStatusName(Convert.ToInt32(dr["BroadcastTextID"]), localeLanguage);

                status = new Status(StatusId, Name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return status;
        }

        /// <summary>
        /// Return all status in database
        /// </summary>
        /// <returns></returns>
        public override List<Status> SelectAllElement()
        {
            string query = "SELECT * FROM Status;";
            List<Status> allStatus = new List<Status>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allStatus.Add(ConvertSqlDataReaderToClass(reader));
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

            return allStatus;
        }

        /// <summary>
        /// Return status by id
        /// </summary>
        /// <param name="StatusId"></param>
        /// <returns></returns>
        public override Status SelectElementById(int StatusId)
        {
            string query = "SELECT * FROM Status WHERE StatusId = @StatusId;";
            Status status = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@StatusId", StatusId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            status = ConvertSqlDataReaderToClass(reader);
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

            return status;
        }

        /// <summary>
        /// Return id from the close status (for function CloseIntervention())
        /// </summary>
        /// <returns></returns>
        public int SearchStatusIdByName(string name)
        {
            string query = "SELECT StatusId FROM Status WHERE Name Like @name;";

            int statusId = 0;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@name", name);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            statusId = Convert.ToInt32(reader["StatusId"]);
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

            return statusId;
        }

        /// <summary>
        /// Return status by name parameter
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Status GetStatusByName(string Name)
        {
            Status status = SelectElementById(SearchStatusIdByName(Name));
            CloseConnection();
            return status;
        }

        /// <summary>
        /// Return the locale name for the status
        /// </summary>
        /// <param name="BroadcastTextID"></param>
        /// <param name="localeLanguage"></param>
        /// <returns></returns>
        private string GetLocalesStatusName(int BroadcastTextID, string localeLanguage)
        {
            string columnName = GetColumnNameLocaleLanguage(localeLanguage);
            string query = "SELECT " + columnName + " FROM broadcast_text WHERE BroadcastTextID = @BroadcastTextID;";
            string localeStatus = "";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@BroadcastTextID", BroadcastTextID);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            localeStatus = reader[columnName].ToString();
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
            return localeStatus;
        }
    }
}
