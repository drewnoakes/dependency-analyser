<img src="img/logo.svg" width="100" />

# .NET Dependency Analyser

Shows the dependencies between .NET projects and assemblies as a graph.

## How it works

Run the application, and open a .NET assembly, project or solution from the _File|Open..._ menu (or just press <kbd>Ctrl</kbd>+<kbd>O</kbd>).

Let's see how it looks if we run the application on itself:

![Example screenshot from .NET Assembly Dependency Analyser graph](img/ui-unfiltered.png)

As you can see there is a lot of information here and the graph is impossible to read as-is.
It's possible to zoom and pan, and you can drag nodes around to make things easier to see,
but most of the time you'll want to hide assemblies you're not interested in.

Using the _Filter..._ command lets us exclude items we don't want to see. Removing several
`System.*`and other framework assemblies gives a clearer picture:

![A graph showing dependencies when most of the behind-the-scenes assemblies have been removed](img/ui-filtered.png)

You can open either assemblies or MSBuild project/solution files. The latter produces richer data,
including target frameworks which can be useful for multi-targeting projects.

## Simplify

The simplify button removes any "redundant" edges from the graph. For example if we started with:

```mermaid
flowchart LR
   A --> B
   A --> C
   B --> C
```

The dependency between `A` and `C` is shown two ways here. Both directly and indirectly (via `B`). The simplify function removes any direct reference which is also made indirectly, which can clean up the graph considerably. Our example would then become:


```mermaid
flowchart LR
   A --> B
   B --> C
```

## Mermaid support

The _File_ menu has an option to copy the diagram using the mermaid syntax, which is [supported on GitHub](https://github.blog/2022-02-14-include-diagrams-markdown-files-mermaid/).

Our example from above would be:

```mermaid
flowchart TD
    DependencyAnalyser --> Microsoft.Msagl.WpfGraphControl
    DependencyAnalyser --> AutomaticGraphLayout.Drawing
    DependencyAnalyser --> Microsoft.Build.Locator
    DependencyAnalyser --> Microsoft.CodeAnalysis.Workspaces
    DependencyAnalyser --> Microsoft.CodeAnalysis.Workspaces.MSBuild
    Microsoft.Msagl.WpfGraphControl --> AutomaticGraphLayout.Drawing
    Microsoft.Msagl.WpfGraphControl --> AutomaticGraphLayout
    AutomaticGraphLayout.Drawing --> AutomaticGraphLayout
    Microsoft.CodeAnalysis.Workspaces --> Microsoft.CodeAnalysis
    Microsoft.CodeAnalysis.Workspaces.MSBuild --> Microsoft.Build
    Microsoft.CodeAnalysis.Workspaces.MSBuild --> Microsoft.CodeAnalysis
    Microsoft.CodeAnalysis.Workspaces.MSBuild --> Microsoft.Build.Framework
    Microsoft.CodeAnalysis.Workspaces.MSBuild --> Microsoft.CodeAnalysis.Workspaces
```

## Installation

1. Download zipped binaries on the [releases page](https://github.com/drewnoakes/dependency-analyser/releases) and extract to a folder on your PC.
2. Run `DependencyAnalyser.exe` to start the program.