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
    class RequestDBPrestationType : FactoryDB<PrestationType>
    {
        /// <summary>
        /// Converty the SqlDataReader to prestation type
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override PrestationType ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int PrestationTypeId = 0;
            string Name = "";
            PrestationType prestationType = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["PrestationTypeId"].ToString()))
                    PrestationTypeId = Convert.ToInt32(dr["PrestationTypeId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    Name = Convert.ToString(dr["Name"]);

                prestationType = new PrestationType(PrestationTypeId, Name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return prestationType;
        }

        /// <summary>
        /// Return all prestation type
        /// </summary>
        /// <returns></returns>
        public override List<PrestationType> SelectAllElement()
        {
            string query = "SELECT * FROM PrestationType;";
            List<PrestationType> allPrestationType = new List<PrestationType>();

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
                            allPrestationType.Add(ConvertSqlDataReaderToClass(reader));
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

            return allPrestationType;
        }

        /// <summary>
        /// Return prestation type by prestation type id
        /// </summary>
        /// <param name="PrestationTypeId"></param>
        /// <returns></returns>
        public override PrestationType SelectElementById(int PrestationTypeId)
        {
            string query = "SELECT * FROM PrestationType WHERE PrestationTypeId = @PrestationTypeId;";
            PrestationType PrestationType = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@PrestationTypeId", PrestationTypeId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PrestationType = ConvertSqlDataReaderToClass(reader);
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

            return PrestationType;
        }
    }
}
