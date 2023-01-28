using System.Collections.Generic;

namespace BTreeUtility.Nodes
{
    public class HighScoreSelector : ISelector
    {
        public INode Next { get; set; } = new QualifierDefault();
        
        public List<IQualifier> Qualifiers { get; } = new List<IQualifier>();
        
        
        public INode Select(IBTContext context)
        {
            INode result = Next;

            var maxScore = 0f;
            for (int i = 0; i < Qualifiers.Count; i++)
            {
                var qualifier = Qualifiers[i];
                var qualifierScore = qualifier.Score(context);
                if (qualifierScore > maxScore)
                {
                    maxScore = qualifierScore;
                    result = qualifier;
                }
            }
            return result;
        }
    }
}