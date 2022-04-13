using System;
using System.Collections.Generic;
using System.Linq;

namespace Hierarchy
{
    public static partial class HierarchyExtensions
    {
        public static int Count<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            return Enumerable.Count(hierarchyNodes.ToFlatNodeList());
        }

        public static int Count<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<IHierarchyNode<TData>, bool> predicate)
        {
            return Enumerable.Count(hierarchyNodes.ToFlatNodeList(), predicate);
        }

        public static IEnumerable<IHierarchyNode<TData>> Except<TData>(this IEnumerable<IHierarchyNode<TData>> first, IEnumerable<IHierarchyNode<TData>> second)
        {
            return Enumerable.Except(first.ToFlatNodeList(), second.ToFlatNodeList());
        }

        public static bool All<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<IHierarchyNode<TData>, bool> predicate)
        {
            return Enumerable.All(hierarchyNodes.ToFlatNodeList(), predicate);
        }

        public static IEnumerable<IHierarchyNode<TData>> Where<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<IHierarchyNode<TData>, bool> predicate)
        {
            return Enumerable.Where(hierarchyNodes.ToFlatNodeList(), predicate);
        }

        public static IHierarchyNode<TData> First<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            return Enumerable.First(hierarchyNodes.ToFlatNodeList());
        }

        public static IHierarchyNode<TData> First<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<IHierarchyNode<TData>, bool> predicate)
        {
            return Enumerable.First(hierarchyNodes.ToFlatNodeList(), predicate);
        }

        public static IHierarchyNode<TData>? FirstOrDefault<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            return Enumerable.FirstOrDefault(hierarchyNodes.ToFlatNodeList());
        }

        public static IHierarchyNode<TData>? FirstOrDefault<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<IHierarchyNode<TData>, bool> predicate)
        {
            return Enumerable.FirstOrDefault(hierarchyNodes.ToFlatNodeList(), predicate);
        }
    }
}
