using System;
using System.Collections.Generic;
using System.Linq;

namespace Hierarchy
{
    public static partial class GraphExtensions
    {
        /// <summary>
        /// Pass in a flat list with id selectors to get back a list of hierarchy nodes in the form of a hierarchy
        /// </summary>
        /// <typeparam name="TGraphModel">The hierarchy node model that will be used</typeparam>
        /// <typeparam name="TData">The data type of the flat list</typeparam>
        /// <typeparam name="TKey">The data type of the keys for the items id and parent id</typeparam>
        /// <param name="flatList">The flat list that will be transformed to a hierarchy</param>
        /// <param name="idSelector">Returns the unique identifier for this item</param>
        /// <param name="parentIdsSelector">Returns the parent id for this item</param>
        /// <returns>A hierarchical model for each item wrapped in a hierarchy node to be able to traverse the hierarchy</returns>
        private static IEnumerable<TGraphModel> ToGraphInternal<TGraphModel, TData, TKey>(this IEnumerable<TData> flatList, Func<TData, TKey> idSelector, Func<TData, IEnumerable<TKey>> parentIdsSelector, TKey rootId = default)
            where TGraphModel : IGraphNode<TData>, new()
        {
            var lookup = flatList.Where(item => item is not null && idSelector(item) is not null)
                                                            .Select(f => new TGraphModel { Data = f })
                                                            .ToDictionary(h => idSelector(h.Data));

            if (lookup is null || lookup.Count == 0)
            {
                yield break;
            }

            foreach (var item in lookup.Values)
            {
                var parentIds = parentIdsSelector(item.Data);
                if (parentIds is not null)
                {
                    foreach (var parentId in parentIds)
                    {
                        if (parentId is null || !lookup.TryGetValue(parentId, out var parent) || parentId.Equals(rootId))
                        {
                            continue;
                        }

                        parent.Children.Add(item);
                        item.Parents.Add(parent);
                    }
                }
                yield return item;
            }
        }

        public static List<TGraphModel> ToGraph<TGraphModel, TData, TKey>(this IEnumerable<TData> flatList, Func<TData, TKey> idSelector, Func<TData, IEnumerable<TKey>> parentIdsSelector, TKey rootId = default)
            where TGraphModel : IGraphNode<TData>, new()
        {
            // NOTE: If we do not call ToList() here then the children will not be present in future calls.
            //       We must do at least one iteration through the call to make sure that the parent child
            //       relationship is built
            return flatList.ToGraphInternal<TGraphModel, TData, TKey>(idSelector, parentIdsSelector, rootId).ToList();
        }

        public static List<GraphNode<TData>> ToGraph<TData, TKey>(this IEnumerable<TData> flatList, Func<TData, TKey> idSelector, Func<TData, IEnumerable<TKey>> parentIdsSelector, TKey rootId = default)
        {
            return flatList.ToGraph<GraphNode<TData>, TData, TKey>(idSelector, parentIdsSelector, rootId);
        }
    }
}
