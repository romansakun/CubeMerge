using System.Collections.Generic;

namespace LinkerUtility.Runtime
{
    public class LinkerNode : ILinkerNode
    {
        private readonly Stack<ILinkerNode> _buffStack = new Stack<ILinkerNode>();
        private readonly List<ILinkerNode> _nodes = new List<ILinkerNode>();
        private readonly Stack<ILinkerNode> _stack = new Stack<ILinkerNode>();

        public void AddNode(ILinkerNode node)
        {
            _nodes.Add(node); 
            _stack.Clear();
            for (int i = _nodes.Count - 1; i >= 0 ; i--)
                _stack.Push(_nodes[i]);
        }
        
        public void RemoveNode(ILinkerNode node)
        {
            _nodes.Remove(node);
            _stack.Clear();
            for (int i = _nodes.Count - 1; i >= 0 ; i--)
                _stack.Push(_nodes[i]);
        }

        public ILinkerNode Next()
        {
            if (!HasNext())
                return null;

            var node = _stack.Pop();
            
            _buffStack.Clear();
            if (node.HasNext())
                _buffStack.Push(node.Next());

            while (_buffStack.Count > 0)
                _stack.Push(_buffStack.Pop());
            
            return node;
        }

        public bool HasNext()
        {
            return _stack.Count != 0;
        }
    }
}
