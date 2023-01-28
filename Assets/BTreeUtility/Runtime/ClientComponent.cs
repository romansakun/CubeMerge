//#define AI_DEBUG

using System;
using System.Collections.Generic;
using BTreeUtility.Nodes;
using UnityEngine;

namespace BTreeUtility
{
    public class ClientComponent
    {
        private readonly ISelector _rootSelector;
        private readonly IAIContext _context;
        private readonly List<INode> _nodeChain;

        private float _executionLastCallTime;

        public ClientComponent(ISelector rootSelector, IAIContext context)
        {
            _rootSelector = rootSelector ?? throw new ArgumentException($"{nameof(rootSelector)} is null!");
            _context = context ?? throw new ArgumentException($"{nameof(context)} is null!");
            
            _executionLastCallTime = Time.realtimeSinceStartup;
            
            _nodeChain = new List<INode>(15);
        }

        public void Execute()
        {
            _context.DeltaTime =  Time.realtimeSinceStartup - _executionLastCallTime;
            _executionLastCallTime = Time.realtimeSinceStartup;

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
                        currentNode = selector.Select(_context) ?? selector.Next;
                        break;
                    case IAction action:
                        action.Execute(_context);
                        currentNode = action.Next;
                        break;
                    default:
                        currentNode = currentNode.Next;
                        break;
                }
            }
            
#if AI_DEBUG && UNITY_EDITOR
            Debug.Log($"Node chain:{_nodeChain.GetNodeNames(false)}");
#endif
        }
    }
}