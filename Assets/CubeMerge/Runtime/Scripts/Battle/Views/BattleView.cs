using System;
using UnityEngine;

public class BattleView : MonoBehaviour
{
    [SerializeField] private Vector2 _tilePositionOffset = new Vector2(-4, -4);
    [SerializeField] private Transform _darkSquare;
    [SerializeField] private Transform _lightSquare;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i % 2 != 0 || j % 2 != 0)
                    Instantiate(_darkSquare, new Vector3(i + _tilePositionOffset.x, 0.1f, j + _tilePositionOffset.y), _darkSquare.rotation, transform);
                else
                    Instantiate(_lightSquare, new Vector3(i + _tilePositionOffset.x, 0.1f, j + _tilePositionOffset.y), _lightSquare.rotation, transform);
            }
        }
    }

}
