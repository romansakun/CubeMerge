using System.Collections.Generic;

namespace LinkerUtility.Runtime
{
    public class LinkerNode : ILinkerNode
    {
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
            
            var bufStack = new Stack<ILinkerNode>();
            while (node.HasNext())
                bufStack.Push(node.Next());

            while (bufStack.Count > 0)
                _stack.Push(bufStack.Pop());
            
            return node;
        }

        public bool HasNext()
        {
            return _stack.Count != 0;
        }
    }
}
