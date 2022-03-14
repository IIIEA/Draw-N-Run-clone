using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class DrawingHandler : MonoBehaviour
{
    public event Action OnPathGenerated = null;
    public UnityAction<bool> DrawedInPanel = null;

    [SerializeField] private RectTransform _rectTrransform = null;
    [SerializeField] private float _distance = 10;

    private List<Vector2> _points = new List<Vector2>();

    private bool _isDrawing = false;
    private int _pointsToCreatePath = 2;


    public bool IsDrawing { get => _isDrawing; }
    public List<Vector2> Points => _points;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _points.Clear();

            _isDrawing = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isDrawing = false;

            if (_points.Count >= _pointsToCreatePath)
            {
                OnPathGenerated?.Invoke();
            }
        }

        if (_isDrawing)
        {
            Drawing(Input.mousePosition);
        }
    }

    private Vector2 GetPositionOnScreen(Vector3 screenPosition)
    {
        Vector3[] corners = new Vector3[4];
        _rectTrransform.GetWorldCorners(corners);

        Vector2 horizontal = new Vector2(corners[0].x, corners[3].x);
        Vector2 vertical = new Vector2(corners[0].y, corners[1].y);

        Vector2 position = new Vector2(
            Mathf.InverseLerp(horizontal.x, horizontal.y, screenPosition.x),
            Mathf.InverseLerp(vertical.x, vertical.y, screenPosition.y)
        );

        return position;
    }

    private void Drawing(Vector3 screenPosition)
    {
        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(_rectTrransform, screenPosition);

        DrawedInPanel?.Invoke(isInside);

        if (!isInside)
        {
            return;
        }

        Vector3 pos = GetPositionOnScreen(screenPosition);

        if (_points.Count == 0)
        {
            _points.Add(pos);
        }
        else
        {
            Vector3 last = _points[_points.Count - 1];
            if (Vector3.Distance(pos, last) > _distance * 0.01f)
            {
                _points.Add(pos);
            }
        }
    }
}
