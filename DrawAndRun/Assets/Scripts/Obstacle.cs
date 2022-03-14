using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void Init(bool active)
    {
        Vector3 size = Vector3.one;
        size.y = active ? 1 : 0.1f;
        transform.localScale = size;
    }
}
