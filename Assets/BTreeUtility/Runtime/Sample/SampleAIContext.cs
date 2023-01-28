using System.Collections.Generic;

namespace BTreeUtility.Sample
{
    public class SampleAIContex : IAIContext
    {
        public float DeltaTime { get; set; }
        public int BestValue { get; set; }
        public int WorstValue { get; set; }

        public bool ConditionOne = false;
        public bool ConditionTwo = false;

        public List<int> Values = new List<int>();
    }
}