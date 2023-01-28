using System;
using System.Collections.Generic;

namespace BTreeUtility.Nodes
{
    public abstract class NodeFactoryBase
    {
        private INodeMap _nodeMap;
        
        public NodeFactoryBase(INodeMap nodeMap)
        {
            _nodeMap = nodeMap;
        }
        
        public Dictionary<int, INode> CreateNodes()
        {
            if (_nodeMap.Nodes == null || _nodeMap.Nodes.Count == 0)
                throw new Exception($"Nodes missing!\nCheck your Map:\n{_nodeMap.GetType().FullName}");
            
            var nodesCount = _nodeMap.Nodes.Count;
            var nodes = new Dictionary<int, INode>(nodesCount);

            var isFirstNode = true;
            ISelector currentSelector = null;
            foreach (var nodeId in _nodeMap.Nodes)
            {
                var node = CreateNode(nodeId);

                if (isFirstNode)
                {
                    if (node is not ISelector rootSelector)
                        throw new Exception($"First node must be the Selector!\nCheck your Map:\n{_nodeMap.GetType().FullName}");
                    
                    currentSelector = rootSelector;
                    nodes.Add(nodeId, rootSelector);
                    isFirstNode = false;
                    continue;
                }
                switch (node)
                {
                    case ISelector selector:
                        if (currentSelector.Qualifiers.Count == 0)
                            throw new Exception($"Selector must have at least one qualifier under itself!\nCheck your Map:\n{_nodeMap.GetType().FullName}");

                        currentSelector = selector;
                        nodes.Add(nodeId, currentSelector);
                        break;
                    
                    case IQualifier qualifier:
                        if (currentSelector == null)
                            throw new Exception($"Qualifiers must be under selectors!\nCheck order nodes in your Map:\n{_nodeMap.GetType().FullName}");
                        
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
            var connections = _nodeMap.Connections;
            if (connections == null || connections.Count == 0)
                throw new Exception($"Connections missing!\nCheck your Map:\n{_nodeMap.GetType().FullName}");

            foreach (var pair in connections)
            {
                var sourceKey = pair.Key;
                var currentKey = pair.Value;
                var attemptCount = allNodes.Count;
                while (connections.ContainsKey(currentKey) && attemptCount > 0)
                {
                    if (currentKey == sourceKey)
                        throw new Exception($"Circular connection!\nCheck your Map:\n{_nodeMap.GetType().FullName}");
                    
                    currentKey = connections[currentKey];
                    attemptCount--;
                }
                
                if (!allNodes.ContainsKey(pair.Value))
                    throw new Exception($"Nodes in your map dont contains : [{pair.Value}]!\nCheck your Map:\n{_nodeMap.GetType().FullName}");
                
                allNodes[pair.Key].Next = allNodes[pair.Value];
            }
        }

        protected abstract INode CreateNode(int nodeId);
    }
}