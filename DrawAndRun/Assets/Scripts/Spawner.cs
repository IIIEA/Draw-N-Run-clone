using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _player = null;
    [Space]
    [SerializeField] private Vector3 _center = Vector3.zero;
    [SerializeField] private Vector2 _size = Vector2.one;

    [SerializeField] private DrawingHandler _drawingHandler = null;

    private Vector3[] _spawnPoints = new Vector3[0];

    private List<Player> _players = new List<Player>();

    private void Start()
    {
        _center = gameObject.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(_spawnPoints[i], 0.1f);
        }

        Gizmos.color = Color.green;

        Vector3 size = new Vector3(_size.x, 0, _size.y);
        Gizmos.DrawWireCube(gameObject.transform.position, size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ConvertPoint(new Vector2(0, 0)), 0.1f);
        Gizmos.DrawWireSphere(ConvertPoint(new Vector2(0, 1)), 0.1f);
        Gizmos.DrawWireSphere(ConvertPoint(new Vector2(1, 1)), 0.1f);
        Gizmos.DrawWireSphere(ConvertPoint(new Vector2(1, 0)), 0.1f);

        Gizmos.color = Color.black;
        Vector3[] corners = new Vector3[4];
        GetCorners(corners);
        for (int i = 0; i < corners.Length; i++)
        {
            Gizmos.DrawWireSphere(corners[i], 0.2f);
        }
    }

    public void RecalculatePosition(int count)
    {
        List<Vector3> points = _drawingHandler.Points
            .Select(v2 => ConvertPoint(v2))
            .ToList();

        _spawnPoints = new PathSpawnPoints(points).Subdivide(count).ToArray();

        for (int i = 0; i < _players.Count; i++)
        {
            Player dude = _players[i];
            dude.transform.position = _spawnPoints[i];
        }
    }

    public void SpawnAt(Vector3 position)
    {
        Player dude = Instantiate(_player, position, Quaternion.identity, transform);

        _players.Add(dude);
    }

    private void GetCorners(Vector3[] corners)
    {
        // bl, tl, tr, br;

        Vector2 halfSize = _size * 0.5f;

        corners[0] = _center + new Vector3(-halfSize.x, 0, -halfSize.y);
        corners[1] = _center + new Vector3(-halfSize.x, 0, halfSize.y);
        corners[2] = _center + new Vector3(halfSize.x, 0, halfSize.y);
        corners[3] = _center + new Vector3(halfSize.x, 0, -halfSize.y);
    }

    private Vector3 ConvertPoint(Vector2 point01)
    {
        Vector3[] corners = new Vector3[4];
        GetCorners(corners);

        Vector3 point = Vector3.zero;
        point.x = Mathf.Lerp(corners[0].x, corners[3].x, point01.x);
        point.z = Mathf.Lerp(corners[0].z, corners[1].z, point01.y);
        return point;
    }
}
