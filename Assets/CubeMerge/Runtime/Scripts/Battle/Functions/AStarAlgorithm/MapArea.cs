using System.Collections.Generic;

namespace CubeMerge.Runtime.Scripts.Battle
{
    public class MapArea
    {
        public AreaBounds Bounds;
        public List<AreaBounds> Obstacles;

        public MapArea()
        {
            Bounds = new AreaBounds(new Position(0, 0), 8, 10);
            Obstacles = new List<AreaBounds>();
        }
        
        public MapArea(AreaBounds bounds )
        {
            Bounds = bounds;
            Obstacles = new List<AreaBounds>();
        }
        
        public bool IsFreePosition(Position position)
        {
            if (!Bounds.IsInBounds(position))
                return false;

            if (Obstacles == null || Obstacles.Count == 0)
                return true;
            
            foreach (var obstacle in Obstacles)
            {
                if (obstacle.IsInBounds(position))
                    return false;
            }
            return true;
        }
    }
}