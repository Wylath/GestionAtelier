using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using GestionAtelier.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.DB
{
    class RequestDBInterventionTire : FactoryDB<InterventionTire>
    {
        RequestDBIntervention RI = new RequestDBIntervention();
        RequestDBTires RT = new RequestDBTires();

        /// <summary>
        /// Converty the SqlDataReader to InterventionTire
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override InterventionTire ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int InterventionTireId = 0;
            int TireNumber = 0;
            int ChangeCause = 0;
            int InterventionId = 0;
            Tires NewTireBarcodeId = null;
            InterventionTire interventionTire = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["InterventionTireId"].ToString()))
                    InterventionTireId = Convert.ToInt32(dr["InterventionTireId"]);
                if (!string.IsNullOrEmpty(dr["TireNumber"].ToString()))
                    TireNumber = Convert.ToInt32(dr["TireNumber"]);
                if (!string.IsNullOrEmpty(dr["ChangeCause"].ToString()))
                    ChangeCause = Convert.ToInt32(dr["ChangeCause"]);
                if (!string.IsNullOrEmpty(dr["InterventionId"].ToString()))
                    InterventionId = Convert.ToInt32(dr["InterventionId"]);
                if (!string.IsNullOrEmpty(dr["NewTireBarcodeId"].ToString()))
                    NewTireBarcodeId = RT.SelectElementById(Convert.ToInt32(dr["NewTireBarcodeId"]));

                interventionTire = new InterventionTire(InterventionTireId, TireNumber, ChangeCause, InterventionId, NewTireBarcodeId);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return interventionTire;
        }

        /// <summary>
        /// Return all interventionTire (not used)
        /// </summary>
        /// <returns></returns>
        public override List<InterventionTire> SelectAllElement()
        {
            return null;
        }

        /// <summary>
        /// Return InterventionTire by id (not used)
        /// </summary>
        /// <param name="InterventionTireId"></param>
        /// <returns></returns>
        public override InterventionTire SelectElementById(int InterventionTireId)
        {
            return null;
        }

        /// <summary>
        /// Return all intervention tire by intervention with InterventionId as parameter
        /// </summary>
        /// <param name="InterventionId"></param>
        /// <returns></returns>
        public List<InterventionTire> GetAllInterventionTireByIntervention(int InterventionId)
        {
            string query = "SELECT * FROM InterventionTire WHERE InterventionId = @InterventionId;";
            List<InterventionTire> allInterventionTire = new List<InterventionTire>();

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
                            allInterventionTire.Add(ConvertSqlDataReaderToClass(reader));
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

            return allInterventionTire;
        }

        /// <summary>
        /// Insert new intervention tire to database
        /// </summary>
        /// <param name="interTire"></param>
        /// <returns></returns>
        public bool InsertNewInterventionTire(InterventionTire interTire)
        {
            string query = "";
            if (interTire.ChangeCause != 4) // Permutation
                query = "INSERT INTO InterventionTire (TireNumber, ChangeCause, InterventionId, NewTireBarcodeId) VALUES"
                + "(@TireNumber, @ChangeCause, @InterventionId, @NewTireBarcodeId);";
            else
                query = "INSERT INTO InterventionTire (TireNumber, ChangeCause, InterventionId) VALUES"
                    + "(@TireNumber, @ChangeCause, @InterventionId);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@TireNumber", interTire.TireNumber);
                cmd.Parameters.AddWithValue("@ChangeCause", interTire.ChangeCause);
                cmd.Parameters.AddWithValue("@InterventionId", interTire.InterventionId);
                if (interTire.ChangeCause != 4) // Permutation
                    cmd.Parameters.AddWithValue("@NewTireBarcodeId", interTire.NewTireBarcodeId.RECID);

                try
                {
                    cmd.ExecuteNonQuery();

                    // Update the status in intervention if first interventionTire and nothing prestation
                    RI.ChangeStatusIntervention(interTire.InterventionId);

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

        /// <summary>
        /// Update intervention tire to database
        /// </summary>
        /// <param name="interTire"></param>
        /// <returns></returns>
        public bool UpdateInterventionTire(InterventionTire interTire)
        {
            string query = "";
            if (interTire.ChangeCause != 4) // Permutation
                query = "UPDATE InterventionTire SET TireNumber = @TireNumber, ChangeCause =  @ChangeCause, InterventionId = @InterventionId, NewTireBarcodeId = @NewTireBarcodeId, ModifiedDateTime = @ModifiedDateTime, ModifiedBy = @ModifiedBy WHERE InterventionTireId = @InterventionTireId";
            else
                query = "UPDATE InterventionTire SET TireNumber = @TireNumber, ChangeCause =  @ChangeCause, InterventionId = @InterventionId, ModifiedDateTime = @ModifiedDateTime, ModifiedBy = @ModifiedBy WHERE InterventionTireId = @InterventionTireId";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@TireNumber", interTire.TireNumber);
                cmd.Parameters.AddWithValue("@ChangeCause", interTire.ChangeCause);
                cmd.Parameters.AddWithValue("@InterventionId", interTire.InterventionId);
                if (interTire.ChangeCause != 4) // Permutation
                    cmd.Parameters.AddWithValue("@NewTireBarcodeId", interTire.NewTireBarcodeId.RECID);
                cmd.Parameters.AddWithValue("@InterventionTireId", interTire.InterventionTireId);
                cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
                if (!string.IsNullOrEmpty(CurrentUser.employeeId))
                    cmd.Parameters.AddWithValue("@ModifiedBy", CurrentUser.employeeId);

                try
                {
                    cmd.ExecuteNonQuery();

                    // Update the status in intervention if first interventionTire and nothing prestation
                    RI.ChangeStatusIntervention(interTire.InterventionId);
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
                return true;
            }

            return false;
        }
    }
}
