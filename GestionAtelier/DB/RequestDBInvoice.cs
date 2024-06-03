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
    class RequestDBInvoice : FactoryDB<Invoice>
    {
        RequestDBPrestation RP = new RequestDBPrestation();

        /// <summary>
        /// Converty the SqlDataReader to Invoice
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public override Invoice ConvertSqlDataReaderToClass(SqlDataReader reader)
        {
            int InvoiceId = 0;
            float Ponderation = 0;
            int TVA = 0;
            int Price = 0;
            int NumberInvoice = 0;
            DateTime DateInvoice = new DateTime();
            List<Prestation> prestationsList = new List<Prestation>();
            Invoice invoice = null;

            try
            {
                if (!string.IsNullOrEmpty(reader["InvoiceId"].ToString()))
                    InvoiceId = Convert.ToInt32(reader["InvoiceId"]);
                if (!string.IsNullOrEmpty(reader["Ponderation"].ToString()))
                    Ponderation = float.Parse(Convert.ToString(reader["Ponderation"]));
                if (!string.IsNullOrEmpty(reader["TVA"].ToString()))
                    TVA = Convert.ToInt32(reader["TVA"]);
                if (!string.IsNullOrEmpty(reader["Price"].ToString()))
                    Price = Convert.ToInt32(reader["Price"]);
                if (!string.IsNullOrEmpty(reader["NumberInvoice"].ToString()))
                    NumberInvoice = Convert.ToInt32(reader["NumberInvoice"]);
                if (!string.IsNullOrEmpty(reader["DateInvoice"].ToString()))
                    DateInvoice = Convert.ToDateTime(reader["DateInvoice"]);

                prestationsList = RP.GetAllPrestationByInvoice(Convert.ToInt32(reader["InvoiceId"]));

                invoice = new Invoice(InvoiceId, Ponderation, TVA, Price, NumberInvoice, DateInvoice, prestationsList);
            }
            catch (Exception ex)
            {
                ShowDebugInfo();
                throw new MyErrorException(ex.Message, GetType().Name, method, line);
            }

            return invoice;
        }

        /// <summary>
        /// Return all invoice with intervention list
        /// </summary>
        /// <returns></returns>
        public override List<Invoice> SelectAllElement()
        {
            string query = "SELECT DISTINCT i.* FROM Invoice i JOIN Prestation p ON i.InvoiceId = p.InvoiceId;";

            List<Invoice> allInvoice = new List<Invoice>();

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
                            allInvoice.Add(ConvertSqlDataReaderToClass(reader));
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

            return allInvoice;
        }

        /// <summary>
        /// Return Invoice by id (not used)
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        public override Invoice SelectElementById(int InvoiceId)
        {
            return null;
        }

        /// <summary>
        /// Update the relation between prestation and invoice
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="InterventionId"></param>
        /// <returns></returns>
        private bool UpdatePrestationInvoice(int InvoiceId, int PrestationId)
        {
            string query = "UPDATE Prestation SET InvoiceId = @InvoiceId WHERE PrestationId = @PrestationId;";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@PrestationId", PrestationId);
                cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ShowDebugInfo();
                    throw new MyErrorException(ex.Message, GetType().Name, method, line);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Return the InvoiceId for a NumberInvoice
        /// </summary>
        /// <param name="NumberInvoice"></param>
        /// <returns></returns>
        private int GetInvoiceIdByNumberInvoice(int NumberInvoice)
        {
            string query = "SELECT InvoiceId FROM Invoice WHERE NumberInvoice = @NumberInvoice;";

            int InvoiceId = 0;

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@NumberInvoice", NumberInvoice);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            InvoiceId = Convert.ToInt32(reader["InvoiceId"]);
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

            return InvoiceId;
        }

        /// <summary>
        /// Insert the new invoice to database with prestationId
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="PrestationId"></param>
        /// <returns></returns>
        public bool InsertNewInvoice(Invoice invoice, List<Prestation> PrestationId)
        {
            string query = "INSERT INTO Invoice (Ponderation, TVA, Price, NumberInvoice, DateInvoice) VALUES"
                + "(@Ponderation, @TVA, @Price, @NumberInvoice, @DateInvoice);";

            if (!CheckIfConnectionIsOpen())
                OpenConnection();

            if (CheckIfConnectionIsOpen())
            {
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.Parameters.AddWithValue("@Ponderation", invoice.Ponderation);
                cmd.Parameters.AddWithValue("@TVA", invoice.TVA);
                cmd.Parameters.AddWithValue("@Price", invoice.Price);
                cmd.Parameters.AddWithValue("@NumberInvoice", invoice.NumberInvoice);
                cmd.Parameters.AddWithValue("@DateInvoice", invoice.DateInvoice.Date);

                try
                {
                    cmd.ExecuteNonQuery();

                    // Insert the relation between invoice and intervention
                    int invoiceId = 0;
                    invoiceId = GetInvoiceIdByNumberInvoice(invoice.NumberInvoice);
                    if (invoiceId > 0)
                    {
                        foreach (var prestation in PrestationId)
                        {
                            UpdatePrestationInvoice(invoiceId, prestation.PrestationId);
                        }
                    }

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
    }
}
