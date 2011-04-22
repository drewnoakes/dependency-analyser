using System.Drawing;
using System.Drawing.Printing;

namespace Drew.DependencyPlotter
{
	/// <summary>
	/// Logic for printing dependancy graphs.
	/// </summary>
	public class DependancyGraphPrintDocument : PrintDocument
	{
		Image _dotImage;

		public DependancyGraphPrintDocument(Image dotImage)
		{
			_dotImage = dotImage;
		}

//		protected override void OnBeginPrint(PrintEventArgs ev) 
//		{
//			base.OnBeginPrint(ev) ;
//		}

		protected override void OnPrintPage(PrintPageEventArgs args) 
		{
			base.OnPrintPage(args);

			float leftMargin = args.MarginBounds.Left;
			float topMargin = args.MarginBounds.Top;

			float availableHeight = args.MarginBounds.Height;
			float availableWidth = args.MarginBounds.Width;

			Font font = new Font("Arial", 8);
			args.Graphics.DrawString("Created using .NET Dependancy Grapher", font, Brushes.Black, leftMargin, topMargin, new StringFormat());

			// maximise image on page without distorting dimensions
			args.Graphics.DrawImage(_dotImage, leftMargin, topMargin + font.Height, availableWidth, availableHeight - font.Height);

			args.HasMorePages = false;
		}
	}
}
