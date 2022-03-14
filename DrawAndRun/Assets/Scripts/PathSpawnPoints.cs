using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathSpawnPoints
{
    public ICollection<Vector3> Points { get; }

    private List<Point> _points = new List<Point>();

    public PathSpawnPoints(ICollection<Vector3> points)
    {
        Points = points;

        float distance = GetLength(points.ToArray());

        float temp = 0;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 a = Points.ElementAt(i);
            Vector3 b = Points.ElementAt(i + 1);
            float percentage = temp / distance;

            Point point = new Point(a, percentage, temp);
            _points.Add(point);

            temp += (b - a).magnitude;
        }

        _points.Add(new Point(Points.ElementAt(Points.Count - 1), 1, distance));
    }

    public class Point
    {
        public Vector3 Position { get; }
        public float Percentage { get; }
        public float Length { get; }

        public Point(Vector3 position, float percentage, float length)
        {
            Position = position;
            Percentage = percentage;
            Length = length;
        }
    }

    private float GetLength(ICollection<Vector3> points)
    {
        float distance = 0;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 a = points.ElementAt(i);
            Vector3 b = points.ElementAt(i + 1);
            distance += (b - a).magnitude;
        }

        return distance;
    }

    public ICollection<Vector3> Subdivide(int count)
    {
        Vector3[] result = new Vector3[count];

        float stepSize = 1f / (count - 1);

        for (int i = 0; i < count; i++)
        {
            float t = i * stepSize;
            Vector3 evaluated = Evaluate(t);
            result[i] = evaluated;
        }

        return result;
    }

    public Vector3 Evaluate(float time)
    {
        time = Mathf.Clamp01(time);

        int prevIndex = 0, nextIndex = 0;

        for (int i = 0; i < _points.Count - 1; i++)
        {
            Point next = _points[i + 1];

            if (time <= next.Percentage)
            {
                prevIndex = i;
                nextIndex = i + 1;

                break;
            }
        }

        Point p1 = _points[prevIndex];
        Point p2 = _points[nextIndex];

        float totalTime = Mathf.InverseLerp(p1.Percentage, p2.Percentage, time);

        Vector3 result = Vector3.Lerp(p1.Position, p2.Position, time);

        return result;
    }
}
