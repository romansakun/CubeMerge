using LinkerUtility.Runtime;
using NUnit.Framework;
using UnityEngine;

public class LinkerTests
{
    class TestLinkerNode : LinkerNode
    {
        public int Id;

        public TestLinkerNode(int id)
        {
            Id = id;
        }
    }
    
    [Test]
    public void LinkerTestsSimplePasses()
    {
        var node = new TestLinkerNode(100);
        for (int i = 1; i <= 5; i++) node.AddNext(new TestLinkerNode(100 + i));
        
        var subNode = new TestLinkerNode(200);
        for (int i = 1; i <= 3; i++) subNode.AddNext(new TestLinkerNode(200 + i));
        
        var subsubNode = new TestLinkerNode(300);
        for (int i = 1; i <= 4; i++) subsubNode.AddNext(new TestLinkerNode(300 + i));
        
        subNode.AddNext(subsubNode);
        for (int i = 5; i <= 9; i++) subNode.AddNext(new TestLinkerNode(200 + i));
        
        node.AddNext(subNode);
        for (int i = 6; i <= 9; i++) node.AddNext(new TestLinkerNode(100 + i));
        
        
        Debug.Log($"node id: {((TestLinkerNode)node).Id}");
        while (node.HasNext())
        {
            Debug.Log($"node id: {((TestLinkerNode)node.Next()).Id}");    
        }
    }

}
