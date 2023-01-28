namespace BTreeUtility.Nodes
{
    public interface IOptionScorer<in T, in O> where T: class, IAIContext
    {
        float Score(T context, O option);
    }
}