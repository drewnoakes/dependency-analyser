From [Wikipedia](http://en.wikipedia.org/wiki/DOT_language):

<blockquote>
DOT is a plain text graph description language. It is a simple way of describing graphs that both humans and computer programs can use.<br>
</blockquote>

The _.NET Dependency Analyser_ uses Dot to generate the directed graphs you see as its output.

More information on Dot can be found at http://www.graphviz.org/

Consider this graph:

![http://dependency-analyser.googlecode.com/svn/trunk/Documentation/four-node-graph.png](http://dependency-analyser.googlecode.com/svn/trunk/Documentation/four-node-graph.png)

The document that describes this graph is:

```
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
```

This markup is visible in .NET Dependency Analyster in the _Dot Output_ tab.

## WinGraphviz ##

The _.NET Dependency Analyser_ [currently](http://code.google.com/p/dependency-analyser/issues/detail?id=2) uses the WinGraphviz COM component to perform layout of the directed graph.  Hence you must install WinGraphviz in order to use this application.

WinGraphviz takes the Dot document as its input and produces the image shown.