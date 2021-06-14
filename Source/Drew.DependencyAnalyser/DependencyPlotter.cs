using System.Drawing;
using WINGRAPHVIZLib;

namespace Drew.DependencyAnalyser
{
    public static class DependencyPlotter
    {
        public static PlotResult CalculatePlot(double aspectRatio, DependencyGraph<string> graph, FilterPreferences filterPreferences)
        {
            const double widthInches = 100;
            var heightInches = (double)(int)(widthInches / aspectRatio);

            // node [color=lightblue2, style=filled];
            // page=""8.5,11"" 
            // size=""7.5, 10""
            // ratio=All
            //                widthInches = 75;
            //                heightInches = 100;

            var extraCommands = $"size=\"{widthInches},{heightInches}\"\r\n    center=\"\"\r\n    ratio=All\r\n    node[width=.25,hight=.375,fontsize=12,color=lightblue2,style=filled]";
            var dotCommand = DotCommandBuilder.Generate(graph, filterPreferences, extraCommands);

            // a temp file to store image
            var tempFile = TemporaryFileManager.CreateTemporaryFile();

            // generate dot image
            var dot = new DOTClass();
            dot.ToPNG(dotCommand).Save(tempFile);
            var dotImage = Image.FromFile(tempFile);

            // generate SVG
            var svgXml = dot.ToSvg(dotCommand);

            return new PlotResult(dotCommand, dotImage, svgXml);
        }
    }

    public class PlotResult
    {
        public string DotCommand { get; }
        public Image Image { get; }
        public string SvgXml { get; }

        public PlotResult(string dotCommand, Image image, string svgXml)
        {
            DotCommand = dotCommand;
            Image = image;
            SvgXml = svgXml;
        }
    }
}