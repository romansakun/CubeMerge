namespace BTreeUtility.Nodes
{
    public abstract class QualifierBase<T> :IQualifier where T: class, IAIContext
    {
        public INode Next { get; set; }

        protected QualifierBase()
        {
            Next = new DefaultNode();
        }

        protected QualifierBase(INode nextNode)
        {
            Next = nextNode;
        }

        public float Score(IAIContext context)
        {
            return Score(context as T);
        }

        protected abstract float Score(T context);
    }
}