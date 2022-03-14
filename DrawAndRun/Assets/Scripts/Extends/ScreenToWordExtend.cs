using UnityEngine;

public static class ScreenToWordExtend
{
    public static Vector3 ScreenToWorld(Vector3 screenpoint)
    {
        Plane xyPerpective = new Plane(Vector3.forward, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(screenpoint);
        xyPerpective.Raycast(ray, out float enter);
        Vector3 point = ray.GetPoint(enter);
        return point;
    }
}
