using System.Collections.Generic;

namespace BTreeUtility
{
    public abstract class MapBase
    {
        public List<int> Nodes;
        public Dictionary<int, int> Connections;
    }
}