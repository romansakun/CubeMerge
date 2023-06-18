using System.Collections.Generic;
using Core.Runtime.PathFinder;
using MVVM.Runtime.ReactiveProperties;
using UnityEngine;

namespace CubeMerge.Runtime.Scripts.Battle
{
    public class BattleAreaViewModel : IViewModel
    {
        public EmptyProperty StartPathFinding;

        private int[,] _battleArea = new int[10, 10];
        
        public BattleAreaViewModel()
        {
            StartPathFinding = new EmptyProperty();
        }
        
        public List<Vector3> GetPath(Vector3 start, Vector3 target)
        {
            var pathFinder = new PathFinder();
            var path = pathFinder.GetPath(_battleArea, new Point(start.z, start.x), new Point(target.z, target.x));
            var result = new List<Vector3>();
            foreach (var pos in path)
            {
                result.Add(new Vector3(pos.Y, 0 , pos.X));
            }
            return result;
        }

        public void Dispose()
        {
            StartPathFinding.Dispose();
        }

        public void AddObstacles(Transform[] obstacles)
        {
            _battleArea = new int[10, 10];
            
            foreach (var obstacle in obstacles)
            {
                _battleArea[(int)obstacle.position.z, (int)obstacle.position.x] = 1;
            }
        }
    }
}