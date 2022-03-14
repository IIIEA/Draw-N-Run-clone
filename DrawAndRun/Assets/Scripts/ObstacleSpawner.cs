using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridSpawnPoints))]
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private bool m_IsActive = false;

    [SerializeField] private Obstacle _obstaclePrefab = null;
    [SerializeField] private Obstacle[] _obstacles = null;

    private GridSpawnPoints _grid;
    public bool IsAcive => m_IsActive;

    private void Start()
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        _grid = GetComponent<GridSpawnPoints>();

        GetComponent<BoxCollider>().size = _grid.TotalSize();

        Vector3[] positions = _grid.GetPositions();

        _obstacles = new Obstacle[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            Obstacle obstacle = Instantiate(_obstaclePrefab, transform);
            obstacle.transform.localPosition = positions[i];
            _obstacles[i] = obstacle;
        }

        UpdateState();
    }

    private void UpdateState()
    {
        for (int i = 0; i < _obstacles.Length; i++)
        {
            _obstacles[i].Init(m_IsActive);
        }
    }

    private void OnValidate()
    {
        UpdateState();
    }
}
