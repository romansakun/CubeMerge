using System.Collections.Generic;

namespace Core.Runtime.PathFinder
{
    public class PathFinder
    {
        private readonly int _directionsLenght;
        private readonly Point[] _directions = {
            new Point(-1, 0),
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, 0),
        };

        public PathFinder()
        {
            _directionsLenght = _directions.Length;
        }

        public List<Point> GetPath(int[,] map, Point source, Point target)
        {
            var path = new List<Point>();
            var pathMap = new PathMap(map);
            var pathQueue = new Queue<Point>();
            var pathComplete = false;
            pathQueue.Enqueue(source);
            while (!pathComplete && pathQueue.Count > 0)
            {
                var point = pathQueue.Dequeue();
                var node = pathMap.Nodes[point.X, point.Y];
                if (node == null)
                    continue;

                var directionIndex = GetStartDirectionIndex(pathMap, point, target);
                for (int i = 0; i < _directionsLenght; i++)
                {
                    var index = (directionIndex + i) % _directionsLenght;
                    var newDirectionPoint = point + _directions[index];
                    if (source == newDirectionPoint)
                        continue;
                    
                    if (!pathMap.Contains(newDirectionPoint))
                        continue;

                    var nextNode = pathMap.Nodes[newDirectionPoint.X, newDirectionPoint.Y];
                    if (nextNode == null || nextNode.Previous != null)
                        continue;
                    
                    nextNode.Previous = node;
                    pathQueue.Enqueue(newDirectionPoint);
                    if (newDirectionPoint == target)
                        pathComplete = true;
                }
            }

            if (pathComplete)
            {
                var node = pathMap.Nodes[target.X, target.Y];
                while(node.Previous != null)
                {
                    path.Add(node.Point);
                    node = node.Previous;
                }
                path.Reverse();
            }

            return path;
        }

        private int GetStartDirectionIndex(PathMap pathMap, Point point, Point target)
        {
            var dirIndex = 0;
            var isBestDirPointNotSelected = true;
            var bestDirPoint = point;
            for (int i = 0; i < _directionsLenght; i++)
            {
                var dirPoint = point + _directions[i];
                if (!pathMap.Contains(dirPoint))
                    continue;

                if (isBestDirPointNotSelected)
                {
                    bestDirPoint = dirPoint;
                    isBestDirPointNotSelected = false;
                    continue;
                }
                
                if (Point.SumDiff(target, dirPoint) > Point.SumDiff(target, bestDirPoint))
                    continue;

                var bestDiff = (target - bestDirPoint).Abs();
                var diff = (target - dirPoint).Abs();
                if (bestDiff.Max() < diff.Max())
                    continue;
                
                dirIndex = i;
                bestDirPoint = dirPoint;
            }
            return dirIndex;
        }
    }
}