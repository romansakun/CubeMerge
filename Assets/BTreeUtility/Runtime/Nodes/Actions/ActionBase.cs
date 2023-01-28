namespace BTreeUtility.Nodes
{
    public abstract class ActionBase<T> : IAction where T: class, IBTContext
    {
        public INode Next { get; set; }

        public void Execute(IBTContext context)
        {
            Execute(context as T);
        }

        protected abstract void Execute(T context);
    }
}