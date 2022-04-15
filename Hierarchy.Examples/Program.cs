using Hierarchy;

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
    new() { Id = 27, ParentId = 26, Name = "Financial Project 2: Grunt 1" },
    new() { Id = 28, ParentId = 26, Name = "Financial Project 2: Grunt 2" },
    new() { Id = 29, ParentId = 26, Name = "Financial Project 2: Grunt 3" },
    new() { Id = 30, ParentId = 26, Name = "Financial Project 2: Grunt 4" },
    new() { Id = 8, ParentId = 4, Name = "Marketing Manager" },
    new() { Id = 9, ParentId = 0, Name = "COO" },
};

var hierarchyList = flatList.ToHierarchy(t => t.Id, t => t.ParentId);
Console.WriteLine("We convert the flat list to a hierarchy");
Console.WriteLine(hierarchyList.PrintTree());

var node = hierarchyList.AllNodes().First(n => n.Data.Id == 7); // Returns the node with Id 7
Console.WriteLine("We search through all nodes in the hierarchy for the one with Id = 7");
Console.WriteLine(node.Data);
Console.WriteLine();

var siblingNodes = node.SiblingNodes(); // Returns the following nodes { 5, 6, 8 }  (NOTE: This excludes the node being used)
Console.WriteLine("We get all other nodes that are at the same level as this node");
Console.WriteLine(siblingNodes.PrintNodes());

var childNodes = node.DescendantNodes(); // Returns the following nodes { 13, 14 }
Console.WriteLine("We get all descendant nodes of this node");
Console.WriteLine(childNodes.PrintNodes());

var parentNodes = node.AncestorNodes(); // Returns the following nodes { 4 }
Console.WriteLine("We get all ancestor nodes of this node");
Console.WriteLine(parentNodes.PrintNodes());

var leafNodes = node.LeafNodes(); // Returns the following node { 14 } 
Console.WriteLine("We get all leaf nodes (descendant nodes that do not have childen) of this node");
Console.WriteLine(leafNodes.PrintNodes());

var rootNode = node.RootNode(); // Returns the following node { 4 }
Console.WriteLine("We get the top level node of this branch (the highest level ancestor)");
Console.WriteLine(rootNode.Data);

Console.ReadLine();