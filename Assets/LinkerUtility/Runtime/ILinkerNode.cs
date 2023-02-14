using System.Collections.Generic;

namespace LinkerUtility.Runtime
{
    public interface ILinkerNode
    {
        List<ILinkerNode> NextNodes { get; }
        ILinkerNode Next();
        bool HasNext();
    }
}