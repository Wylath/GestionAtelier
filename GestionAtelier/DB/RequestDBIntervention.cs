using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using GestionAtelier.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.DB
{
    class RequestDBIntervention : FactoryDB<Intervention>
    {
        RequestDBStatus RST = new RequestDBStatus();
        RequestDBInterventionType RIT = new RequestDBInterventionType();
        RequestDBEmployee RE = new RequestDBEmployee();
        RequestDBParmSite RPS = new RequestDBParmSite();

        /// <summary>
        /// Converty the SqlDataReader to Intervention
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Intervention ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            RequestDBVehicle RV = new RequestDBVehicle();
            Intervention intervention = null;
            int interventionId = 0;
            DateTime DateOut = DateTime.Now;
            DateTime DateEstimate = DateTime.Now;
            DateTime DateIn = DateTime.Now;
            string PieceCom = "";
            float TimeEstimate = 0.0f;
            Vehicle veh = null;
            InterventionType interType = null;
            Employee submitter = null;
            Employee applicant = null;
            Status status = null;
            DateTime StatusDate = DateTime.Now;
            ParmSite site = null;
            Priority priority = null;
            bool pieceOrder = false;

            try
            {
                interventionId = Convert.ToInt32(dr["InterventionId"]);
                if (!string.IsNullOrEmpty(dr["DateOut"].ToString()))
                    DateOut = Convert.ToDateTime(dr["DateOut"]);
                if (!string.IsNullOrEmpty(dr["DateEstimate"].ToString()))
                    DateEstimate = Convert.ToDateTime(dr["DateEstimate"]);
                if (!string.IsNullOrEmpty(dr["DateIn"].ToString()))
                    DateIn = Convert.ToDateTime(dr["DateIn"]);
                if (!string.IsNullOrEmpty(dr["PieceCom"].ToString()))
                    PieceCom = Convert.ToString(dr["PieceCom"]);
                if (!string.IsNullOrEmpty(dr["TimeEstimate"].ToString()))
                    float.TryParse(Convert.ToString(dr["TimeEstimate"]), out TimeEstimate);
                veh = RV.GetVehicleById(Convert.ToString(dr["VehicleId"]));
                interType = RIT.SelectElementById(Convert.ToInt32(dr["interventionTypeId"]));
                submitter = RE.GetEmployeeById(Convert.ToString(dr["Submitter"]));
                applicant = RE.GetEmployeeById(Convert.ToString(dr["Applicant"]));
                status = RST.SelectElementById(Convert.ToInt32(dr["StatusId"]));
                if (!string.IsNullOrEmpty(dr["StatusDate"].ToString()))
                    StatusDate = Convert.ToDateTime(dr["StatusDate"]);
                site = RPS.GetParmSiteById(Convert.ToString(dr["SiteId"]));
                priority = new Priority(Convert.ToInt32(dr["Priority"]));
                if (!string.IsNullOrEmpty(dr["PieceOrder"].ToString()))
                    pieceOrder = Convert.ToBoolean(dr["PieceOrder"]);

                intervention = new Intervention(interventionId, veh, interType, submitter, applicant, status, StatusDate, DateIn, DateOut, DateEstimate, pieceOrder, PieceCom, TimeEstimate, priority, site);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }
            return intervention;
        }

        /// <summary>
        /// Retrun all intervention (not used)
        /// </summary>
        /// <returns></returns>
        public override List<Intervention> SelectAllElement()
        {
            return null;
        }

        /// <summary>
        /// Return all interventions
        /// </summary>
        /// <returns></returns>
        public List<Intervention> GetAllInterventions(bool close = false, string SiteId = "", int barcode = 0)
        {
            int StatusId = RST.SearchStatusIdByName("Close");
            string querySiteId = (!string.IsNullOrEmpty(SiteId)) ? " AND SiteId = @SiteId " : "";
            string queryBarcode = (barcode > 0) ? " AND Barcode = @Barcode " : "";
            string query;
            if (close)
            {
                query = "SELECT * FROM Intervention WHERE StatusId = @StatusId" + querySiteId + queryBarcode + ";";
            }
            else query = "SELECT * FROM Intervention WHERE StatusId != @StatusId" + querySiteId + queryBarcode + ";";

            List<Intervention> allIntervention = new List<Intervention>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@StatusId", StatusId);
                if (!string.IsNullOrEmpty(querySiteId))
                    cmd.Parameters.AddWithValue("@SiteId", SiteId);
                if (barcode > 0)
                    cmd.Parameters.AddWithValue("@Barcode", barcode);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allIntervention.Add(ConvertSqlDataReaderToClass(reader));
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

            return allIntervention;
        }

        /// <summary>
        /// Get all interventions on vehicle with vehicleId as parameter
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public List<Intervention> GetAllInterventionsOnVehicle(string VehicleId, string SiteId = "")
        {
            string querySiteId = (!string.IsNullOrEmpty(SiteId)) ? " AND SiteId = @SiteId " : "";

            string query = "SELECT * FROM Intervention WHERE VehicleId = @VehicleId" + querySiteId + ";";

            List<Intervention> allIntervention = new List<Intervention>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
                if (!string.IsNullOrEmpty(querySiteId))
                    cmd.Parameters.AddWithValue("@SiteId", SiteId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allIntervention.Add(ConvertSqlDataReaderToClass(reader));
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

            return allIntervention;
        }

        /// <summary>
        /// Return Intervention by Id
        /// </summary>
        /// <param name="InterventionId"></param>
        /// <returns></returns>
        public override Intervention SelectElementById(int InterventionId)
        {
            string query = "SELECT * FROM Intervention WHERE InterventionId = @InterventionId;";
            Intervention Intervention = null;

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
                            Intervention = ConvertSqlDataReaderToClass(reader);
                        }
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                //CloseConnection();
            }

            return Intervention;
        }

        /// <summary>
        /// Insert the new intervention to database
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public bool InsertNewIntervention(Intervention intervention)
        {
            string query = "INSERT INTO Intervention (VehicleId, DateIn, DateOut, DateEstimate, Applicant, Submitter, InterventionTypeId, StatusId, StatusDate, PieceOrder, PieceCom, TimeEstimate, Priority, SiteId) VALUES"
                + "(@VehicleId, @DateIn, @DateOut, @DateEstimate, @Applicant, @Submitter, @InterventionTypeId, @StatusId, @StatusDate, @PieceOrder, @PieceCom, @TimeEstimate, @Priority, @SiteId);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@VehicleId", intervention.vehicleId.VehicleId);
                cmd.Parameters.AddWithValue("@DateIn", intervention.DateIn);
                cmd.Parameters.AddWithValue("@DateOut", intervention.DateOut);
                cmd.Parameters.AddWithValue("@DateEstimate", intervention.DateEstimate);
                cmd.Parameters.AddWithValue("@Applicant", intervention.Applicant.EmployeeId);
                cmd.Parameters.AddWithValue("@Submitter", intervention.Submitter.EmployeeId);
                cmd.Parameters.AddWithValue("@InterventionTypeId", intervention.interventionTypeId.InterventionTypeId);
                cmd.Parameters.AddWithValue("@StatusId", intervention.StatusId.StatusId);
                cmd.Parameters.AddWithValue("@StatusDate", intervention.StatusDate);
                cmd.Parameters.AddWithValue("@PieceOrder", intervention.PieceOrder);
                cmd.Parameters.AddWithValue("@PieceCom", intervention.PieceCom);
                cmd.Parameters.AddWithValue("@TimeEstimate", intervention.TimeEstimate);
                cmd.Parameters.AddWithValue("@Priority", intervention.Priority.GetIntNamePriority(intervention.Priority.Name));
                cmd.Parameters.AddWithValue("@SiteId", intervention.SiteId.SiteId);
                cmd.Connection = db;

                try
                {
                    cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Update the intervention to database
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public bool UpdateIntervention(Intervention intervention)
        {
            string pieceComQuery = "";
            pieceComQuery = (!string.IsNullOrEmpty(intervention.PieceCom)) ? ", PieceCom = @PieceCom" : "";
            string query = "UPDATE Intervention SET VehicleId = @VehicleId, DateIn = @DateIn, DateOut = @DateOut, DateEstimate = @DateEstimate, Applicant = @Applicant, Submitter = @Submitter, InterventionTypeId = @InterventionTypeId, StatusId = @StatusId, StatusDate = @StatusDate, PieceOrder = @PieceOrder" + pieceComQuery + ", TimeEstimate = @TimeEstimate, Priority = @Priority, SiteId = @SiteId, ModifiedDateTime = @ModifiedDateTime, ModifiedBy = @ModifiedBy WHERE InterventionId = @InterventionId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@VehicleId", intervention.vehicleId.VehicleId);
                cmd.Parameters.AddWithValue("@DateIn", intervention.DateIn);
                cmd.Parameters.AddWithValue("@DateOut", intervention.DateOut);
                cmd.Parameters.AddWithValue("@DateEstimate", intervention.DateEstimate.Date);
                cmd.Parameters.AddWithValue("@Applicant", intervention.Applicant.EmployeeId);
                cmd.Parameters.AddWithValue("@Submitter", intervention.Submitter.EmployeeId);
                cmd.Parameters.AddWithValue("@InterventionTypeId", intervention.interventionTypeId.InterventionTypeId);
                cmd.Parameters.AddWithValue("@StatusId", intervention.StatusId.StatusId);
                cmd.Parameters.AddWithValue("@StatusDate", intervention.StatusDate.Date);
                cmd.Parameters.AddWithValue("@PieceOrder", intervention.PieceOrder);
                if (!string.IsNullOrEmpty(intervention.PieceCom))
                    cmd.Parameters.AddWithValue("@PieceCom", intervention.PieceCom);
                cmd.Parameters.AddWithValue("@TimeEstimate", intervention.TimeEstimate);
                cmd.Parameters.AddWithValue("@Priority", intervention.Priority.GetIntNamePriority(intervention.Priority.Name));
                cmd.Parameters.AddWithValue("@SiteId", intervention.SiteId.SiteId);
                cmd.Parameters.AddWithValue("@InterventionId", intervention.interventionId);
                cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
                if (!string.IsNullOrEmpty(CurrentUser.employeeId))
                    cmd.Parameters.AddWithValue("@ModifiedBy", CurrentUser.employeeId);

                try
                {
                    cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Update the barcode to intervention
        /// </summary>
        /// <param name="interventionId"></param>
        /// <param name="Barcode"></param>
        /// <returns></returns>
        public bool UpdateBarcodeIntervention(int interventionId, int Barcode)
        {
            string query = "UPDATE Intervention SET Barcode = @Barcode WHERE InterventionId = @InterventionId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Barcode", Barcode);
                cmd.Parameters.AddWithValue("@InterventionId", interventionId);

                try
                {
                    cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Insert the old status in Status history table
        /// </summary>
        /// <param name="statusHistory"></param>
        /// <returns></returns>
        private bool InsertStatusInStatusHistory(StatusHistory statusHistory)
        {
            string query = "INSERT INTO StatusHistory (StatusHistoryDate, InterventionId, StatusIdPrev) VALUES"
                + "(@StatusHistoryDate, @InterventionId, @StatusIdPrev);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@StatusHistoryDate", statusHistory.StatusHistoryDate);
                cmd.Parameters.AddWithValue("@InterventionId", statusHistory.InterventionId);
                cmd.Parameters.AddWithValue("@StatusIdPrev", statusHistory.StatusIdPrev);

                try
                {
                    cmd.ExecuteNonQuery();

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
        /// Update the status in intervention
        /// </summary>
        /// <param name="StatusId"></param>
        /// <param name="intervention"></param>
        /// <returns></returns>
        private bool UpdateStatusIntervention(int StatusId, Intervention intervention)
        {
            string query = "UPDATE Intervention SET StatusId = @StatusId, StatusDate = @StatusDate WHERE InterventionId = @InterventionId";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@StatusId", StatusId);
                cmd.Parameters.AddWithValue("@StatusDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@InterventionId", intervention.interventionId);

                try
                {
                    cmd.ExecuteNonQuery();

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
        /// Close the intervention
        /// </summary>
        /// <param name="intervention"></param>
        /// <returns></returns>
        public bool CloseIntervention(Intervention intervention)
        {
            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                int statusId = RST.SearchStatusIdByName("Close");

                if (!InsertStatusInStatusHistory(new StatusHistory(intervention.StatusDate, intervention.interventionId, intervention.StatusId.StatusId)))
                {
                    CloseConnection();
                    return false;
                }

                if (UpdateStatusIntervention(statusId, intervention))
                {
                    CloseConnection();
                    return true;
                }

                CloseConnection();
            }

            return false;
        }

        /// <summary>
        /// Change Status intervention to InProgress
        /// </summary>
        /// <param name="InterventionId"></param>
        public void ChangeStatusIntervention(int InterventionId)
        {
            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                int statusId = RST.SearchStatusIdByName("InProgress");

                Intervention intervention = SelectElementById(InterventionId);

                if (intervention.StatusId.StatusId == statusId || intervention.StatusId.StatusId == RST.SearchStatusIdByName("Close"))
                    return;

                if (!InsertStatusInStatusHistory(new StatusHistory(intervention.StatusDate, intervention.interventionId, intervention.StatusId.StatusId)))
                    return;

                UpdateStatusIntervention(statusId, intervention);
            }
        }

        /// <summary>
        /// Check if the intervention already has a barcode
        /// </summary>
        /// <param name="InterventionId"></param>
        /// <returns></returns>
        public bool CheckIfExistingBarcodeForIntervention(int InterventionId)
        {
            string query = "SELECT COUNT(Barcode) FROM Intervention WHERE InterventionId = @InterventionId;";
            int countFindValue = 0;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand command = new SqlCommand(query, db);
                command.Parameters.AddWithValue("@InterventionId", InterventionId);

                try
                {
                    // int result = command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            countFindValue++;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                CloseConnection();
            }

            return (countFindValue > 0) ? true : false;
        }
    }
}
