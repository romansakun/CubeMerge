using System.Collections.Generic;
using MVVM.Runtime.ReactiveProperties;
using UnityEngine;

namespace CubeMerge.Runtime.Scripts.Battle
{
    public class BattleAreaViewModel : IViewModel
    {
        private ReactiveProperty<MapArea> _battleArea;
        public IReactiveProperty<MapArea> BattleArea => _battleArea;

        private AStar _aStar;
        
        
        public BattleAreaViewModel()
        {
            var bounds = new AreaBounds(new Position(0, 0), 8, 10);
            var map = new MapArea(bounds);
            _aStar = new AStar(map, .1f);
            
            _battleArea = new ReactiveProperty<MapArea>(map);
        }

        public List<Vector3> GetPath(Vector3 start, Vector3 target)
        {
            var startPos = new Position(start.x, start.z);
            var targetPos = new Position(target.x, target.z);
            var path = _aStar.GetPath(startPos, targetPos);

            var result = new List<Vector3>(path.Count);
            while (path.Count > 0)
            {
                var pos = path.Pop();
                result.Add(new Vector3(pos.X, 0 , pos.Y));
            }
            return result;
        }
        
        public void Dispose()
        {
            _battleArea.Dispose();    
        }
    }
}