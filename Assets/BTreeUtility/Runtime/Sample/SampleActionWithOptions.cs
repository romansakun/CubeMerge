using System.Collections.Generic;
using BTreeUtility.Nodes;

namespace BTreeUtility.Sample
{
    public class SampleActionWithOptions : ActionWithOptions<SampleAIContex, int>
    {
        public SampleActionWithOptions(List<IOptionScorer<SampleAIContex, int>> scorers) : base(scorers)
        {
        }

        protected override void Execute(SampleAIContex context)
        {
            context.BestValue = GetBest(context, context.Values);
            context.WorstValue = GetWorst(context, context.Values);
        }
    }
}