using Core.Runtime.PathFinder;
using NUnit.Framework;

namespace Core.Editor.Tests
{
    public class PathFinderTests
    {
        [Test]
        public void Test()
        {
            var map = new int[3, 3]
            {
                {0, 0, 0},
                {0, 0, 0},
                {0, 0, 0}
            };
            var pathFinder = new PathFinder();

            var path = pathFinder.GetPath(map, new Point(2, 2), new Point(0, 0));
            
            Assert.IsTrue(path.Count == 4);
        }

        [Test]
        public void Test1()
        {
            var map = new int[3, 3]
            {
                {0, 0, 0},
                {0, 1, 0},
                {0, 0, 0}
            };
            var pathFinder = new PathFinder();

            var path = pathFinder.GetPath(map, new Point(0, 0), new Point(2, 2));
            
            Assert.IsTrue(path.Count == 4);
        }
        
        
        [Test]
        public void Test2()
        {
            var map = new int[3, 3]
            {
                {0, 0, 0},
                {0, 1, 1},
                {0, 0, 0}
            };
            var pathFinder = new PathFinder();

            var path = pathFinder.GetPath(map, new Point(2, 2), new Point(0, 0));
            
            Assert.IsTrue(path.Count == 4);
        }

        [Test]
        public void WhenSourceEqualTarget()
        {
            var map = new int[3, 3]
            {
                {0, 0, 0},
                {0, 1, 1},
                {0, 0, 0}
            };
            var pathFinder = new PathFinder();

            var path = pathFinder.GetPath(map, new Point(0, 0), new Point(0, 0));
            
            Assert.IsTrue(path.Count == 0);
        }
        
    }
}