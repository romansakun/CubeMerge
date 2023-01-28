using System.Collections.Generic;
using BTreeUtility;
using BTreeUtility.Nodes;
using NUnit.Framework;
using Sample;
using UnityEditor.VersionControl;

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
    public void WhenNotQualifierAfterSelector_ThenFirstNodeMustBeSelectorException ()
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

    [Test]
    public void WhenConnectionHasInfiniteLoop_ThenCircularConnectionException()
    {
        _map.SetNodes(new List<int>() {1});
        _map.SetConnections(new Dictionary<int, int>() {{1, 1}});
        _factory.CreatingFunc = nodeId => new FirstScoreSelector();

        var errorMessage = Assert.Catch(() => _factory.CreateNodes()).Message;
        StringAssert.Contains("Circular connection", errorMessage);
    }
}
