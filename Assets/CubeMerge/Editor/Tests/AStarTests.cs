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
    }

    private readonly TestCase[] _testCases = new[]
    {
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(8, 10)},
            Start = new Position(-2, -2),
            Target = new Position(4, 5)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(10, 10)},
            Start = new Position(-5, 0),
            Target = new Position(5, 5)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(5, 5)},
            Start = new Position(1, 2),
            Target = new Position(-2, -2)
        },
        new TestCase()
        {
            Map = new MapArea() {Bounds = new AreaBounds(4, 6), 
                Obstacles = new List<AreaBounds>()
                {
                    new AreaBounds(new Position(-1,1),3, 1)
                }},
            Start = new Position(-1, -2),
            Target = new Position(1, 3)
        }
    };

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, .5f)]
    [TestCase(2, .75f)]
    [TestCase(3, .25f)]
    public void WhenGetPath_ThenExpectedFirsStep(int testCaseId, float step)
    {
        var testCase = _testCases[testCaseId];
        var aStar = new AStar(testCase.Map, step);

        var path = aStar.GetPath(testCase.Start, testCase.Target);

        var pos = new Position();
        while (path.Count > 0)
        {
            pos = path.Pop();
            Debug.Log(pos.ToString());
        }
        Assert.IsTrue(Position.SqrDistance(testCase.Target, pos) < step * step);
    }
}
