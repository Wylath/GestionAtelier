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
    class RequestDBTires : FactoryDB<Tires>
    {
        /// <summary>
        /// Converty the SqlDataReader to Tires
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Tires ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int RECID = 0;
            string CompanyId = "";
            string ItemId = "";
            string Dim1 = "";
            string Dim2 = "";
            string Dim3 = "";
            string BarCode = "";
            string Comment = "";
            string ItemType = "";

            Tires tires = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["RECID"].ToString()))
                    RECID = Convert.ToInt32(dr["RECID"]);
                if (!string.IsNullOrEmpty(dr["CompanyId"].ToString()))
                    CompanyId = Convert.ToString(dr["CompanyId"]);
                if (!string.IsNullOrEmpty(dr["ItemId"].ToString()))
                    ItemId = Convert.ToString(dr["ItemId"]);
                if (!string.IsNullOrEmpty(dr["Dim1"].ToString()))
                    Dim1 = Convert.ToString(dr["Dim1"]);
                if (!string.IsNullOrEmpty(dr["Dim2"].ToString()))
                    Dim2 = Convert.ToString(dr["Dim2"]);
                if (!string.IsNullOrEmpty(dr["Dim3"].ToString()))
                    Dim3 = Convert.ToString(dr["Dim3"]);
                if (!string.IsNullOrEmpty(dr["BarCode"].ToString()))
                    BarCode = Convert.ToString(dr["BarCode"]);
                if (!string.IsNullOrEmpty(dr["Comment"].ToString()))
                    Comment = Convert.ToString(dr["Comment"]);
                if (!string.IsNullOrEmpty(dr["ItemType"].ToString()))
                    ItemType = Convert.ToString(dr["ItemType"]);

                tires = new Tires(RECID, CompanyId, ItemId, Dim1, Dim2, Dim3, BarCode, Comment, ItemType);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return tires;
        }

        /// <summary>
        /// Return all Tires
        /// </summary>
        /// <returns></returns>
        public override List<Tires> SelectAllElement()
        {
            string query = "SELECT * FROM Tires;";
            List<Tires> allTires = new List<Tires>();

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
                            allTires.Add(ConvertSqlDataReaderToClass(reader));
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

            return allTires;
        }

        /// <summary>
        /// Return all Tires by companyID
        /// </summary>
        /// <returns></returns>
        public List<Tires> SelectAllTiresByCompanyId(string CompanyId)
        {
            string query = "SELECT * FROM Tires WHERE CompanyID = @CompanyId;";
            List<Tires> allTires = new List<Tires>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allTires.Add(ConvertSqlDataReaderToClass(reader));
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

            return allTires;
        }

        /// <summary>
        /// Return all tires by intervention with InterventionId as parameter
        /// </summary>
        /// <param name="InterventionId"></param>
        /// <returns></returns>
        public List<Tires> GetAllTiresByIntervention(int InterventionId)
        {
            string query = "SELECT ti.* FROM Tires ti JOIN InterventionTire iti ON iti.NewTireBarcodeId = ti.RECID WHERE iti.InterventionId = @InterventionId;";
            List<Tires> allTires = new List<Tires>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InterventionId", InterventionId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allTires.Add(ConvertSqlDataReaderToClass(reader));
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

            return allTires;
        }

        /// <summary>
        /// Return tire by id
        /// </summary>
        /// <param name="TiresId"></param>
        /// <returns></returns>
        public override Tires SelectElementById(int TiresId)
        {
            string query = "SELECT * FROM Tires WHERE RECID = @TiresId;";
            Tires tire = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@TiresId", TiresId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tire = ConvertSqlDataReaderToClass(reader);
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

            return tire;
        }
    }
}
