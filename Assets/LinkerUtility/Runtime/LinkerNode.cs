using System.Collections.Generic;

namespace LinkerUtility.Runtime
{
    public abstract class LinkerNode : ILinkerNode
    {
        private readonly List<ILinkerNode> _nextNodes = new List<ILinkerNode>();
        private readonly Stack<ILinkerNode> _stack = new Stack<ILinkerNode>();

        public List<ILinkerNode> NextNodes => _nextNodes;
        

        public void AddNext(ILinkerNode node)
        {
            _nextNodes.Add(node); 
            _stack.Clear();
            for (int i = _nextNodes.Count - 1; i >= 0 ; i--)
                _stack.Push(_nextNodes[i]);
        }
     
        public ILinkerNode Next()
        {
            if (!HasNext())
                return null;

            var node = _stack.Pop();

            int nodeIndex = node.NextNodes.Count - 1;
            while (nodeIndex >= 0)
            {
                var last = node.NextNodes[nodeIndex];
                _stack.Push(last);
                nodeIndex--;
            }
            
            return node;
        }

        public bool HasNext()
        {
            return _stack.Count > 0;
        }
    }
}
