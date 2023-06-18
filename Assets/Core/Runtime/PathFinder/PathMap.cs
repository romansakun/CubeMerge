namespace Core.Runtime.PathFinder
{
    public class PathMap
    {
        public PathNode[,] Nodes;

        public PathMap(int[,] map)
        {
            Nodes = new PathNode[map.GetLength(0), map.GetLength(1)];
            for (var i = 0; i < map.GetLength(0); i++)
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 0)
                    Nodes[i, j] = new PathNode {Point =  new Point(i, j)};
                else
                    Nodes[i, j] = null;
            }
        }

        public bool Contains(Point point)
        {
            if (point.X < 0 || point.Y < 0)
                return false;
            
            if (point.X >= Nodes.GetLength(0) || point.Y >= Nodes.GetLength(1))
                return false;

            return true;
        } 
    }
}