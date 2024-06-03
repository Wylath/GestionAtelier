using GestionAtelier.ToolBox;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace GestionAtelier
{
    abstract class DBConnect
    {
        protected SqlConnection db;
        private string server;
        private string database;
        private string domain;
        private string uid;
        private string password;

        protected const string defaultLanguage = "en-GB";
        protected string localeLanguage = "";

        protected static string method = "";
        protected static int line = 0;

        protected static void ShowDebugInfo()
        {
            StackFrame stackFrame = new StackFrame(1, true);

            method = stackFrame.GetMethod().ToString();
            line = stackFrame.GetFileLineNumber();
        }

        /// <summary>
        /// Constructor DBConnect
        /// </summary>
        protected DBConnect()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize values to db
        /// </summary>
        protected void Initialize()
        {
            server = ConfigurationManager.AppSettings["server"].ToString();
            database = ConfigurationManager.AppSettings["database"].ToString();
            domain = ConfigurationManager.AppSettings["domain"].ToString();
            uid = ConfigurationManager.AppSettings["uid"].ToString();
            password = ConfigurationManager.AppSettings["password"].ToString();
            string ConnectionString;
            if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(database))
            {
                if (!string.IsNullOrEmpty(domain) && !string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(password))
                    ConnectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";Integrated Security=True" + "User ID = " + domain + @"\" + uid + "; Password = " + password + "; MultipleActiveResultSets = True;";
                else if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(password))
                    ConnectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";Integrated Security=True" + "User ID = " + uid + "; Password = " + password + "; MultipleActiveResultSets = True;";
                else
                    ConnectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";Integrated Security=True; MultipleActiveResultSets = True;";
            }
            else
            {
                ShowDebugInfo();
                throw new MyErrorException("Error with the server or the database in App.config.", GetType().Name, method, line);
            }

            db = new SqlConnection(ConnectionString);

            CultureInfo current = CultureInfo.CurrentCulture;
            
            switch (current.Name)
            {
                case "fr-BE":
                case "fr-FR":
                    localeLanguage = "fr-FR";
                    break;
                case "nl-NL":
                case "nl-BE":
                    localeLanguage = "nl-NL";
                    break;
                case "pl-PL":
                    localeLanguage = "pl-PL";
                    break;
                default:
                    localeLanguage = defaultLanguage;
                    break;
            }

        }

        /// <summary>
        /// Return the column name for the table locales_broadcast_text
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        protected string GetColumnNameLocaleLanguage(string language)
        {
            switch(language)
            {
                case "fr-FR":
                    return "Text_frFR";
                case "nl-NL":
                    return "Text_nlNL";
                case "pl-PL":
                    return "Text_plPL";
                default:
                    return "Text_enGB";
            }
        }

        /// <summary>
        /// Check if column exists in the table
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected bool ColumnExists(IDataReader reader, string columnName)
        {
            return reader.GetSchemaTable().Rows.OfType<DataRow>().Any(row => row["ColumnName"].ToString() == columnName);
        }

        /// <summary>
        /// Check if the connection to the db is already open
        /// </summary>
        /// <returns></returns>
        protected bool CheckIfConnectionIsOpen()
        {
            try
            {
                if (db.State > 0)
                    return true;
                else return false;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        ShowDebugInfo();
                        throw new MyErrorException("Cannot connect to server. Contact administrator", GetType().Name, method, line, true);
                    case 1045:
                        ShowDebugInfo();
                        throw new MyErrorException("Invalid username/password, please try again", GetType().Name, method, line, true);
                }
                return false;
            }
        }

        /// <summary>
        /// open connection to the database
        /// </summary>
        /// <returns></returns>
        protected bool OpenConnection()
        {
            try
            {
                db.Open();
                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        ShowDebugInfo();
                        throw new MyErrorException("Cannot connect to server. Contact administrator", GetType().Name, method, line, true);
                    case 1045:
                        ShowDebugInfo();
                        throw new MyErrorException("Invalid username/password, please try again", GetType().Name, method, line, true);
                }
                return false;
            }
        }

        /// <summary>
        /// Close the connection to the db
        /// </summary>
        /// <returns></returns>
        protected bool CloseConnection()
        {
            try
            {
                db.Close();
                return true;
            }
            catch (Exception /*ex*/)
            {
                ShowDebugInfo();
                throw new MyErrorException("Cannot close the connection to server. Contact administrator", GetType().Name, method, line, true);
            }
        }
    }
}
