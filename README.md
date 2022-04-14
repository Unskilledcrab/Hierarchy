[![Nuget](https://github.com/Unskilledcrab/hierarchy/actions/workflows/Nuget.yml/badge.svg)](https://github.com/Unskilledcrab/hierarchy/actions/workflows/Nuget.yml)

# Hierarchy

## What is it
A .Net Standard library that can be used for hierarchical data

## How to use
Pass in any flat list that has an identification property and a parent identification property. Here below we have a class **Example** which has the properties **Id** and **ParentId**. 


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
        var hierarchyList = flatList.ToHierarchy(t => t.Id, t => t.ParentId);
        var node = hierarchyList.FirstOrDefault(n => n.Data?.Id == 7); // Returns the node with Id 7
        var siblingNodes = node.GetAllSiblingNodes(); // Returns the following nodes { 5, 6, 8 }  (NOTE: This excludes the node being used)
        var childNodes = node.GetAllChildrenNodes(); // Returns the following nodes { 13, 14 }
        var parentNodes = node.GetAllParentNodes(); // Returns the following nodes { 4 }
        var leafNodes = node.GetLeafNodes(); // Returns the following node { 14 } 
        var rootNode = node.GetRootNode(); // Returns
    }

