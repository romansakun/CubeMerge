using System.Collections.Generic;

namespace BTreeUtility.Nodes
{
    public class FirstScoreSelector : ISelector
    {
        public INode Next { get; set; }
        public List<IQualifier> Qualifiers { get; } = new List<IQualifier>();

        public INode Select(IBTContext context)
        {
            for (var i = 0; i < Qualifiers.Count; i++)
            {
                var qualifier = Qualifiers[i];
                if (qualifier.Score(context) > 0)
                    return qualifier;
            }
            return Next;
        }
    }
}