using System;
using System.Collections.Generic;
using System.Text;
using BTreeUtility.Nodes;

namespace BTreeUtility
{
    public static class NodeCollectionExtensions
    {
        public static ISelector GetRootSelector(this Dictionary<string, INode> nodes)
        {
            if (nodes == null || nodes.Count == 0)
                throw new ArgumentException($"Nodes missing!");
            
            using var nodesEnumerator = nodes.GetEnumerator();
            nodesEnumerator.MoveNext();
            var rootSelector = nodesEnumerator.Current.Value as ISelector;
            if (rootSelector == null)
                throw new ArgumentException($"First node is not {nameof(ISelector)}!");
            
            nodesEnumerator.Dispose();
        
            return rootSelector;
        }

        /// <summary>
        /// For debug
        /// </summary>
        public static string GetNodeNames(this List<INode> nodes, bool fullName = true)
        {
            if (nodes == null || nodes.Count == 0)
                return string.Empty;
            
            var result = new StringBuilder(nodes.Count);
            foreach (var node in nodes)
            {
                if (node == null) 
                    continue;

                var name = fullName ? node.GetType().FullName : node.GetType().Name;
                result.Append($"\n-> {name}");
            }
            return result.ToString();
        }
    }
}