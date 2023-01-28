using System;
using System.Collections.Generic;
using BTreeUtility.Nodes;

namespace BTreeUtility
{
    public abstract class NodesFactoryBase
    {
        private MapBase _map;
        
        public NodesFactoryBase(MapBase map)
        {
            _map = map;
        }
        
        public Dictionary<int, INode> CreateNodes()
        {
            var nodesCount = _map.Nodes.Count;
            var nodes = new Dictionary<int, INode>(nodesCount);

            var isFirstNode = true;
            ISelector currentSelector = null;
            foreach (var nodeId in _map.Nodes)
            {
                var node = CreateNode(nodeId);

                if (isFirstNode)
                {
                    if (node is not ISelector rootSelector)
                        throw new Exception($"First node must be the Selector!\nCheck your Map:\n{_map.GetType().FullName}");
                    
                    currentSelector = rootSelector;
                    nodes.Add(nodeId, rootSelector);
                    isFirstNode = false;
                    continue;
                }
                switch (node)
                {
                    case ISelector selector:
                        if (currentSelector.Qualifiers.Count == 0)
                            throw new Exception($"Selector must have at least one qualifier under itself!\nCheck your Map:\n{_map.GetType().FullName}");

                        currentSelector = selector;
                        nodes.Add(nodeId, currentSelector);
                        break;
                    
                    case IQualifier qualifier:
                        if (currentSelector == null)
                            throw new Exception($"Qualifiers must be under selectors!\nCheck order nodes in your Map:\n{_map.GetType().FullName}");
                        
                        currentSelector.Qualifiers.Add(qualifier);
                        nodes.Add(nodeId, qualifier);
                        break;
                    
                    case IAction action:
                        nodes.Add(nodeId, action);
                        break;
                }
            }

            ConnectNodes(nodes);
            
            return nodes;
        }
        
        private void ConnectNodes(Dictionary<int, INode> allNodes)
        {
            var connections = _map.Connections;
            foreach (var connection in connections)
            {
                if (!allNodes.ContainsKey(connection.Value))
                    throw new Exception($"Nodes in your map dont contains : [{connection.Value}]!");
                
                allNodes[connection.Key].Next = allNodes[connection.Value];
            }
        }

        public abstract INode CreateNode(int nodeId);
    }
}