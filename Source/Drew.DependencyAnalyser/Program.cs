using System;
using System.Windows.Forms;

namespace Drew.DependencyAnalyser
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new DependencyAnalyserForm());
        }
    }
}
