using System.Collections.Generic;
using BTreeUtility.Nodes;
using NUnit.Framework;
using BTreeUtility.Sample;

public class Tests
{
    // A Test behaves as an ordinary method
    [Test]
    public void WhenExecute_ThenCorrectBestAndWorstValues()
    {
        var context = new SampleAIContex();
        context.Values = new List<int>()
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7
        };
        //options:

        var options = new List<IOptionScorer<SampleAIContex, int>>()
        {
            new SampleOptionScorerOne(),
            new SampleOptionScorerTwo()
        };
        //actions:
        var action = new SampleActionWithOptions(options);
        
        action.Execute(context);
       
        Assert.AreEqual(6, context.BestValue);
        Assert.AreEqual(1, context.WorstValue);
    }
    
}
