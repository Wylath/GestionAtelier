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
    class RequestDBEmployee : FactoryDB<Employee>
    {
        /// <summary>
        /// Converty the SqlDataReader to employee
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override Employee ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            string EmployeeId = "";
            string Name = "";
            string BadgeNumber = "";
            string Company = "";
            string Comment = "";
            string Responsible = "";
            int Status = 0;
            Employee employee = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["EmployeeId"].ToString()))
                    EmployeeId = Convert.ToString(dr["EmployeeId"]);
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    Name = Convert.ToString(dr["Name"]);
                if (!string.IsNullOrEmpty(dr["BadgeNumber"].ToString()))
                    BadgeNumber = Convert.ToString(dr["BadgeNumber"]);
                if (!string.IsNullOrEmpty(dr["Company"].ToString()))
                    Company = Convert.ToString(dr["Company"]);
                if (!string.IsNullOrEmpty(dr["Comment"].ToString()))
                    Comment = Convert.ToString(dr["Comment"]);
                if (!string.IsNullOrEmpty(dr["Responsible"].ToString()))
                    Responsible = Convert.ToString(dr["Responsible"]);
                if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                    Status = Convert.ToInt32(dr["Status"]);

                employee = new Employee(EmployeeId, Name, BadgeNumber, Company, Comment, Responsible, Status);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return employee;
        }

        /// <summary>
        /// Return all employee
        /// </summary>
        /// <returns></returns>
        public override List<Employee> SelectAllElement()
        {
            string query = "SELECT * FROM Employee ORDER BY Name;";
            List<Employee> allEmployee = new List<Employee>();

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
                            allEmployee.Add(ConvertSqlDataReaderToClass(reader));
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

            return allEmployee;
        }

        /// <summary>
        /// Return all employee with ProfileLevel 1,2,4 in UserProfile
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectAllEmployeeByUserProfile(ParmSite parmsite)
        {
            string query = "SELECT DISTINCT e.* FROM Employee e JOIN [User] u ON e.EmployeeId = u.EmployeeId JOIN RightUserProfile rup ON u.UserId = rup.UserId JOIN UserProfile up ON rup.UserProfileId = up.UserProfileId WHERE up.ProfileLevel IN (1, 2, 4) AND up.SiteId = @SiteId ORDER BY Name;";
            List<Employee> allEmployee = new List<Employee>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@SiteId", parmsite.SiteId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allEmployee.Add(ConvertSqlDataReaderToClass(reader));
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

            return allEmployee;
        }

        /// <summary>
        /// Return Employee by Id : type int (not used)
        /// </summary>
        /// <param name="ParmSiteId"></param>
        /// <returns></returns>
        public override Employee SelectElementById(int EmployeeId)
        {
            return null;
        }

        /// <summary>
        /// Return employee by id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public Employee GetEmployeeById(string EmployeeId)
        {
            string query = "SELECT * FROM Employee WHERE EmployeeId = @EmployeeId;";
            Employee Employee = null;

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
                            Employee = ConvertSqlDataReaderToClass(reader);
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

            return Employee;
        }

        /// <summary>
        /// Return employee by badgenumber
        /// </summary>
        /// <param name="BadgeNumber"></param>
        /// <returns></returns>
        public Employee GetEmployeeByBadgeNumber(string BadgeNumber)
        {
            string query = "SELECT * FROM Employee WHERE BadgeNumber = @BadgeNumber;";
            Employee Employee = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@BadgeNumber", BadgeNumber);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Employee = ConvertSqlDataReaderToClass(reader);
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

            return Employee;
        }
    }
}
