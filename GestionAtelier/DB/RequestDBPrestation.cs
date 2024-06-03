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
    class RequestDBPrestation : FactoryDB<Prestation>
    {
        RequestDBIntervention RI = new RequestDBIntervention();
        RequestDBEmployee RE = new RequestDBEmployee();
        RequestDBPrestationType RPT = new RequestDBPrestationType();
        RequestDBFuel RF = new RequestDBFuel();

        /// <summary>
        /// Converty the SqlDataReader to prestation
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Prestation ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int PrestationId = 0;
            Intervention InterventionId = null;
            float HoursCount = 0.0f;
            Employee EmployeeId = null;
            PrestationType PrestationTypeId = null;
            DateTime Date = DateTime.Now;
            int TruckCrane = 0;
            int Hour = 0;
            int Kilometer = 0;
            int HourFuel = 0;
            int KilometerFuel = 0;
            DateTime DateFuel = DateTime.Now;
            Prestation prestation = null;
            Fuel fuel = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["PrestationId"].ToString()))
                    PrestationId = Convert.ToInt32(dr["PrestationId"]);
                if (!string.IsNullOrEmpty(dr["InterventionId"].ToString()))
                    InterventionId = RI.SelectElementById(Convert.ToInt32(dr["InterventionId"]));
                if (!string.IsNullOrEmpty(dr["HoursCount"].ToString()))
                    HoursCount = float.Parse(dr["HoursCount"].ToString());
                if (!string.IsNullOrEmpty(dr["EmployeeId"].ToString()))
                    EmployeeId = RE.GetEmployeeById(Convert.ToString(dr["EmployeeId"]));
                if (!string.IsNullOrEmpty(dr["PrestationTypeId"].ToString()))
                    PrestationTypeId = RPT.SelectElementById(Convert.ToInt32(dr["PrestationTypeId"]));
                if (!string.IsNullOrEmpty(dr["Date"].ToString()))
                    Date = Convert.ToDateTime(dr["Date"]);
                if (!string.IsNullOrEmpty(dr["TruckCrane"].ToString()))
                    TruckCrane = Convert.ToInt32(dr["TruckCrane"]);
                if (!string.IsNullOrEmpty(dr["Hour"].ToString()))
                    Hour = Convert.ToInt32(dr["Hour"]);
                if (!string.IsNullOrEmpty(dr["Kilometer"].ToString()))
                    Kilometer = Convert.ToInt32(dr["Kilometer"]);
                if (!string.IsNullOrEmpty(dr["HourFuel"].ToString()))
                    HourFuel = Convert.ToInt32(dr["HourFuel"]);
                if (!string.IsNullOrEmpty(dr["KilometerFuel"].ToString()))
                    KilometerFuel = Convert.ToInt32(dr["KilometerFuel"]);
                if (!string.IsNullOrEmpty(dr["DateFuel"].ToString()))
                    DateFuel = Convert.ToDateTime(dr["DateFuel"]);
                if (string.IsNullOrEmpty(dr["HourFuel"].ToString()) && string.IsNullOrEmpty(dr["KilometerFuel"].ToString()))
                {
                    fuel = RF.GetLastFuelByVehicleId(InterventionId.vehicleId.VehicleId);

                    if (fuel != null)
                        if (fuel.Hour > HourFuel || fuel.Kilometer > KilometerFuel)
                        {
                            HourFuel = fuel.Hour;
                            KilometerFuel = fuel.Kilometer;
                            DateFuel = fuel.DatePrise;
                        }
                }

                prestation = new Prestation(PrestationId, InterventionId, HoursCount, EmployeeId, PrestationTypeId, Date, TruckCrane, Hour, Kilometer, HourFuel, KilometerFuel, DateFuel);

                if (!string.IsNullOrEmpty(dr["Note"].ToString()))
                    prestation.Comment = dr["Note"].ToString();
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return prestation;
        }

        /// <summary>
        /// Return all prestation (not used)
        /// </summary>
        /// <returns></returns>
        public override List<Prestation> SelectAllElement()
        {
            return null;
        }

        /// <summary>
        /// Return prestation by id (not used)
        /// </summary>
        /// <param name="PrestationId"></param>
        /// <returns></returns>
        public override Prestation SelectElementById(int PrestationId)
        {
            return null;
        }

        /// <summary>
        /// Return all prestations
        /// </summary>
        /// <returns></returns>
        public List<Prestation> GetAllPrestations(bool forInvoice = false)
        {
            string query = "";
            if (forInvoice)
                query = "SELECT * FROM Prestation WHERE InvoiceId IS NULL OR InvoiceId = 0;";
            else query = "SELECT * FROM Prestation;";

            List<Prestation> allPrestation = new List<Prestation>();

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
                            allPrestation.Add(ConvertSqlDataReaderToClass(reader));
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

            return allPrestation;
        }

        /// <summary>
        /// Return the base for display the prestation to window intervention
        /// </summary>
        /// <returns></returns>
        public List<Prestation> GetAllPrestationsBase(int InterventionTypeId, string VehicleId, List<int> ActivePrestation = null)
        {
            string query = "SELECT pt.* FROM PrestationType pt JOIN InterventionPrestationGroup ipg ON ipg.InterventionTypeId = @InterventionTypeId AND ipg.PrestationTypeId = pt.PrestationTypeId;";
            List<Prestation> allPrestation = new List<Prestation>();
            int HourFuel = 0;
            int KilometerFuel = 0;
            DateTime DateFuel = DateTime.Now;
            Fuel fuel = null;


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
                        fuel = RF.GetLastFuelByVehicleId(VehicleId);

                        if (fuel != null)
                            if (fuel.Hour > 0 || fuel.Kilometer > 0)
                            {
                                HourFuel = fuel.Hour;
                                KilometerFuel = fuel.Kilometer;
                                DateFuel = fuel.DatePrise;
                            }

                        while (reader.Read())
                        {
                            if (ActivePrestation != null)
                            {
                                if (!ActivePrestation.Contains(Convert.ToInt32(reader["PrestationTypeId"])))
                                    allPrestation.Add(new Prestation(RPT.ConvertSqlDataReaderToClass(reader), DateTime.Now, DateFuel, HourFuel, KilometerFuel));
                            }
                            else allPrestation.Add(new Prestation(RPT.ConvertSqlDataReaderToClass(reader), DateTime.Now, DateFuel, HourFuel, KilometerFuel));
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

            return allPrestation;
        }

        /// <summary>
        /// Get All prestations by intervention with interventionId as parameter. If the call function is for the facture, forBill change for true value
        /// </summary>
        /// <param name="InterventionId"></param>
        /// <returns></returns>
        public List<Prestation> GetAllPrestationsByIntervention(int InterventionId)
        {
            string query = "SELECT * FROM Prestation WHERE InterventionId = @InterventionId;";
            List<Prestation> allPrestation = new List<Prestation>();

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
                            Prestation prestation = ConvertSqlDataReaderToClass(reader);
                            prestation.Realized = true;
                            if (prestation.TruckCrane == 1)
                                prestation.Truck = true;
                            if (prestation.TruckCrane == 2)
                                prestation.Crane = true;
                            allPrestation.Add(prestation);
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

            return allPrestation;
        }

        /// <summary>
        /// Return all prestations by invoice (Just using by the function GetAllInvoice())
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        public List<Prestation> GetAllPrestationByInvoice(int InvoiceId)
        {
            string query = "SELECT * FROM Prestation WHERE InvoiceId = @InvoiceId;";

            List<Prestation> allPrestation = new List<Prestation>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allPrestation.Add(ConvertSqlDataReaderToClass(reader));
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

            return allPrestation;
        }

        /// <summary>
        /// Check if the prestation is already present on the intervention
        /// </summary>
        /// <param name="InterventionId"></param>
        /// <param name="PrestationTypeId"></param>
        /// <returns></returns>
        public bool CheckIfPrestationIsAlreadyOnIntervention(int InterventionId, int PrestationId)
        {
            string query = "SELECT * FROM Prestation WHERE InterventionId = @InterventionId AND PrestationId = @PrestationId;";
            int countFindValue = 0;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand command = new SqlCommand(query, db);
                command.Parameters.AddWithValue("@InterventionId", InterventionId);
                command.Parameters.AddWithValue("@PrestationId", PrestationId);

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

        /// <summary>
        /// Insert new prestation to database
        /// </summary>
        /// <param name="prestation"></param>
        /// <returns></returns>
        public bool InsertNewPrestation(Prestation prestation)
        {
            string query = "INSERT INTO Prestation (InterventionId, HoursCount, EmployeeId, PrestationTypeId, Date, TruckCrane, Hour, Kilometer, Note, HourFuel, KilometerFuel, DateFuel) VALUES"
                + "(@InterventionId, @HoursCount, @employeeId, @prestationTypeId, @date, @TruckCrane, @Hour, @Kilometer, @Note, @HourFuel, @KilometerFuel, @DateFuel);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InterventionId", prestation.InterventionId.interventionId);
                cmd.Parameters.AddWithValue("@HoursCount", prestation.HoursCount);
                cmd.Parameters.AddWithValue("@employeeId", prestation.EmployeeId.EmployeeId);
                cmd.Parameters.AddWithValue("@prestationTypeId", prestation.PrestationTypeId.PrestationTypeId);
                cmd.Parameters.AddWithValue("@date", prestation.Date);
                cmd.Parameters.AddWithValue("@TruckCrane", prestation.TruckCrane);
                cmd.Parameters.AddWithValue("@Hour", prestation.HourVehicle);
                cmd.Parameters.AddWithValue("@Kilometer", prestation.KilometerVehicle);
                string note = (!string.IsNullOrEmpty(prestation.Comment) ? prestation.Comment : "");
                cmd.Parameters.AddWithValue("@Note", note);
                cmd.Parameters.AddWithValue("@HourFuel", prestation.HourFuel);
                cmd.Parameters.AddWithValue("@KilometerFuel", prestation.KilometerFuel);
                cmd.Parameters.AddWithValue("@DateFuel", prestation.DateFuel);

                try
                {
                    cmd.ExecuteNonQuery();

                    // Update the status in intervention if first prestation
                    RI.ChangeStatusIntervention(prestation.InterventionId.interventionId);

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
        /// Update prestation to database
        /// </summary>
        /// <param name="prestation"></param>
        /// <returns></returns>
        public bool UpdatePrestation(Prestation prestation)
        {
            string query = "UPDATE Prestation SET InterventionId = @InterventionId, HoursCount = @HoursCount, EmployeeId = @employeeId, PrestationTypeId = @prestationTypeId, Date = @date, Hour = @Hour, Kilometer = @Kilometer, TruckCrane = @TruckCrane, Note = @Note, HourFuel = @HourFuel, KilometerFuel = @KilometerFuel, DateFuel = @DateFuel, ModifiedDateTime = @ModifiedDateTime, ModifiedBy = @ModifiedBy WHERE PrestationId = @PrestationId";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@InterventionId", prestation.InterventionId.interventionId);
                cmd.Parameters.AddWithValue("@HoursCount", prestation.HoursCount);
                cmd.Parameters.AddWithValue("@employeeId", prestation.EmployeeId.EmployeeId);
                cmd.Parameters.AddWithValue("@prestationTypeId", prestation.PrestationTypeId.PrestationTypeId);
                cmd.Parameters.AddWithValue("@date", prestation.Date);
                cmd.Parameters.AddWithValue("@Hour", prestation.HourVehicle);
                cmd.Parameters.AddWithValue("@Kilometer", prestation.KilometerVehicle);
                cmd.Parameters.AddWithValue("@TruckCrane", prestation.TruckCrane);
                cmd.Parameters.AddWithValue("@PrestationId", prestation.PrestationId);
                string note = (!string.IsNullOrEmpty(prestation.Comment) ? prestation.Comment : "");
                cmd.Parameters.AddWithValue("@Note", note);
                cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
                if (!string.IsNullOrEmpty(CurrentUser.employeeId))
                    cmd.Parameters.AddWithValue("@ModifiedBy", CurrentUser.employeeId);
                cmd.Parameters.AddWithValue("@HourFuel", prestation.HourFuel);
                cmd.Parameters.AddWithValue("@KilometerFuel", prestation.KilometerFuel);
                cmd.Parameters.AddWithValue("@DateFuel", prestation.DateFuel);

                try
                {
                    cmd.ExecuteNonQuery();

                    // Update the status in intervention if first prestation
                    RI.ChangeStatusIntervention(prestation.InterventionId.interventionId);
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
