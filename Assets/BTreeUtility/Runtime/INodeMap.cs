using System.Collections.Generic;

namespace BTreeUtility
{
    public interface INodeMap
    {
        public List<int> Nodes { get; }
        public Dictionary<int, int> Connections { get; }
    }
}