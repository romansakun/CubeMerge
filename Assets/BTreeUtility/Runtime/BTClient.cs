using System;
using System.Collections.Generic;
using BTreeUtility.Nodes;

namespace BTreeUtility
{
    public class BTClient
    {
        private readonly List<INode> _nodeChain = new List<INode>();
        private readonly IBTContext _context;
        private readonly ISelector _rootSelector;

        public BTClient(ISelector rootSelector, IBTContext context)
        {
            _rootSelector = rootSelector ?? throw new ArgumentException($"{nameof(rootSelector)} is null!");
            _context = context ?? throw new ArgumentException($"{nameof(context)} is null!");
        }

        public void Execute()
        {
            _nodeChain.Clear();
            var currentNode = _rootSelector.Select(_context);
            while (currentNode != null)
            {
                _nodeChain.Add(currentNode);
                if (currentNode == _rootSelector)
                    throw new Exception($"AI logic looped!\n{_nodeChain.GetNodeNames()}");

                switch (currentNode)
                {
                    case ISelector selector:
                        currentNode = selector.Select(_context);
                        break;
                    case IQualifier qualifier:
                        currentNode = qualifier.Next;
                        break;
                    case IAction action:
                        action.Execute(_context);
                        currentNode = action.Next;
                        break;
                    default:
                        throw new Exception("Only selector or action can be next node!");
                }
            }
            
#if UNITY_EDITOR && BT_CLIENT_DEBUG
            UnityEngine.Debug.Log($"Node chain:{_nodeChain.GetNodeNames(false)}");
#endif
        }
    }
}