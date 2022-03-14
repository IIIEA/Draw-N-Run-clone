using UnityEngine;

public class SC_CursorTrail : MonoBehaviour
{
    [SerializeField] private DrawingHandler _drawer = null;

    public Color trailColor = new Color(1, 0, 0.38f);
    public float distanceFromCamera = 5;
    public float startWidth = 0.1f;
    public float endWidth = 0f;
    public float trailTime = 0.24f;

    Transform trailTransform = null;
    [SerializeField] Camera thisCamera = null;
    TrailRenderer _trail = null;

    void Start()
    {
        GameObject trailObj = new GameObject("Mouse Trail");
        trailObj.transform.SetParent(gameObject.transform);
        trailTransform = trailObj.transform;
        TrailRenderer trail = trailObj.AddComponent<TrailRenderer>();
        _trail = trail;
        trail.Clear();
        trail.time = -1f;
        MoveTrailToCursor(Input.mousePosition);
        trail.time = trailTime;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;
        trail.numCapVertices = 2;
        trail.sharedMaterial = new Material(Shader.Find("Unlit/Color"));
        trail.sharedMaterial.color = trailColor;
    }

    private void OnEnable()
    {
        _drawer.DrawedInPanel += DrawTrail;
    }

    private void OnDisable()
    {
        _drawer.DrawedInPanel -= DrawTrail;
    }

    void MoveTrailToCursor(Vector3 screenPosition)
    {
        if(_drawer.IsDrawing == false)
        {
            _trail.Clear();
        }

        trailTransform.position = thisCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
    }

    private void DrawTrail(bool arg)
    {
        if (arg)
        {
            MoveTrailToCursor(Input.mousePosition);
        }
    }
}