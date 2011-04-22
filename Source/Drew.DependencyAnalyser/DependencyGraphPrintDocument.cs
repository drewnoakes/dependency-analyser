using System.Drawing;
using System.Drawing.Printing;

namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Logic for printing dependency graphs.
    /// </summary>
    public class DependencyGraphPrintDocument : PrintDocument
    {
        Image _dotImage;

        public DependencyGraphPrintDocument(Image dotImage)
        {
            _dotImage = dotImage;
        }

//        protected override void OnBeginPrint(PrintEventArgs ev) 
//        {
//            base.OnBeginPrint(ev) ;
//        }

        protected override void OnPrintPage(PrintPageEventArgs args) 
        {
            base.OnPrintPage(args);

            float leftMargin = args.MarginBounds.Left;
            float topMargin = args.MarginBounds.Top;
            float bottomMargin = args.MarginBounds.Bottom;

            float availableHeight = args.MarginBounds.Height;
            float availableWidth = args.MarginBounds.Width;

            using (var font = new Font("Arial", 8))
            {
                args.Graphics.DrawString("Created using .NET Dependency Analyser - http://drewnoakes.com/code/", font, Brushes.Black, leftMargin, bottomMargin - font.Height, new StringFormat());

                // maximise image on page without distorting dimensions
                args.Graphics.DrawImage(_dotImage, leftMargin, topMargin, availableWidth, availableHeight - font.Height);
            }

            args.HasMorePages = false;
        }
    }
}
