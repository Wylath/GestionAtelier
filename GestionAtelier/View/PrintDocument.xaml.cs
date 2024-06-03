using System.Windows;
using System.Windows.Documents;

namespace GestionAtelier.View
{
    /// <summary>
    /// Logique d'interaction pour PrintDocument.xaml
    /// </summary>
    public partial class PrintDocument
    {
        private FixedDocumentSequence _document;
        public PrintDocument(FixedDocumentSequence document)
        {
            _document = document;
            InitializeComponent();
            PreviewD.Document = document;
        }
    }
}
