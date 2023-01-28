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

        public float Score(IBTContext context)
        {
            return _score;
        }
    }
}