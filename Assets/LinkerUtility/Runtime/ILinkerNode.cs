namespace LinkerUtility.Runtime
{
    public interface ILinkerNode
    {
        ILinkerNode Next();
        bool HasNext();
    }
}