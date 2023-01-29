using System.Collections;
using System.Collections.Generic;
using System.Xml;
using CubeMerge.Runtime.Scripts.Battle;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;
using Bounds = CubeMerge.Runtime.Scripts.Battle.Bounds;

public class AStarTests
{
    private class TestCase
    {
        public MapArea Map;
        public Position Start;
        public Position Target;
        public Position FirstStepExpected;
    }

    private TestCase[] _testCases = new[]
    {
        new TestCase()
        {
            Map = new MapArea() {Bounds = new Bounds(8, 10)},
            Start = new Position(-2, -2),
            Target = new Position(4, 5),
            FirstStepExpected = new Position(-1, -1)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new Bounds(10, 10)},
            Start = new Position(-5, 0),
            Target = new Position(5, 5),
            FirstStepExpected = new Position(-4, 1)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new Bounds(5, 5)},
            Start = new Position(1, 2),
            Target = new Position(-2, -2),
            FirstStepExpected = new Position(0, 1)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new Bounds(4, 6), 
                Obstacles = new List<Bounds>()
                {
                    new Bounds(new Position(-1,1),3, 1)
                }},
            Start = new Position(-1, -2),
            Target = new Position(1, 3),
            FirstStepExpected = new Position(0, -1)
        }
    };

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void WhenGetPath_ThenExpectedFirsStep(int testCaseId)
    {
        var testCase = _testCases[testCaseId];
        var aStar = new AStar(testCase.Map);

        var path = aStar.GetPath(testCase.Start, testCase.Target);

        Assert.AreEqual(testCase.FirstStepExpected, path.Pop());
        
        while (path.Count > 0)
            Debug.Log(path.Pop().ToString());
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator AStarTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
