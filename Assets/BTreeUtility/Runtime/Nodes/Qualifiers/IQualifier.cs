namespace BTreeUtility.Nodes
{
    public interface IQualifier: INode
    {
        float Score(IAIContext context);
    }
}