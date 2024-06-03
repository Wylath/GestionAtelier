using GestionAtelier.ToolBox;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace GestionAtelier.View
{
    /// <summary>
    /// Logique d'interaction pour GenerateDocInvoice.xaml
    /// </summary>
    public partial class GenerateDocInvoice
    {
        const string ErrorMessageGenerateInvoice = "Aucun numéro de facture n'a été rentré.";
        const string ErrorMessageSaveInvoice = "Erreur lors de l'enregistrement du document.";
        const string ErrorTitleGenerateInvoice = "Erreur numéro de facture.";
        const string ErrorTitleSaveInvoice = "Erreur sauvegarde";
        static string method = "";
        static int line = 0;

        private static void ShowDebugInfo()
        {
            StackFrame stackFrame = new StackFrame(1, true);

            method = stackFrame.GetMethod().ToString();
            line = stackFrame.GetFileLineNumber();
        }

        public FixedDocumentSequence Document { get; set; }

        public GenerateDocInvoice()
        {
            InitializeComponent();
        }

        private void Bt_print_invoice(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBlock_NumberInvoice.Text))
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageGenerateInvoice, ErrorTitleGenerateInvoice, GetType().Name, method, line);
            }

            string directory = Directory.GetCurrentDirectory() + @"\Invoice\PrintDocument\";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string nameInvoice = directory + "Facture_" + TxtBlock_NumberInvoice.Text + ".xps";
            string nameDetailInvoice = directory + "FactureDetail_" + TxtBlock_NumberInvoice.Text + ".xps";

            displayPrintFile(nameInvoice, FD);
            displayPrintFile(nameDetailInvoice, FD_Detail);

            Close();
        }

        private void Bt_generate_PDF(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBlock_NumberInvoice.Text))
            {
                ShowDebugInfo();
                new MyErrorException(ErrorMessageGenerateInvoice, ErrorTitleGenerateInvoice, GetType().Name, method, line);
            }

            string directory = Directory.GetCurrentDirectory() + @"\Invoice\SaveDocument\";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string nameFile = directory + "Facture_" + TxtBlock_NumberInvoice.Text;
            string nameFileDetail = directory + "FactureDetail_" + TxtBlock_NumberInvoice.Text;

            displaySaveFileDialog(nameFile, FD);
            displaySaveFileDialog(nameFileDetail, FD);

            Close();
        }

        /// <summary>
        /// Prepare the data for the print file windows
        /// </summary>
        /// <param name="nameFile"></param>
        /// <param name="infoDoc"></param>
        public void displayPrintFile(string nameFile, FlowDocument infoDoc)
        {
            if (File.Exists(nameFile))
                File.Delete(nameFile);

            var xpsDocument = new XpsDocument(nameFile, FileAccess.ReadWrite);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

            FlowDocument doc = infoDoc;
            doc.PageHeight = ActualHeight / 2;
            doc.PageWidth = ActualWidth;
            doc.ColumnWidth = 1104;

            writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator);

            Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();

            var windows = new PrintDocument(Document);
            windows.ShowDialog();
        }

        /// <summary>
        /// Prepare and display the data for the save file dialog
        /// </summary>
        /// <param name="fileDialog"></param>
        /// <param name="infoDoc"></param>
        public void displaySaveFileDialog(string nameFile, FlowDocument infoDoc)
        {
            SaveFileDialog pd = new SaveFileDialog();
            pd.FileName = nameFile; // Default file name
            pd.DefaultExt = ".xps"; // Default file extension
            pd.Filter = "XPS Documents (.xps)|*.xps";

            // Process save file dialog box results
            if (pd.ShowDialog() == true)
            {
                // Save document
                string filename = pd.FileName;

                if (File.Exists(filename))
                    File.Delete(filename);

                PrintTicket pt = new PrintTicket();
                pt.PageOrientation = PageOrientation.Landscape;
                var xpsDocument = new XpsDocument(filename, FileAccess.ReadWrite);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                FlowDocument doc = infoDoc;
                doc.PageHeight = ActualHeight / 2;
                doc.PageWidth = ActualWidth;
                doc.ColumnWidth = 1104;

                PageMediaSize pageMediaSizeDetail = new PageMediaSize(doc.PageWidth, doc.PageHeight);
                pt.PageMediaSize = pageMediaSizeDetail;

                try
                {
                    writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator, pt);
                }
                catch(Exception ex)
                {
                    ShowDebugInfo();
                    new MyErrorException(ErrorMessageSaveInvoice, ErrorTitleSaveInvoice, GetType().Name, method, line);
                }

                xpsDocument.Close();
            }
        }
    }
}
