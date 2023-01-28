using System.Collections.Generic;
using BTreeUtility;
using BTreeUtility.Nodes;
using NUnit.Framework;
using Sample;

public class Tests
{
    private SampleNodeMap _map;
    private SampleNodeFactory _factory;
    private SampleContext _context;
    
    [SetUp]
    public void SetUp()
    {
        _map = new SampleNodeMap();
        _factory = new SampleNodeFactory(_map);
        _context = new SampleContext();
    }
    
    [Test]
    public void WhenMapNodesNull_ThenNodesMissingException ()
    {
        var errorMessage = Assert.Catch(()=>_factory.CreateNodes()).Message;
        StringAssert.Contains("Nodes missing", errorMessage);
    }
    
    [Test]
    public void WhenFirstNodeNotSelector_ThenFirstNodeMustBeSelectorException ()
    {
        _map.SetNodes(new List<int>() {1});
        _factory.CreatingFunc = nodeId => new SampleAction();

        var errorMessage = Assert.Catch(()=>_factory.CreateNodes()).Message;
        StringAssert.Contains("First node must be the Selector", errorMessage);
    }  
    
    [Test]
    public void WhenConnectionsNull_ThenConnectionsMissingException ()
    {
        _map.SetNodes(new List<int>() {1});
        _factory.CreatingFunc = nodeId => new FirstScoreSelector();

        var errorMessage = Assert.Catch(()=>_factory.CreateNodes()).Message;
        StringAssert.Contains("Connections missing", errorMessage);
    }
    
    
    [Test]
    public void WhenConnectionsHaveExtraNode_ThenNodeMissingException ()
    {
        _map.SetNodes(new List<int>() {1});
        _map.SetConnections(new Dictionary<int, int>(){{1, 2}});
        _factory.CreatingFunc = nodeId => new FirstScoreSelector();

        var errorMessage = Assert.Catch(()=>_factory.CreateNodes()).Message;
        StringAssert.Contains("Nodes in your map dont contains", errorMessage);
    }

    [Test]
    public void WhenConnectionKeyEqualsValue_ThenCircularConnectionException()
    {
        _map.SetNodes(new List<int>() {1});
        _map.SetConnections(new Dictionary<int, int>() {{1, 1}});
        _factory.CreatingFunc = nodeId => new FirstScoreSelector();

        var errorMessage = Assert.Catch(() => _factory.CreateNodes()).Message;
        StringAssert.Contains("Circular connection", errorMessage);
    }

    [Test]
    public void WhenConnectionHasInfiniteLoop_ThenCircularConnectionException()
    {
        _map.SetNodes(new List<int>() {1, 2, 3});
        _map.SetConnections(new Dictionary<int, int>() {{1, 2}, {2, 3}, {3, 2}});
        _factory.CreatingFunc = nodeId => nodeId == 1 ? new FirstScoreSelector() : new SampleAction();

        var errorMessage = Assert.Catch(() => _factory.CreateNodes()).Message;
        StringAssert.Contains("Circular connection", errorMessage);
    }

    [Test]
    public void WhenActionExecuted ()
    {
        _map.SetNodes(new List<int>() {1, 2});
        _map.SetConnections(new Dictionary<int, int>(){{1, 2}});
        _factory.CreatingFunc = nodeId => {
            switch (nodeId)
            {
                case 1: return new FirstScoreSelector();
                case 2: return new SampleAction(){Action = c => c.Number = 5};
                default: return null;
            }
        };
        var nodes = _factory.CreateNodes();
        var client = new BTClient(nodes.GetRootSelector(), _context);
        client.Execute();
        
        Assert.AreEqual(5, _context.Number);    
    }
    
    //todo test extensions for AllScorers
    [Test]
    public void WhenActionWithOptionsExecuted ()
    {
        _map.SetNodes(new List<int>() {1, 2});
        _map.SetConnections(new Dictionary<int, int>(){{1, 2}});
        var options = new List<IOptionScorer<SampleContext, string>>()
        {
            new SampleOption() {ScoreFunc = str => str.Contains("lol") ? 10 : 1},
            new SampleOption() {ScoreFunc = str => str.Contains("kek") ? 5 : 10}
        };
        var actionWithOption = new SampleActionWithOptions(options) {
            stringsToEvaluate = new List<string>() {
                "lol!", "!kek", "ololo kek", "alala"
            }
        };
        _factory.CreatingFunc = id => id == 1 ? new FirstScoreSelector() : actionWithOption;
        var nodes = _factory.CreateNodes();
        var client = new BTClient(nodes.GetRootSelector(), _context);
        client.Execute();
        
        
        //Assert.AreEqual("lol!", _context.AllScorers);    
        Assert.AreEqual("lol!", _context.Best);    
        Assert.AreEqual("!kek", _context.Worst);    
    }
}
