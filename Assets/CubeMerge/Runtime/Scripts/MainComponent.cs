using CubeMerge.Runtime.Scripts;
using UnityEngine;

namespace CubeMerge.Runtime
{
    public class MainComponent : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _sceneViews;

        private Main _main;
        private ProjectContext _context;
        
        private void Awake()
        {
            _context = new ProjectContext();   
            _context.SceneViews = _sceneViews;
            _main = new Main();
        }

        private void Start()
        {
            _main.Init(_context);
        }

        private void Update()
        {
            if (Time.frameCount % 20 == 0)
                _main.Execute();
        }
    }
}