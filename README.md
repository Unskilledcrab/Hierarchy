# Hierarchy
A .Net Standard library that can be used for hierarchical data

[![Nuget](https://github.com/Unskilledcrab/Hierarchy/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Unskilledcrab/Hierarchy/actions/workflows/dotnet.yml)

## Getting Started
1. Install the package from [NuGet](https://www.nuget.org/packages/Hierarchy)
2. Transform any flat hierarchical list into a hierarchical tree structure by using the **ToHierarchy** extension method
3. Use other extension methods to perform operations on the new Hierarchy list.

### Example
```csharp
using Hierarchy;

public class Person
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return $"Person: {Id} '{Name}'";
    }
}

List<Person> flatList = new()
{
    new() { Id = 1, ParentId = 0, Name = "CEO" },
    new() { Id = 2, ParentId = 1, Name = "North America Operaror" },
    new() { Id = 3, ParentId = 1, Name = "South America Operator" },
    new() { Id = 12, ParentId = 3, Name = "Brazil Operator" },
    new() { Id = 10, ParentId = 1, Name = "Europe Operator" },
    new() { Id = 11, ParentId = 1, Name = "Africa Operator" },
    new() { Id = 4, ParentId = 0, Name = "CFO" },
    new() { Id = 5, ParentId = 4, Name = "Financial Manager" },
    new() { Id = 6, ParentId = 4, Name = "Financial Designer" },
    new() { Id = 7, ParentId = 4, Name = "Financial Senior Team Lead" },
    new() { Id = 14, ParentId = 7, Name = "Financial Project 2: Lead" },
    new() { Id = 13, ParentId = 7, Name = "Financial Project 1: Lead" },
    new() { Id = 15, ParentId = 13, Name = "Financial Project 1: Member 1" },
    new() { Id = 16, ParentId = 13, Name = "Financial Project 1: Member 2" },
    new() { Id = 17, ParentId = 13, Name = "Financial Project 1: Member 3" },
    new() { Id = 18, ParentId = 13, Name = "Financial Project 1: Member 4" },
    new() { Id = 19, ParentId = 14, Name = "Financial Project 2: Member 1" },
    new() { Id = 20, ParentId = 14, Name = "Financial Project 2: Member 2" },
    new() { Id = 21, ParentId = 14, Name = "Financial Project 2: Member 3" },
    new() { Id = 22, ParentId = 14, Name = "Financial Project 2: Member 4" },
    new() { Id = 23, ParentId = 22, Name = "Financial Project 2: Grunt 1" },
    new() { Id = 24, ParentId = 22, Name = "Financial Project 2: Grunt 2" },
    new() { Id = 25, ParentId = 22, Name = "Financial Project 2: Grunt 3" },
    new() { Id = 26, ParentId = 22, Name = "Financial Project 2: Grunt 4" },
    new() { Id = 27, ParentId = 26, Name = "Financial Project 2: Leaf 1" },
    new() { Id = 28, ParentId = 26, Name = "Financial Project 2: Leaf 2" },
    new() { Id = 29, ParentId = 26, Name = "Financial Project 2: Leaf 3" },
    new() { Id = 30, ParentId = 26, Name = "Financial Project 2: Leaf 4" },
    new() { Id = 8, ParentId = 4, Name = "Marketing Manager" },
    new() { Id = 9, ParentId = 0, Name = "COO" },
};

var hierarchyList = flatList.ToHierarchy(t => t.Id, t => t.ParentId);
Console.WriteLine("We convert the flat list to a hierarchy");
Console.WriteLine(hierarchyList.PrintTree());

// NOTE: When you want to search through the entire tree, you must start with the **AllNodes()** extension method 
//       this will make sure you aren't performing linq operations just on the nodes at the top level
var node = hierarchyList.AllNodes().First(n => n.Data.Id == 14); 
Console.WriteLine("We search through all nodes in the hierarchy for the one with this id");
Console.WriteLine(node.Data);
Console.WriteLine();

var siblingNodes = node.SiblingNodes();
Console.WriteLine("We get all other nodes that are at the same level as this node");
Console.WriteLine(siblingNodes.PrintNodes());

var childNodes = node.DescendantNodes();
Console.WriteLine("We get all descendant nodes of this node");
Console.WriteLine(childNodes.PrintNodes());

var parentNodes = node.AncestorNodes(); 
Console.WriteLine("We get all ancestor nodes of this node");
Console.WriteLine(parentNodes.PrintNodes());

var leafNodes = node.LeafNodes();
Console.WriteLine("We get all leaf nodes (descendant nodes that do not have childen) of this node");
Console.WriteLine(leafNodes.PrintNodes());

var rootNode = node.RootNode(); 
Console.WriteLine("We get the top level node of this branch (the highest level ancestor)");
Console.WriteLine(rootNode.Data);

Console.ReadLine();
```

The output of the code above will look like this

```bash
We convert the flat list to a hierarchy
Person: 1 'CEO'
├─ Person: 2 'North America Operaror'
├─ Person: 3 'South America Operator'
│   └─ Person: 12 'Brazil Operator'
├─ Person: 10 'Europe Operator'
└─ Person: 11 'Africa Operator'
Person: 4 'CFO'
├─ Person: 5 'Financial Manager'
├─ Person: 6 'Financial Designer'
├─ Person: 7 'Financial Senior Team Lead'
│   ├─ Person: 14 'Financial Project 2: Lead'
│   │   ├─ Person: 19 'Financial Project 2: Member 1'
│   │   ├─ Person: 20 'Financial Project 2: Member 2'
│   │   ├─ Person: 21 'Financial Project 2: Member 3'
│   │   └─ Person: 22 'Financial Project 2: Member 4'
│   │       ├─ Person: 23 'Financial Project 2: Grunt 1'
│   │       ├─ Person: 24 'Financial Project 2: Grunt 2'
│   │       ├─ Person: 25 'Financial Project 2: Grunt 3'
│   │       └─ Person: 26 'Financial Project 2: Grunt 4'
│   │           ├─ Person: 27 'Financial Project 2: Leaf 1'
│   │           ├─ Person: 28 'Financial Project 2: Leaf 2'
│   │           ├─ Person: 29 'Financial Project 2: Leaf 3'
│   │           └─ Person: 30 'Financial Project 2: Leaf 4'
│   └─ Person: 13 'Financial Project 1: Lead'
│       ├─ Person: 15 'Financial Project 1: Member 1'
│       ├─ Person: 16 'Financial Project 1: Member 2'
│       ├─ Person: 17 'Financial Project 1: Member 3'
│       └─ Person: 18 'Financial Project 1: Member 4'
└─ Person: 8 'Marketing Manager'
Person: 9 'COO'

We search through all nodes in the hierarchy for the one with this id
Person: 14 'Financial Project 2: Lead'

We get all other nodes that are at the same level as this node
Person: 13 'Financial Project 1: Lead'

We get all descendant nodes of this node
Person: 19 'Financial Project 2: Member 1'
Person: 20 'Financial Project 2: Member 2'
Person: 21 'Financial Project 2: Member 3'
Person: 22 'Financial Project 2: Member 4'
Person: 23 'Financial Project 2: Grunt 1'
Person: 24 'Financial Project 2: Grunt 2'
Person: 25 'Financial Project 2: Grunt 3'
Person: 26 'Financial Project 2: Grunt 4'
Person: 27 'Financial Project 2: Leaf 1'
Person: 28 'Financial Project 2: Leaf 2'
Person: 29 'Financial Project 2: Leaf 3'
Person: 30 'Financial Project 2: Leaf 4'

We get all ancestor nodes of this node
Person: 7 'Financial Senior Team Lead'
Person: 4 'CFO'

We get all leaf nodes (descendant nodes that do not have childen) of this node
Person: 19 'Financial Project 2: Member 1'
Person: 20 'Financial Project 2: Member 2'
Person: 21 'Financial Project 2: Member 3'
Person: 23 'Financial Project 2: Grunt 1'
Person: 24 'Financial Project 2: Grunt 2'
Person: 25 'Financial Project 2: Grunt 3'
Person: 27 'Financial Project 2: Leaf 1'
Person: 28 'Financial Project 2: Leaf 2'
Person: 29 'Financial Project 2: Leaf 3'
Person: 30 'Financial Project 2: Leaf 4'

We get the top level node of this branch (the highest level ancestor)
Person: 4 'CFO'

```
