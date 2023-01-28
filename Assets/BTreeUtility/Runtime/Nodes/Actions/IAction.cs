namespace BTreeUtility.Nodes
{
    public interface IAction : INode
    {
        void Execute(IAIContext context);
    }
}