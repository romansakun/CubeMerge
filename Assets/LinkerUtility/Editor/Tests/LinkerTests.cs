using System.Collections;
using System.Collections.Generic;
using LinkerUtility.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LinkerTests
{
    // A Test behaves as an ordinary method

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
        for (int i = 1; i <= 5; i++) node.AddNode(new TestLinkerNode(100 + i));
        
        var subNode = new TestLinkerNode(200);
        for (int i = 1; i <= 3; i++) subNode.AddNode(new TestLinkerNode(200 + i));
        
        var subsubNode = new TestLinkerNode(300);
        for (int i = 1; i <= 4; i++) subsubNode.AddNode(new TestLinkerNode(300 + i));
        
        subNode.AddNode(subsubNode);
        for (int i = 5; i <= 9; i++) subNode.AddNode(new TestLinkerNode(200 + i));
        
        node.AddNode(subNode);
        for (int i = 6; i <= 9; i++) node.AddNode(new TestLinkerNode(100 + i));
        
        
        Debug.Log($"node id: {((TestLinkerNode)node).Id}");
        while (node.HasNext())
        {
            var n = node.Next();
            Debug.Log($"node id: {((TestLinkerNode)n).Id}");    
        }
    }

   
}
