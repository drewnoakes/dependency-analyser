After manually drawing an assembly dependency graph for a twenty-something-project solution in 2003, this software was born.

## How it works

Run the application, and open a .NET assembly from the _File|Open..._ menu (or just press CTRL+O).

The analyser will immediately generate a diagram such as the one shown:

![Example screenshot from .NET Assembly Dependency Analyser graph](https://raw.githubusercontent.com/drewnoakes/dependency-analyser/master/Documentation/ui-unfiltered.png)

Most assemblies will reference large numbers of system assemblies, either directly or indirectly.
In this graph, both the `System` and `mscorlib` assemblies are referenced by almost all other assemblies.
The diagram is clearer without these explicit references.

See this graph, exported as a [PNG file](https://raw.githubusercontent.com/drewnoakes/dependency-analyser/master/Documentation/many-node-graph.png).

![Example of the exclude menu showing how to omit selected assemblies from the graph](https://raw.githubusercontent.com/drewnoakes/dependency-analyser/master/Documentation/filter-window.png)

Select which assemblies you want to include in the plot and press OK.

![A graph showing dependencies when most of the behind-the-scenes assemblies have been removed](https://raw.githubusercontent.com/drewnoakes/dependency-analyser/master/Documentation/ui-filtered.png)

This plot tells us a great deal about the analysed assembly (in this case, `Drew.DependencyAnalyser.Tests.dll`).
It requires three non-system assemblies: `Drew.DependencyAnalyser`, `Drew.DependencyAnalyser.Tests` and `nunit.framework`,
even though `nunit.framework` is not referenced directly.  We can also tell, unsurprisingly, that `Drew.DependencyAnalyser`
uses WinForms assemblies.

Note the circular dependency between `System` and `System.Xml`!!!

Excluding all `System` assemblies shows all dependent assemblies that must be deployed with the
selected assembly for it to operate properly.

![A graph showing uncluttered core dependencies when all framework and other supporting assemblies have been removed](https://raw.githubusercontent.com/drewnoakes/dependency-analyser/master/Documentation/four-node-graph.png)

Here's an example of the [SVG output](https://raw.githubusercontent.com/drewnoakes/dependency-analyser/master/Documentation/four-node-graph.svg).

## The graph

The graph is produced via a Dot script.  WinGraphViz is a COM component that generates Dot
images, and is used by this application.  It must be installed for the .NET dependency analyser
to work.

A Dot script may look something like this:

    digraph G {
      size="100,50"
      center=""
      ratio=All
      node[width=.25,hight=.375,fontsize=12,color=lightblue2,style=filled]
      1 -> 3;
      1 -> 17;
      3 -> 15;
      1 [label="Drew.DependencyAnalyser.Tests"];
      3 [label="Drew.DependencyAnalyser"];
      15 [label="Interop.WINGRAPHVIZLib"];
      17 [label="nunit.framework"];
    }

This markup can be seen in the 'Dot Output' tab.

More information on Dot can be found at http://www.graphviz.org/

## Installation

1. Download zipped binaries on the [releases page](https://github.com/drewnoakes/dependency-analyser/releases) and extract to a folder on your PC.
2. Run the WinGraphviz installer included in the archive (if you do not already have it installed).
3. Run `DependencyAnalyser.exe` to start the program.