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
                if (j % 2 == 0)
                    InstantiateTile(i % 2 == 0 ? _darkSquare : _lightSquare, i, j);
                else
                    InstantiateTile(i % 2 != 0 ? _darkSquare : _lightSquare, i, j);
            }
        }
    }

    private void InstantiateTile(Transform tile, int i, int j)
    {
        var pos = new Vector3(i + _tilePositionOffset.x, 0.1f, j + _tilePositionOffset.y);
        Instantiate(tile, pos, tile.rotation, transform);
    }
}
