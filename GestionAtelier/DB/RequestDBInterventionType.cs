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
    class RequestDBInterventionType : FactoryDB<InterventionType>
    {
        /// <summary>
        /// Converty the SqlDataReader to interventiontype
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override InterventionType ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int InterventionTypeId = 0;
            string Name = "";
            InterventionType interventionType = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["InterventionTypeId"].ToString()))
                    InterventionTypeId = Convert.ToInt32(dr["InterventionTypeId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    Name = Convert.ToString(dr["Name"]);

                interventionType = new InterventionType(InterventionTypeId, Name);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return interventionType;
        }

        /// <summary>
        /// Return all intervention type
        /// </summary>
        /// <returns></returns>
        public override List<InterventionType> SelectAllElement()
        {
            string query = "SELECT * FROM InterventionType;";
            List<InterventionType> allInterventionType = new List<InterventionType>();

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
                            allInterventionType.Add(ConvertSqlDataReaderToClass(reader));
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

            return allInterventionType;
        }

        /// <summary>
        /// Return interventionType by id
        /// </summary>
        /// <param name="InterventionTypeId"></param>
        /// <returns></returns>
        public override InterventionType SelectElementById(int InterventionTypeId)
        {
            string query = "SELECT * FROM InterventionType WHERE InterventionTypeId = @InterventionTypeId;";
            InterventionType InterventionType = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InterventionTypeId", InterventionTypeId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            InterventionType = ConvertSqlDataReaderToClass(reader);
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

            return InterventionType;
        }
    }
}
