using UnityEngine;

namespace BTreeUtility
{
    public class BtClientComponent : MonoBehaviour
    {
        private BTClient _client;
        private IBTContext _context;
        private float _executionLastCallTime;
        private float _executionDeltaTime;
        private float _executionTimer;

        private void Awake()
        {
            _executionLastCallTime = Time.realtimeSinceStartup;
            _executionDeltaTime = 0;
            _executionTimer = 0;
        }

        public void Init(BTClient client)
        {
            _client = client;
        }

        public void SetExecutionDeltaTime(float delta)
        {
            _executionDeltaTime = delta;
        }

        private void Update()
        {
            if (_client == null)
                return;
            
            if (_executionTimer <= 0)
            {
                _executionTimer = _executionDeltaTime;
                _context.DeltaTime = Time.realtimeSinceStartup - _executionLastCallTime;
                _executionLastCallTime = Time.realtimeSinceStartup;
                 
                _client.Execute();
            }
            _executionTimer -= Time.deltaTime;
        }
    }
}