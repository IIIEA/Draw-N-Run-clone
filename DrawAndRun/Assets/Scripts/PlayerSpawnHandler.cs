using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridSpawnPoints))]
public class PlayerSpawnHandler : MonoBehaviour
{
    [SerializeField] private int _countPlayers;
    [SerializeField] private DrawingHandler _drawingHandler = null;
    [SerializeField] private Spawner _spawner = null;

    private GridSpawnPoints _grid;

    public int Count => _countPlayers;

    private void Start()
    {
        _grid = GetComponent<GridSpawnPoints>();
        _countPlayers = _grid.TotalPlaces;
        SpawnInitial();
    }

    private void OnEnable()
    {
        _drawingHandler.OnPathGenerated += RecalculatePositions;
    }

    private void OnDisable()
    {
        _drawingHandler.OnPathGenerated -= RecalculatePositions;
    }

    private void SpawnInitial()
    {
       
        Vector3[] positions = _grid.GetPositions();

        for (int i = 0; i < positions.Length; i++)
        {
            _spawner.SpawnAt(positions[i]);
        }
    }

    private void RecalculatePositions()
    {
        _spawner.RecalculatePosition(_countPlayers);
    }
}
