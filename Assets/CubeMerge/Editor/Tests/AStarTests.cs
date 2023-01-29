using System.Collections.Generic;
using CubeMerge.Runtime.Scripts.Battle;
using NUnit.Framework;
using UnityEngine;

public class AStarTests
{
    private class TestCase
    {
        public MapArea Map;
        public Position Start;
        public Position Target;
        public Position FirstStepExpected;
    }

    private readonly TestCase[] _testCases = new[]
    {
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(8, 10)},
            Start = new Position(-2, -2),
            Target = new Position(4, 5),
            FirstStepExpected = new Position(-1, -1)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(10, 10)},
            Start = new Position(-5, 0),
            Target = new Position(5, 5),
            FirstStepExpected = new Position(-4, 1)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(5, 5)},
            Start = new Position(1, 2),
            Target = new Position(-2, -2),
            FirstStepExpected = new Position(0, 1)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(4, 6), 
                Obstacles = new List<AreaBounds>()
                {
                    new AreaBounds(new Position(-1,1),3, 1)
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
        var aStar = new AStar(testCase.Map, 1);

        var path = aStar.GetPath(testCase.Start, testCase.Target);

        Assert.AreEqual(testCase.FirstStepExpected, path.Pop());
        
        while (path.Count > 0)
            Debug.Log(path.Pop().ToString());

    }
}
