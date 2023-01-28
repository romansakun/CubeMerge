using System;
using System.Collections.Generic;
using BTreeUtility;
using BTreeUtility.Nodes;

namespace Sample
{
    public class SampleContext : IBTContext
    {
        public float DeltaTime { get; set; }
        
        public string Message;
        public float Number;
        public int Count;
    }
    
    public class SampleQualifier : QualifierBase<SampleContext>
    {
        public Func<SampleContext, float> ScoreFunc = context => 0;
        
        protected override float Score(SampleContext context)
        {
            return ScoreFunc(context);
        }
    }   
    
    public class SampleAction: ActionBase<SampleContext>
    {
        public Action<SampleContext> Action;
        
        protected override void Execute(SampleContext context)
        {
            Action(context);
        }
    }

    public class SampleOption : IOptionScorer<SampleContext, string>
    {
        public Func<SampleContext, float> ScoreFunc = context => 0;
        
        public float Score(SampleContext context, string option)
        {
            return ScoreFunc(context);
        }
    }

    public class SampleActionWithOptions: ActionWithOptions<SampleContext, string>
    {
        public List<string> stringsToEvaluate = new List<string>();

        public SampleActionWithOptions(List<IOptionScorer<SampleContext, string>> scorers) : base(scorers)
        {
        }

        protected override void Execute(SampleContext context)
        {
            //GetBest(context, stringsToEvaluate);
        }
    }
    
    public class SampleNodeMap : INodeMap
    {
        private List<int> _nodes;
        private Dictionary<int, int> _connections;
        public List<int> Nodes => _nodes;
        public Dictionary<int, int> Connections => _connections;

        public void SetNodes(List<int> nodes) => _nodes = nodes;
        public void SetConnections(Dictionary<int, int> connections) => _connections = connections;
    }
    
    public class SampleNodeFactory : NodeFactoryBase
    {
        public Func<int, INode> CreatingFunc = id => throw new Exception("no nodes");

        public SampleNodeFactory(INodeMap nodeMap) : base(nodeMap)
        {
        }

        protected override INode CreateNode(int nodeId)
        {
            return CreatingFunc(nodeId);
        }
    }
}