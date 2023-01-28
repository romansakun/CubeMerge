using System.Collections.Generic;

namespace BTreeUtility.Nodes
{
    public interface ISelector: INode
    {
        List<IQualifier> Qualifiers { get; }
        
        INode Select(IBTContext context);
    }
}