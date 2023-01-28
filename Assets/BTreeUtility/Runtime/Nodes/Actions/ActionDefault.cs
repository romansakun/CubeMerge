using UnityEngine;

namespace BTreeUtility.Nodes
{
    public class ActionDefault : IAction
    {
        public INode Next { get; set; }

        public void Execute(IBTContext context)
        {
            Debug.Log("DefaultAction.Execute()");
        }
    }
}