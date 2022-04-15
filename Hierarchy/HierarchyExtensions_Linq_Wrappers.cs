using System;
using System.Collections.Generic;
using System.Linq;

namespace Hierarchy
{
    public static partial class HierarchyExtensions
    {
        public static bool All<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<IHierarchyNode<TData>, bool> predicate)
        {
            return Enumerable.All(hierarchyNodes.ToFlatNodeList(), predicate);
        }

        public static bool Any<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            return Enumerable.Any(hierarchyNodes.ToFlatNodeList());
        }

        public static bool Any<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<IHierarchyNode<TData>, bool> predicate)
        {
            return Enumerable.Any(hierarchyNodes.ToFlatNodeList(), predicate);
        }

        public static IEnumerable<IHierarchyNode<TData>> Append<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, IHierarchyNode<TData> element)
        {
            return Enumerable.Append(hierarchyNodes, element);
        }

        public static IEnumerable<IHierarchyNode<TData>> AsEnumerable<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes)
        {
            return Enumerable.AsEnumerable(hierarchyNodes.ToFlatNodeList());
        }

        #region Averaging

        public static decimal Average(this IEnumerable<IHierarchyNode<decimal>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static double Average(this IEnumerable<IHierarchyNode<double>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static double Average(this IEnumerable<IHierarchyNode<int>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static double Average(this IEnumerable<IHierarchyNode<long>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static float Average(this IEnumerable<IHierarchyNode<float>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static decimal? Average(this IEnumerable<IHierarchyNode<decimal?>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static double? Average(this IEnumerable<IHierarchyNode<double?>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static double? Average(this IEnumerable<IHierarchyNode<int?>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static double? Average(this IEnumerable<IHierarchyNode<long?>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static float? Average(this IEnumerable<IHierarchyNode<float?>> hierarchyNodes)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList());
        }

        public static decimal Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, decimal> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static double Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, double> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static double Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, int> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static double Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, long> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static float Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, float> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static decimal? Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, decimal?> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static double? Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, double?> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static double? Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, int?> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static double? Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, long?> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        public static float? Average<TData>(this IEnumerable<IHierarchyNode<TData>> hierarchyNodes, Func<TData, float?> selector)
        {
            return Enumerable.Average(hierarchyNodes.ToFlatDataList(), selector);
        }

        #endregion

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
