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
    class RequestDBParmSite : FactoryDB<ParmSite>
    {
        /// <summary>
        /// Converty the SqlDataReader to ParmSite
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override ParmSite ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            string SiteId = "";
            string Name = "";
            int Status = 0;
            string CompanyID = "";
            ParmSite parmSite = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["SiteId"].ToString()))
                    SiteId = Convert.ToString(dr["SiteId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    Name = Convert.ToString(dr["Name"]);
                if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                    Status = Convert.ToInt32(dr["Status"]);
                if (!string.IsNullOrEmpty(dr["CompanyID"].ToString()))
                    CompanyID = Convert.ToString(dr["CompanyID"]);
                
                parmSite = new ParmSite(SiteId, Name, Status, CompanyID);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return parmSite;
        }

        /// <summary>
        /// Return all garage
        /// </summary>
        /// <returns></returns>
        public override List<ParmSite> SelectAllElement()
        {
            string query = "SELECT * FROM ParmSite;";
            List<ParmSite> allParmSite = new List<ParmSite>();

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
                            allParmSite.Add(ConvertSqlDataReaderToClass(reader));
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

            return allParmSite;
        }

        /// <summary>
        /// Return ParmSite by Id : type int (not used)
        /// </summary>
        /// <param name="ParmSiteId"></param>
        /// <returns></returns>
        public override ParmSite SelectElementById(int ParmSiteId)
        {
            return null;
        }

        /// <summary>
        /// Return all ParmSite by User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<ParmSite> GetAllParmSiteByEmployee(string EmployeeId)
        {
            string query = "SELECT DISTINCT ps.* FROM ParmSite ps JOIN UserProfile up ON up.SiteId = ps.SiteId JOIN RightUserProfile rup ON up.UserProfileId = rup.UserProfileId JOIN [User] us ON us.UserId = rup.UserId WHERE us.EmployeeId = @EmployeeId;";
            List<ParmSite> allParmSite = new List<ParmSite>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allParmSite.Add(ConvertSqlDataReaderToClass(reader));
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

            return allParmSite;
        }

        /// <summary>
        /// Return ParmSiteById
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        public ParmSite GetParmSiteById(string SiteId)
        {
            string query = "SELECT * FROM ParmSite WHERE SiteId = @SiteId;";
            ParmSite ParmSite = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@SiteId", SiteId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ParmSite = ConvertSqlDataReaderToClass(reader);
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

            return ParmSite;
        }
    }
}
