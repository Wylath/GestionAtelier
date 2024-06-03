using Microsoft.Win32;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace GestionAtelier.View
{
    /// <summary>
    /// Logique d'interaction pour GenerateDocIntervention.xaml
    /// </summary>
    public partial class GenerateDocIntervention
    {
        public FixedDocumentSequence Document { get; set; }

        public GenerateDocIntervention()
        {
            InitializeComponent();
        }

        private void Bt_print_intervention(object sender, RoutedEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory() + @"\InterventionDocuments\";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string fileName = directory + "printPreview.xps";

            if (File.Exists(fileName))
                File.Delete(fileName);

            var xpsDocument = new XpsDocument(fileName, FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            FlowDocument doc = FD;
            doc.PageHeight = ActualHeight;
            doc.PageWidth = ActualWidth;
            doc.ColumnWidth = 1104;
            writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator);
            Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();
            var windows = new PrintDocument(Document);
            windows.ShowDialog();
        }

        private void Bt_generate_PDF(object sender, RoutedEventArgs e)
        {
            SaveFileDialog pd = new SaveFileDialog();
            pd.FileName = "Intervention" + TxtBlock_VehicleName.Text; // Default file name
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
                FlowDocument doc = FD;
                doc.PageHeight = ActualHeight;
                doc.PageWidth = ActualWidth;
                doc.ColumnWidth = 1104;
                PageMediaSize pageMediaSize = new PageMediaSize(doc.PageWidth, doc.PageHeight);
                pt.PageMediaSize = pageMediaSize;
                writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator, pt);
                Document = xpsDocument.GetFixedDocumentSequence();
                xpsDocument.Close();
            }   
        }
    }
}
