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

            for (var i = 0; i < Obstacles.Count; i++)
            {
                if (Obstacles[i].IsInBounds(position))
                    return false;
            }
            return true;
        }
    }
}