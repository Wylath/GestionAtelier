using GestionAtelier.DB;
using GestionAtelier.Model;
using GestionAtelier.ToolBox;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier
{
    class RequestDBVehicle : FactoryDB<Vehicle>
    {
        /// <summary>
        /// Converty the SqlDataReader to vehicle
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Vehicle ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            string VehicleId = "";
            string CompanyId = "";
            string AssetID = "";
            string Name = "";
            string SerialNum = "";
            string VehicleType = "";
            string Make = "";
            string Model = "";
            string Responsible = "";
            string MajorType = "";
            string PaintNumber = "";
            string ModelYear = "";
            string TechInfo1 = "";
            string TechInfo2 = "";
            string TechInfo3 = "";
            int FuelType = 0;
            int Status = 0;
            DateTime SalesDate = DateTime.Now;
            DateTime NextControlTechn = DateTime.Now;
            string TotalCardNum = "";
            string BadgeEpack = "";
            string Department = "";
            string CostCenter = "";
            string Purpose = "";
            string PlateNumber = "";
            string PreviousPlateNumber = "";
            int IntervalHour = 0;
            int IntervalKilometer = 0;

            Vehicle vehicle = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["VehicleId"].ToString()))
                    VehicleId = Convert.ToString(dr["VehicleId"]);
                if (!string.IsNullOrEmpty(dr["CompanyId"].ToString()))
                    CompanyId = Convert.ToString(dr["CompanyId"]);
                if (!string.IsNullOrEmpty(dr["AssetID"].ToString()))
                    AssetID = Convert.ToString(dr["AssetID"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    Name = Convert.ToString(dr["Name"]);
                if (!string.IsNullOrEmpty(dr["SerialNum"].ToString()))
                    SerialNum = Convert.ToString(dr["SerialNum"]);
                if (!string.IsNullOrEmpty(dr["VehicleType"].ToString()))
                    VehicleType = Convert.ToString(dr["VehicleType"]);
                if (!string.IsNullOrEmpty(dr["Make"].ToString()))
                    Make = Convert.ToString(dr["Make"]);
                if (!string.IsNullOrEmpty(dr["Model"].ToString()))
                    Model = Convert.ToString(dr["Model"]);
                if (!string.IsNullOrEmpty(dr["Responsible"].ToString()))
                    Responsible = Convert.ToString(dr["Responsible"]);
                if (!string.IsNullOrEmpty(dr["MajorType"].ToString()))
                    MajorType = Convert.ToString(dr["MajorType"]);
                if (!string.IsNullOrEmpty(dr["PaintNumber"].ToString()))
                    PaintNumber = Convert.ToString(dr["PaintNumber"]);
                if (!string.IsNullOrEmpty(dr["ModelYear"].ToString()))
                    ModelYear = Convert.ToString(dr["ModelYear"]);
                if (!string.IsNullOrEmpty(dr["TechInfo1"].ToString()))
                    TechInfo1 = Convert.ToString(dr["TechInfo1"]);
                if (!string.IsNullOrEmpty(dr["TechInfo2"].ToString()))
                    TechInfo2 = Convert.ToString(dr["TechInfo2"]);
                if (!string.IsNullOrEmpty(dr["TechInfo3"].ToString()))
                    TechInfo3 = Convert.ToString(dr["TechInfo3"]);
                if (!string.IsNullOrEmpty(dr["FuelType"].ToString()))
                    FuelType = Convert.ToInt32(dr["FuelType"]);
                if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                    Status = Convert.ToInt32(dr["Status"]);
                if (!string.IsNullOrEmpty(dr["SalesDate"].ToString()))
                    SalesDate = Convert.ToDateTime(dr["SalesDate"]);
                if (!string.IsNullOrEmpty(dr["NextControlTechn"].ToString()))
                    NextControlTechn = Convert.ToDateTime(dr["NextControlTechn"]);
                if (!string.IsNullOrEmpty(dr["TotalCardNum"].ToString()))
                    TotalCardNum = Convert.ToString(dr["TotalCardNum"]);
                if (!string.IsNullOrEmpty(dr["BadgeEpack"].ToString()))
                    BadgeEpack = Convert.ToString(dr["BadgeEpack"]);
                if (!string.IsNullOrEmpty(dr["Department"].ToString()))
                    Department = Convert.ToString(dr["Department"]);
                if (!string.IsNullOrEmpty(dr["CostCenter"].ToString()))
                    CostCenter = Convert.ToString(dr["CostCenter"]);
                if (!string.IsNullOrEmpty(dr["Purpose"].ToString()))
                    Purpose = Convert.ToString(dr["Purpose"]);
                if (!string.IsNullOrEmpty(dr["PlateNumber"].ToString()))
                    PlateNumber = Convert.ToString(dr["PlateNumber"]);
                if (!string.IsNullOrEmpty(dr["PreviousPlateNumber"].ToString()))
                    PreviousPlateNumber = Convert.ToString(dr["PreviousPlateNumber"]);
                if (!string.IsNullOrEmpty(dr["IntervalHour"].ToString()))
                    IntervalHour = Convert.ToInt32(dr["IntervalHour"]);
                if (!string.IsNullOrEmpty(dr["IntervalKilometer"].ToString()))
                    IntervalKilometer = Convert.ToInt32(dr["IntervalKilometer"]);

                vehicle = new Vehicle(VehicleId, CompanyId, AssetID, Name, SerialNum, VehicleType, Make, Model, Responsible, MajorType, PaintNumber, ModelYear, TechInfo1, TechInfo2, TechInfo3, FuelType, Status, SalesDate, NextControlTechn, TotalCardNum, BadgeEpack, Department, CostCenter, Purpose, PlateNumber, PreviousPlateNumber, IntervalHour, IntervalKilometer);
            }
            catch (Exception ex)
            {

                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return vehicle;
        }

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns></returns>
        public override List<Vehicle> SelectAllElement()
        {
            string query = "SELECT * FROM Vehicles;";
            List<Vehicle> allVehicle = new List<Vehicle>();

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
                            allVehicle.Add(ConvertSqlDataReaderToClass(reader));
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

            return allVehicle;
        }

        /// <summary>
        /// Return the vehicle by id
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns></returns>
        public Vehicle GetVehicleById(string VehicleId)
        {
            string query = "SELECT * FROM Vehicles WHERE VehicleId = @VehicleId;";
            Vehicle vehicle = null;

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
                            vehicle = ConvertSqlDataReaderToClass(reader);
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

            return vehicle;
        }

        /// <summary>
        /// Return Vehicle by id (not used)
        /// </summary>
        /// <param name="TiresId"></param>
        /// <returns></returns>
        public override Vehicle SelectElementById(int TiresId)
        {
            return null;
        }
    }
}
