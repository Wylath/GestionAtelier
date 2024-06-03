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
    class RequestDBUser : FactoryDB<User>
    {
        RequestDBParmSite RPS = new RequestDBParmSite();

        /// <summary>
        /// Converty the SqlDataReader to User
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override User ConvertSqlDataReaderToClass(SqlDataReader dr)
        {
            int UserId = 0;
            int Active = 0;
            string EmployeeId = "";

            User user = null;

            try
            {
                if (!string.IsNullOrEmpty(dr["UserId"].ToString()))
                    UserId = Convert.ToInt32(dr["UserId"]);
                if (!string.IsNullOrEmpty(dr["Active"].ToString()))
                    Active = Convert.ToInt32(dr["Active"]);
                if (!string.IsNullOrEmpty(dr["EmployeeId"].ToString()))
                    EmployeeId = Convert.ToString(dr["EmployeeId"]);

                user = new User(UserId, Active, EmployeeId);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return user;
        }

        /// <summary>
        /// Return all user list
        /// </summary>
        /// <returns></returns>
        public override List<User> SelectAllElement()
        {
            string query = "SELECT * FROM [User];";
            List<User> allUser = new List<User>();

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
                            allUser.Add(ConvertSqlDataReaderToClass(reader));
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

            return allUser;
        }

        /// <summary>
        /// Return User By EmployeeId : parameter type int (not used)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public override User SelectElementById(int UserId)
        {
            return null;
        }

        /// <summary>
        /// Return User By EmployeeId
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public User GetUserByEmployeeId(string EmployeeId)
        {
            string query = "SELECT * FROM [User] WHERE EmployeeId = @EmployeeId;";
            User _User = null;

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
                            _User = ConvertSqlDataReaderToClass(reader);
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

            return _User;
        }

        /// <summary>
        /// Get level access for the employee
        /// </summary>
        /// <param name="_employeeId"></param>
        /// <returns></returns>
        public UserProfile GetLevelAccessForSession(string _employeeId, ParmSite _siteId)
        {
            string SiteId = _siteId.SiteId;
            string query = "SELECT up.* FROM [User] us JOIN RightUserProfile rup ON us.UserId = rup.UserId JOIN UserProfile up ON rup.UserProfileId = up.UserProfileId WHERE EmployeeId = @EmployeeId and up.SiteId = @SiteId;";
            UserProfile userProfile = null;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@EmployeeId", _employeeId);
                cmd.Parameters.AddWithValue("@SiteId", SiteId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userProfile = new UserProfile(Convert.ToInt32(reader["UserProfileId"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["ProfileLevel"]), RPS.GetParmSiteById(Convert.ToString(reader["SiteId"])));
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

            return userProfile;
        }

        /// <summary>
        /// Get all level access for the employee
        /// </summary>
        /// <param name="_employeeId"></param>
        /// <returns></returns>
        public List<UserProfile> GetAllLevelAccessForSession(string _employeeId)
        {
            string query = "SELECT up.* FROM [User] us JOIN RightUserProfile rup ON us.UserId = rup.UserId JOIN UserProfile up ON rup.UserProfileId = up.UserProfileId WHERE EmployeeId = @EmployeeId;";
            List<UserProfile> userProfile = new List<UserProfile>();

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@EmployeeId", _employeeId);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userProfile.Add(new UserProfile(Convert.ToInt32(reader["UserProfileId"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["ProfileLevel"]), RPS.GetParmSiteById(Convert.ToString(reader["SiteId"]))));
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

            return userProfile;
        }

        /// <summary>
        /// Check if user is active or not
        /// </summary>
        /// <param name="Employee"></param>
        /// <returns></returns>
        public bool CheckIfUserIsActive(string EmployeeId)
        {
            string query = "SELECT * FROM [User] WHERE EmployeeId = @EmployeeId AND Active = 1;";
            int countFindValue = 0;

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
                            countFindValue++;
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

            return (countFindValue > 0) ? true : false;
        }

        /// <summary>
        /// Update the status (active) for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateStatusUser(User user)
        {
            string query = "UPDATE [User] SET Active = @Active WHERE UserId = @UserId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@Active", user.Active);

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
    }
}
