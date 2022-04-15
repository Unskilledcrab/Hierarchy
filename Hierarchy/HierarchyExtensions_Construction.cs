using System;
using System.Collections.Generic;
using System.Linq;

namespace Hierarchy
{
    public static partial class HierarchyExtensions
    {
        /// <summary>
        /// Pass in a flat list with id selectors to get back a list of hierarchy nodes in the form of a hierarchy
        /// </summary>
        /// <typeparam name="THierarchyModel">The hierarchy node model that will be used</typeparam>
        /// <typeparam name="TData">The data type of the flat list</typeparam>
        /// <typeparam name="TKey">The data type of the keys for the items id and parent id</typeparam>
        /// <param name="flatList">The flat list that will be transformed to a hierarchy</param>
        /// <param name="idSelector">Returns the unique identifier for this item</param>
        /// <param name="parentIdSelector">Returns the parent id for this item</param>
        /// <returns>A hierarchical model for each item wrapped in a hierarchy node to be able to traverse the hierarchy</returns>
        private static IEnumerable<THierarchyModel> ToHierarchyInternal<THierarchyModel, TData, TKey>(this IEnumerable<TData> flatList, Func<TData, TKey> idSelector, Func<TData, TKey> parentIdSelector)
            where THierarchyModel : IHierarchyNode<TData>, new()
        {
            var lookup = flatList.Where(item => item is not null && idSelector(item) is not null)
                                                            .Select(f => new THierarchyModel { Data = f })
                                                            .ToDictionary(h => idSelector(h.Data));

            if (lookup is null || lookup.Count == 0)
            {
                yield break;
            }

            foreach (var item in lookup.Values)
            {
                var parentId = parentIdSelector(item.Data);
                if (parentId is null || !lookup.TryGetValue(parentId, out var parent))
                {
                    yield return item;
                    continue;
                }

                parent.Children.Add(item);
                item.Parent = parent;
            }
        }

        public static List<THierarchyModel> ToHierarchy<THierarchyModel, TData, TKey>(this IEnumerable<TData> flatList, Func<TData, TKey> idSelector, Func<TData, TKey> parentIdSelector)
            where THierarchyModel : IHierarchyNode<TData>, new()
        {
            // NOTE: If we do not call ToList() here then the children will not be present in future calls.
            //       We must do at least one iteration through the call to make sure that the parent child
            //       relationship is built
            return flatList.ToHierarchyInternal<THierarchyModel, TData, TKey>(idSelector, parentIdSelector).ToList();
        }

        public static List<HierarchyNode<TData>> ToHierarchy<TData, TKey>(this IEnumerable<TData> flatList, Func<TData, TKey> idSelector, Func<TData, TKey> parentIdSelector)
        {
            return flatList.ToHierarchy<HierarchyNode<TData>, TData, TKey>(idSelector, parentIdSelector);
        }

        public static List<HierarchyNode<TData>> ToHierarchy<TData, TKey>(this IEnumerable<TData> flatList)
            where TData : IHierarchy<TKey>
        {
            return flatList.ToHierarchy<HierarchyNode<TData>, TData, TKey>(f => f.Id, f => f.ParentId);
        }
    }
}
