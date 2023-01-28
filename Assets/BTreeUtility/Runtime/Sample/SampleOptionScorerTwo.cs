using BTreeUtility.Nodes;

namespace BTreeUtility.Sample
{
    public class SampleOptionScorerTwo : IOptionScorer<SampleAIContex, int>
    {
        public float Score(SampleAIContex context, int option)
        {
            if (option % 2 == 0)
                return 5;
            else if (option % 3 == 0)
                return 1;
            else
                return 0;
        }
    }
}