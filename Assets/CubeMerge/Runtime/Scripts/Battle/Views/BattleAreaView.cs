using CubeMerge.Runtime.Scripts.Battle;
using MVVM.Runtime;
using UnityEngine;

public class BattleAreaView : View<BattleAreaViewModel>
{
    [SerializeField] private Vector2 _tilePositionOffset = new Vector2(-4, -4);
    [SerializeField] private Transform _darkSquare;
    [SerializeField] private Transform _lightSquare;
    
    [SerializeField] private Transform _source;
    [SerializeField] private Transform _target;
    

    // private void Start()
    // {
    //     Init();
    // }

    public void ShowTiles(float width, float height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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

    public override void Init(BattleAreaViewModel viewModel)
    {
        UpdateViewModel(viewModel);
        AddListener(_viewModel.BattleArea, OnBattleArea);


    }

    private void OnBattleArea()
    {
        var battleArea = _viewModel.BattleArea.Value;

        ShowTiles(battleArea.Bounds.Width, battleArea.Bounds.Heigth);
        
        InvokeRepeating(nameof(ShowPath), 0, .5f);
    }

    //[ContextMenu("ShowPath")]
    public void ShowPath()
    {
        var path = _viewModel.GetPath(_source.position, _target.position);

        foreach (var pos in path)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = pos;
            Destroy(sphere, .5f);
        }
    }
}
