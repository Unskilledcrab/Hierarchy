namespace Hierarchy.Examples
{
    public class GraphExample
    {
        public void Test()
        {

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
            Console.WriteLine("We convert the flat list to a hierarchy");
            //Console.WriteLine(hierarchyList.PrintTree());

            //var node = hierarchyList.AllNodes().First(n => n.Data.Id == 14);
            //Console.WriteLine("We search through all nodes in the hierarchy for the one with this id");
            //Console.WriteLine(node.Data);
            //Console.WriteLine();

            //var siblingNodes = node.SiblingNodes();
            //Console.WriteLine("We get all other nodes that are at the same level as this node");
            //Console.WriteLine(siblingNodes.PrintNodes());

            //var childNodes = node.DescendantNodes();
            //Console.WriteLine("We get all descendant nodes of this node");
            //Console.WriteLine(childNodes.PrintNodes());

            //var parentNodes = node.AncestorNodes();
            //Console.WriteLine("We get all ancestor nodes of this node");
            //Console.WriteLine(parentNodes.PrintNodes());

            //var leafNodes = node.LeafNodes();
            //Console.WriteLine("We get all leaf nodes (descendant nodes that do not have childen) of this node");
            //Console.WriteLine(leafNodes.PrintNodes());

            //var rootNode = node.RootNode();
            //Console.WriteLine("We get the top level node of this branch (the highest level ancestor)");
            //Console.WriteLine(rootNode.Data);

            Console.ReadLine();
        }
    }
}
