using System.Collections.Generic;
using BTreeUtility.Nodes;

namespace BTreeUtility.Nodes
{
    public class FirstScoreSelector : ISelector
    {
        public INode Next { get; set; } = new DefaultNode();
        public List<IQualifier> Qualifiers { get; } = new List<IQualifier>();

        public INode Select(IAIContext context)
        {
            for (var i = 0; i < Qualifiers.Count; i++)
            {
                var qualifier = Qualifiers[i];
                if (qualifier.Score(context) > 0)
                    return qualifier;
            }
            return null;
        }
    }
}