using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionAtelier.ToolBox
{
    class GenerateBarcode
    {
        private Canvas barcodeCanvas = new Canvas();

        public void AutoGenerateBarcode(string text, string directory, string nameFile, string extension, out string barcode)
        {
            /////////////////////////////////////
            // Encode The Data
            /////////////////////////////////////
            Barcode bb = new Barcode();
            bb.BarcodeType = Barcode.BarcodeEnum.Code39;
            bb.Data = text;
            bb.CheckDigit = Barcode.YesNoEnum.Yes;
            bb.encode();

            int thinWidth;
            int thickWidth;

            thinWidth = 3;
            thickWidth = 3 * thinWidth;

            string outputString = bb.EncodedData;
            string humanText = bb.HumanText;
            barcode = humanText;


            /////////////////////////////////////
            // Draw The Barcode
            /////////////////////////////////////
            int len = outputString.Length;
            int currentPos = 10;
            int currentTop = 10;
            int currentColor = 0;
            for (int i = 0; i < len; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Height = 200;
                if (currentColor == 0)
                {
                    currentColor = 1;
                    rect.Fill = new SolidColorBrush(Colors.Black);

                }
                else
                {
                    currentColor = 0;
                    rect.Fill = new SolidColorBrush(Colors.White);

                }
                Canvas.SetLeft(rect, currentPos);
                Canvas.SetTop(rect, currentTop);

                if (outputString[i] == 't')
                {
                    rect.Width = thinWidth;
                    currentPos += thinWidth;

                }
                else if (outputString[i] == 'w')
                {
                    rect.Width = thickWidth;
                    currentPos += thickWidth;

                }
                barcodeCanvas.Children.Add(rect);
            }


            /////////////////////////////////////
            // Add the Human Readable Text
            /////////////////////////////////////
            TextBlock tb = new TextBlock();
            tb.Text = humanText;
            tb.FontSize = 32;
            tb.FontFamily = new FontFamily("Courier New");
            Rect rx = new Rect(0, 0, 0, 0);
            tb.Arrange(rx);
            Canvas.SetLeft(tb, (currentPos - tb.ActualWidth) / 2);
            Canvas.SetTop(tb, currentTop + 205);
            barcodeCanvas.Children.Add(tb);
            barcodeCanvas.Width = currentPos + tb.ActualWidth;
            barcodeCanvas.Height = 200 + tb.ActualHeight;

            ExportCanvasToPnj(barcodeCanvas, directory, nameFile, extension);
        }

        private void ExportCanvasToPnj(Canvas canv, string directory, string nameFile, string extension)
        {
            Transform transform = canv.LayoutTransform;

            canv.LayoutTransform = null;

            Size size = new Size(canv.ActualWidth, canv.ActualHeight);

            canv.Measure(size);
            canv.Arrange(new Rect(size));

            RenderTargetBitmap renderBitmap =
                new RenderTargetBitmap(
                    Convert.ToInt32(canv.ActualWidth),
                Convert.ToInt32(canv.ActualHeight),
                96d,
                96d,
                PixelFormats.Pbgra32);

            Rect bounds = VisualTreeHelper.GetDescendantBounds(canv);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(canv);
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            renderBitmap.Render(dv);


            using (FileStream outStream = new FileStream(directory + nameFile + "." + extension, FileMode.Create))
            {

                PngBitmapEncoder encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                encoder.Save(outStream);
            }
        }
    }
}
