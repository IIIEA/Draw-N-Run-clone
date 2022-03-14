using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeControl : MonoBehaviour {


	public ParticleSystem _particle;
	public Camera _camera;
	public float mouseSensitivity = 0.2f; //порог чувствительности, чтобы не было реакций на незначительные движения или дрожания
	private List<Vector3> position;
	private bool isMove;
	private float curTime;

	void Start () 
	{
		Reset();
	}

	void Reset()
	{
		_particle.emissionRate = 0;
		position = new List<Vector3>();
		isMove = true;
		curTime = 0;
	}

	void Update () 
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(_camera.transform.position.z));
		Vector3 mouse = _camera.ScreenToWorldPoint(curScreenPoint);
		if(Input.GetMouseButton(0))
		{
			_particle.transform.position = mouse;
			_particle.emissionRate = 200;

			if(Mathf.Abs(Input.GetAxis("Mouse X")) > mouseSensitivity || Mathf.Abs(Input.GetAxis("Mouse Y")) > mouseSensitivity)
			{
				isMove = true;
				curTime = 0;
			}
			else if(Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y") == 0)
			{
				curTime += Time.deltaTime;
				if(curTime > 0.1f) //фильтр, чтобы не проскакивали нули
				{
					if(isMove)
					{
						isMove = false;
						position.Add(mouse); //добавление текущей позиции, если мышка не движется
					}
				}

			}
		}
	}
}
