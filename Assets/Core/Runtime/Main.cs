using System;
using UnityEngine;

namespace Core.Runtime
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Initializer _initializer;
        
        private void Awake()
        {
        }

        private void Start()
        {
            _initializer.Initialize();
        }

        private void Update()
        {
            throw new NotImplementedException();
        }

        private void OnEnable()
        {
            throw new NotImplementedException();
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            throw new NotImplementedException();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            throw new NotImplementedException();
        }

        private void OnApplicationQuit()
        {
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }
}