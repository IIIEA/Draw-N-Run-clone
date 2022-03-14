using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private bool _isActive = false;

    private void Update()
    {
        if (_isActive)
        {
            transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }
    }
}
