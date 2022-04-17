# Hierarchy
A .Net Standard library that can be used for hierarchical data

[![Nuget](https://github.com/Unskilledcrab/Hierarchy/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Unskilledcrab/Hierarchy/actions/workflows/dotnet.yml)

## Getting Started
1. Install the package from [NuGet](https://www.nuget.org/packages/Hierarchy)
2. Transform any flat hierarchical or graph list into a hierarchy or graph tree structure by using the **ToHierarchy** or **ToGraph** extension methods
3. Use other extension methods to perform operations on the new tree list.

## Traversal Methods
After building a hierarchy or graph there are two main forms of traversing the trees

- Breadth First
- Depth First

With a breadth first traversal the algorithm will enumerate through the entirety of the closests nodes in the closet layer before enumerating through the next layer.
This is a more performant traversal method to use when you know that what you are searching for is near the node you are searching on and is typically the desired search method.
For this reason, this is the default algorithm used in traversal methods.

With a depth first traversal the algorithm will enumerate through each relationship of a node before moving on to another node in the layer.
This can sometimes be desireable if you have extremely large layers but shallow depth and you know that what you are searching for is deeper in the tree.

Each of these methods has preventions in place for cyclic data (i.e. it will not get stuck in an infinite loop)

## Hierarchy
A hierarchy is a structure in which each node can only ever have a single parent node (or no parent in the case of a root node).
When building a hierarchy, there can not be any cyclic relationships (i.e. you can not have a node reference itself as the parent node).
You can optionally set a specific root id when building a hierarchy. This is useful if you have hierarchical data that has a specific parent id for root nodes like '-1'
By default it will build the hierarchy root nodes in the if any of the following conditions are met

- The parent id is null
- The parent id is not found in the flat list
- The parent id is the default datatype (i.e. if it is an int then the root node will have a parent id of '0') 

Since hierarchies will always have a root node(s), when you use the **ToHierarchy** extension method, you will be returned the root nodes. Each of the root nodes will
have their descendant nodes relationship fully defined so you can traverse the tree by enumerating through their children or using the traversal extension methods provided.

### Hierarchy Example
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
    new() { Id = 30, ParentId = 26, Name = "Financial Project 2: Leaf 4" },
    new() { Id = 28, ParentId = 26, Name = "Financial Project 2: Leaf 2" },
    new() { Id = 29, ParentId = 26, Name = "Financial Project 2: Leaf 3" },
    new() { Id = 8, ParentId = 4, Name = "Marketing Manager" },
    new() { Id = 9, ParentId = 0, Name = "COO" },
};

// NOTE: You can order the incoming list before building the hierarchy 
//       to make sure that your hierarchy is ordered
//       the way you want it to be before presenting and traversing it
var hierarchyList = flatList.OrderBy(f => f.Id).ToHierarchy(t => t.Id, t => t.ParentId);
Console.WriteLine("We convert the flat list to a hierarchy");
Console.WriteLine(hierarchyList.PrintTree());

// NOTE: When you want to search through the entire tree, 
//       you must start with the **AllNodes()** extension method 
//       this will make sure you aren't performing linq operations just on the nodes at the top level
var node = hierarchyList.AllNodes().First(n => n.Data.Id == 14);
Console.WriteLine("We search through all nodes in the hierarchy for the one with this id");
Console.WriteLine(node.Data);
Console.WriteLine();

var siblingNodes = node.SiblingNodes();
Console.WriteLine("We get all other nodes that are at the same level as this node");
Console.WriteLine(siblingNodes.PrintNodes());

var childNodesBreadthFirst = node.DescendantNodes(TraversalType.BreadthFirst);
Console.WriteLine("We get all descendant nodes of this node (Breadth First)");
Console.WriteLine(childNodesBreadthFirst.PrintNodes());

var childNodesDepthFirst = node.DescendantNodes(TraversalType.DepthFirst);
Console.WriteLine("We get all descendant nodes of this node (Depth First)");
Console.WriteLine(childNodesDepthFirst.PrintNodes());

var parentNodes = node.AncestorNodes();
Console.WriteLine("We get all ancestor nodes of this node");
Console.WriteLine(parentNodes.PrintNodes());

var leafNodesBreadthFirst = node.LeafNodes(TraversalType.BreadthFirst);
Console.WriteLine("We get all leaf nodes (descendant nodes that do not have childen) of this node (Breadth First)");
Console.WriteLine(leafNodesBreadthFirst.PrintNodes());

var leafNodesDepthFirst = node.LeafNodes(TraversalType.DepthFirst);
Console.WriteLine("We get all leaf nodes (descendant nodes that do not have childen) of this node (Depth First)");
Console.WriteLine(leafNodesDepthFirst.PrintNodes());

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

We get all descendant nodes of this node (Breadth First)
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

We get all descendant nodes of this node (Depth First)
Person: 22 'Financial Project 2: Member 4'
Person: 26 'Financial Project 2: Grunt 4'
Person: 30 'Financial Project 2: Leaf 4'
Person: 29 'Financial Project 2: Leaf 3'
Person: 28 'Financial Project 2: Leaf 2'
Person: 27 'Financial Project 2: Leaf 1'
Person: 25 'Financial Project 2: Grunt 3'
Person: 24 'Financial Project 2: Grunt 2'
Person: 23 'Financial Project 2: Grunt 1'
Person: 21 'Financial Project 2: Member 3'
Person: 20 'Financial Project 2: Member 2'
Person: 19 'Financial Project 2: Member 1'

We get all ancestor nodes of this node
Person: 7 'Financial Senior Team Lead'
Person: 4 'CFO'

We get all leaf nodes (descendant nodes that do not have childen) of this node (Breadth First)
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

We get all leaf nodes (descendant nodes that do not have childen) of this node (Depth First)
Person: 30 'Financial Project 2: Leaf 4'
Person: 29 'Financial Project 2: Leaf 3'
Person: 28 'Financial Project 2: Leaf 2'
Person: 27 'Financial Project 2: Leaf 1'
Person: 25 'Financial Project 2: Grunt 3'
Person: 24 'Financial Project 2: Grunt 2'
Person: 23 'Financial Project 2: Grunt 1'
Person: 21 'Financial Project 2: Member 3'
Person: 20 'Financial Project 2: Member 2'
Person: 19 'Financial Project 2: Member 1'

We get the top level node of this branch (the highest level ancestor)
Person: 4 'CFO'

```

## Graphs
A graph structure can have multiple parents and children, because of this there is allowed to be cyclic relationships.
You can optionally set a specific root id when building a graph. 
The majority of the time you will want to specify this root node to get back a specific section of the graph.
Because a graph can be cyclic in nature, the 'root' node(s) can be a node with parent nodes.
In this context a root node is just the node(s) that are returned to you as the first level to be enumerated over.
By default it will build the graph root nodes in the if any of the following conditions are met

- The parent id is null
- The parent id is not found in the flat list
- The parent id is the default datatype (i.e. if it is an int then the root node will have a parent id of '0') 

Since by design a graph node may not have any 'root' nodes, when the 'ToGraph' extension method is used, you will be returned the same flat list but
it will build all of the relationships and wrap the data in an **IGraphNode** model to be able to perform common traversal extension methods on.

### Graph Example 
```csharp
using Hierarchy;

public class PersonRelationships
{
    public int Id { get; set; }
    public int[] KnownRelationshipIds { get; set; } = new int[0];
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return $"Person: {Id} '{Name}'";
    }
}

List<PersonRelationships> flatList = new()
{
    new() { Id = 1, KnownRelationshipIds = new int[] { 2, 3, 4 }, Name = "Bob" },
    new() { Id = 2, KnownRelationshipIds = new int[] { 1, 3, 4 }, Name = "Mary" },
    new() { Id = 3, KnownRelationshipIds = new int[] { 1, 2, 4 }, Name = "Jane" },
    new() { Id = 4, KnownRelationshipIds = new int[] { 1, 2, 3 }, Name = "Lion" },
    new() { Id = 5, KnownRelationshipIds = new int[] { 6 }, Name = "Gorilla" },
    new() { Id = 6, KnownRelationshipIds = new int[] { 0 }, Name = "Monkey" },
    new() { Id = 7, KnownRelationshipIds = new int[] { 1, 2, 3, 4, 5 }, Name = "Noodle" },
    new() { Id = 8, KnownRelationshipIds = new int[] { 1, 2, 3 }, Name = "Cake" },
};

var hierarchyList = flatList.OrderBy(f => f.Id).ToGraph(t => t.Id, t => t.KnownRelationshipIds);
```
