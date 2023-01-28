using BTreeUtility.Nodes;

namespace BTreeUtility.Sample
{
    public class SampleOptionScorerOne : IOptionScorer<SampleAIContex, int>
    {
        public float Score(SampleAIContex context, int option)
        {
            if (option % 2 == 0)
                return option;
            else
                return 0;
        }
    }
}