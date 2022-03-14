using UnityEngine;

public class GridSpawnPoints : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private float _nodeSize;
    [SerializeField] private float _spacing;

    private Vector3 _normal = Vector3.up;

    public int TotalPlaces { get => _gridSize.x * _gridSize.y; }

    public Vector3 TotalSize()
    {
        Vector3 size = Vector3.zero;

        size.x = _gridSize.x * _nodeSize + (_gridSize.x - 1) * _spacing;
        size.y = 1;
        size.z = _gridSize.y * _nodeSize + (_gridSize.y - 1) * _spacing;

        return size;
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[_gridSize.x * _gridSize.y];

        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                Vector3 pos = EvaluateNormal(GetPosition(x, y));

                int index = y * _gridSize.x + x;
                positions[index] = pos;
            }
        }

        return positions;
    }

    private Vector3 GetPosition(int x, int y)
    {
        Vector2 xy = new Vector2(x, y);

        Vector2 centerOffset = Vector2.one * 0.5f;

        Vector2 gridOffset = new Vector2(-_gridSize.x * 0.5f, -_gridSize.y * 0.5f);

        Vector3 pos = gridOffset + xy + centerOffset;

        Vector3 position = pos * _nodeSize + pos * _spacing;

        return position;
    }

    private Vector3 EvaluateNormal(Vector3 xy)
    {
        return Quaternion.FromToRotation(Vector3.forward, _normal) * xy;
    }

    private Vector3[] GetCorners(Vector2 xy)
    {
        Vector3[] corners = new Vector3[4];

        Vector2 halfSize = Vector2.one * _nodeSize * 0.5f;

        corners[0] = EvaluateNormal(xy + new Vector2(-halfSize.x, -halfSize.y));
        corners[1] = EvaluateNormal(xy + new Vector2(-halfSize.x, halfSize.y));
        corners[2] = EvaluateNormal(xy + new Vector2(halfSize.x, halfSize.y));
        corners[3] = EvaluateNormal(xy + new Vector2(halfSize.x, -halfSize.y));

        return corners;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                Vector2 xy = GetPosition(x, y);

                Vector3 pos = EvaluateNormal(xy);
                Gizmos.DrawWireSphere(pos + origin, 0.01f);

                var corners = GetCorners(xy);

                for (int i = 0; i < corners.Length; i++)
                {
                    Gizmos.DrawLine(corners[i] + origin, corners[(i + 1) % corners.Length] + origin);
                }
            }
        }
    }
}
