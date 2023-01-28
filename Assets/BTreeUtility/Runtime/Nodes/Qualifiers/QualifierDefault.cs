namespace BTreeUtility.Nodes
{
    public class QualifierDefault : IQualifier
    {
        public INode Next { get; set; } = new DefaultNode();
        
        private readonly float _score;

        public QualifierDefault(float score = 0)
        {
            _score = score;
        }

        public float Score(IAIContext context)
        {
            return _score;
        }
    }
}