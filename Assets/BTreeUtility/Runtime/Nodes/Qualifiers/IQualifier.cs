namespace BTreeUtility.Nodes
{
    public interface IQualifier: INode
    {
        float Score(IBTContext context);
    }
}