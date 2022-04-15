# Hierarchy
A .Net Standard library that can be used for hierarchical data

[![Nuget](https://github.com/Unskilledcrab/Hierarchy/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Unskilledcrab/Hierarchy/actions/workflows/dotnet.yml)

## Getting Started
1. Install the package from [NuGet](https://www.nuget.org/packages/Hierarchy)
2. Transform any flat hierarchical list into a hierarchical tree structure by using the **ToHierarchy** extension method
3. Use other extension methods to perform operations on the new Hierarchy list.

### Example
```csharp
private class Example
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = "";
}

private List<Example> flatList = new()
    {
        new() { Id = 1, ParentId = 0, Name = "Top Level 1" },
            new() { Id = 2, ParentId = 1, Name = "Top Level 1.1" },
            new() { Id = 3, ParentId = 1, Name = "Top Level 1.2" },
                new() { Id = 12, ParentId = 3, Name = "Top Level 1.2.1" },
            new() { Id = 10, ParentId = 1, Name = "Top Level 1.3" },
            new() { Id = 11, ParentId = 1, Name = "Top Level 1.4" },
        new() { Id = 4, ParentId = 0, Name = "Top Level 2" },
            new() { Id = 5, ParentId = 4, Name = "Top Level 2.1" },
            new() { Id = 6, ParentId = 4, Name = "Top Level 2.2" },
            new() { Id = 7, ParentId = 4, Name = "Top Level 2.3" },
                new() { Id = 13, ParentId = 7, Name = "Top Level 2.3.1" },
                    new() { Id = 14, ParentId = 13, Name = "Top Level 2.3.1.1" },
            new() { Id = 8, ParentId = 4, Name = "Top Level 2.4" },
        new() { Id = 9, ParentId = 0, Name = "Top Level 3" },
    };

private void TestTraversalMethod()
{
    // We convert the flat list to a hierarchy
    var hierarchyList = flatList.ToHierarchy(t => t.Id, t => t.ParentId);

    // We search through all nodes in the hierarchy for the one with Id = 7
    var node = hierarchyList.AllNodes().FirstOrDefault(n => n.Data?.Id == 7); // Returns the node with Id 7

    // We get all other nodes that are at the same level as this node
    var siblingNodes = node.SiblingNodes(); // Returns the following nodes { 5, 6, 8 }  (NOTE: This excludes the node being used)

    // We get all descendant nodes of this node
    var childNodes = node.DescendantNodes(); // Returns the following nodes { 13, 14 }

    // We get all ancestor nodes of this node
    var parentNodes = node.AncestorNodes(); // Returns the following nodes { 4 }

    // We get all leaf nodes (descendant nodes that do not have childen) of this node
    var leafNodes = node.LeafNodes(); // Returns the following node { 14 } 

    // We get the top level node of this branch (the highest level ancestor)
    var rootNode = node.RootNode(); // Returns the following node { 4 }
}
```
