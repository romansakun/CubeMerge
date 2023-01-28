namespace BTreeUtility.Nodes
{
    public abstract class ActionBase<T> : IAction where T: class, IAIContext
    {
        public INode Next { get; set; }

        public void Execute(IAIContext context)
        {
            Execute(context as T);
        }

        protected abstract void Execute(T context);
    }
}