using System.Collections.Generic;
using BTreeUtility.Nodes;
using BTreeUtility.Nodes;
using BTreeUtility.Nodes;

namespace BTreeUtility.Sample
{
    public class SampleClientAI
    {
        public SampleAIContex Context;
        public ClientComponent AiClient;

        public void Init(SampleAIContex clientContext)
        {
            Context = clientContext;
            
            //options:
            var options = new List<IOptionScorer<SampleAIContex, int>>()
            {
                new SampleOptionScorerOne(),
                new SampleOptionScorerTwo()
            };
            
            //actions:
            var action = new SampleActionWithOptions(options);
        
            //qualifiers:
            var defaultQualifier = new QualifierDefault(1f);
            defaultQualifier.Next = action;

            //selectors:
            var rootSelector = new FirstScoreSelector();
            rootSelector.Qualifiers.Add(defaultQualifier);
            
            AiClient = new ClientComponent(rootSelector, Context);
        }
        
        

    }
}
