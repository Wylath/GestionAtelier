using System;
using System.IO;
using System.Windows;

namespace GestionAtelier.ToolBox
{
    class MyErrorException : Exception
    {
        /// <summary>
        /// Save app error to file and display a message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="className"></param>
        /// <param name="method"></param>
        /// <param name="line"></param>
        public MyErrorException(string message, string title, string className, string method, int line)
        {
            string path = @"ErrorException.log";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Error message in GestionAtelier :");
                }
            }

            // Open the file to write to.
            using (StreamWriter sw = File.AppendText(path))
            {
                if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(title))
                {
                    sw.WriteLine("{0:yyyy/MM/dd H:mm:ss} - {1} - {2} line : {3} - Title : {4}. Description : {5}", DateTime.Now, className, method, line, title, message);
                }
            }

            MessageBox.Show(message, title);
            throw new Exception(message);
        }

        /// <summary>
        /// Save database error to file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="method"></param>
        /// <param name="line"></param>
        public MyErrorException(string message, string className, string method, int line, bool showMessageBox = false)
        {
            string path = @"DBError.log";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Error message in GestionAtelier :");
                }
            }

            // Open the file to write to.
            using (StreamWriter sw = File.AppendText(path))
            {
                if (!string.IsNullOrEmpty(message))
                {
                    sw.WriteLine("{0:yyyy/MM/dd H:mm:ss} - {1} - {2} line : {3}. Description : {4}", DateTime.Now, className, method, line, message);
                }
            }

            if (showMessageBox)
                MessageBox.Show(message, "Erreur avec la base de données");
        }
    }
}
