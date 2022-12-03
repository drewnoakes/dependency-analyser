using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Microsoft.Msagl.Drawing;
using System.Text;

namespace DependencyAnalyser;

public partial class DependencyAnalyzerWindow
{
    private readonly OpenFileDialog _openFileDialog;
    private readonly FilterPreferences _filterPreferences = new();
    private readonly StringBuilderLogger _logger = new();

    private DependencyGraph<string> _dependencyGraph = new();

    public DependencyAnalyzerWindow()
    {
        _openFileDialog = new OpenFileDialog()
        {
            Filter = "All supported files|*.sln;*.csproj;*.vbproj;*.fsproj;*.exe;*.dll|" +
                     "Solution files (*.sln)|*.sln|" +
                     "Project files (*.csproj)|*.csproj;*.vbproj;*.fsproj|" +
                     "Assemblies (*.dll)|*.dll|" +
                     "Assemblies (*.exe)|*.exe|" +
                     "All files|*.*",
            Title = "Open File"
        };

        InitializeComponent();
    }

    private void OnAboutClicked(object sender, RoutedEventArgs e)
    {
        // TODO make this a dialog with a link button
        MessageBox.Show($"""
            .NET Assembly Dependency Analyser v{Assembly.GetExecutingAssembly().GetName().Version}

            Copyright Drew Noakes 2003-{DateTime.Now.Year}.

            Thanks to John Maher.

            Latest version at https://github.com/drewnoakes/dependency-analyser

            Charts provided using AutomaticGraphLayout. Dependency analysis provided by Roslyn.
            """);
    }

    private void OnFilterClicked(object sender, RoutedEventArgs e)
    {
        using (WaitCursor())
        {
            var filterForm = new FilterForm(_filterPreferences);

            if (filterForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UpdateDiagram();
        }
    }

    private void OnExitClicked(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    #region Wait cursor

    private IDisposable WaitCursor()
    {
        var reverter = new CursorReverter(Cursor, this);
        Cursor = Cursors.AppStarting;
        return reverter;
    }

    private sealed class CursorReverter : IDisposable
    {
        private readonly Cursor _cursor;
        private readonly Window _form;

        public CursorReverter(Cursor cursor, Window form)
        {
            _cursor = cursor;
            _form = form;
        }

        public void Dispose()
        {
            _form.Cursor = _cursor;
        }
    }

    #endregion

    private async void OnOpenClicked(object sender, RoutedEventArgs e)
    {
        // Start a new graph
        _dependencyGraph = new DependencyGraph<string>();

        await MergeFileAsync();
    }

    private async void OnMergeClicked(object sender, RoutedEventArgs e)
    {
        await MergeFileAsync();
    }

    private async Task MergeFileAsync()
    {
        try
        {
            if (_openFileDialog.ShowDialog() != true)
            {
                return;
            }

            string fileName = _openFileDialog.FileName;

            using (WaitCursor())
            {
                if (fileName.EndsWith(".sln") || fileName.EndsWith("proj"))
                {
                    await SolutionAndProjectFileAnalyser.AnalyseAsync(fileName, _dependencyGraph, _logger);
                }
                else
                {
                    var assembly = Assembly.LoadFrom(fileName);
                    AssemblyAnalyser.Analyze(assembly, _dependencyGraph, _logger);
                }

                _filterPreferences.SetAssemblyNames(_dependencyGraph.Nodes);
//                    EnableAndDisableMenuItems();
                UpdateDiagram();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
        }
        finally
        {
            _log.Text = _logger.ToString();
        }
    }

    private void UpdateDiagram()
    {
        var graph = new Graph();

        // TODO style projects and assemblies differently
        // TODO style nodes with no dependents differently
        // TODO style nodes with no dependencies differently
        // TODO track selection and show upstream/downstream differently somehow

        foreach (var depending in _dependencyGraph.Nodes)
        {
            if (!_filterPreferences.IncludeInPlot(depending))
                continue;

            foreach (var depended in _dependencyGraph.GetDependenciesForNode(depending))
            {
                if (!_filterPreferences.IncludeInPlot(depended))
                    continue;

                graph.AddEdge(depending, depended);
            }
        }

        graph.Attr.LayerDirection = LayerDirection.TB;
        graph.Attr.AspectRatio = _graphControl.ActualWidth / _graphControl.ActualHeight;

        _graphControl.Graph = null;
        _graphControl.Graph = graph;

//            _graphControl.InvalidateMeasure();
//            _graphControl.InvalidateVisual();
    }

    private void OnCopyMermaidCodeClicked(object sender, RoutedEventArgs e)
    {
        StringBuilder sb = new();

        sb.AppendLine("flowchart TD");

        foreach (var depending in _dependencyGraph.Nodes)
        {
            if (!_filterPreferences.IncludeInPlot(depending))
                continue;

            foreach (var depended in _dependencyGraph.GetDependenciesForNode(depending))
            {
                if (!_filterPreferences.IncludeInPlot(depended))
                    continue;

                sb.Append("    ").Append(depending).Append(" --> ").Append(depended).AppendLine();
            }
        }

        Clipboard.SetText(sb.ToString());
    }

    private void OnSimplifyClicked(object sender, RoutedEventArgs e)
    {
        using (WaitCursor())
        {
            _dependencyGraph.TransitiveReduce();

            UpdateDiagram();
        }
    }
}
