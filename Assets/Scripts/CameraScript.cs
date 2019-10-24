using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public QutJr jr;
	public QutJrJr jrjr;
	private Camera cam;

	private float zoomSpeed;

	void Start()
	{
		cam = GetComponent<Camera>();
	}
	void Update()
	{
		var maxPosistionX = jr.positionX;
		var minPosistionX = jr.positionX;
		if (jrjr.positionX > maxPosistionX)
		{
			maxPosistionX = jrjr.positionX;
		}
		if (jrjr.positionX < minPosistionX)
		{
			minPosistionX = jrjr.positionX;
		}

		if (maxPosistionX > 10 && maxPosistionX < 15 || minPosistionX < -10 && minPosistionX > -15)
		{
			cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 15, ref zoomSpeed, 0.5f);
		}

		if (maxPosistionX > 15 && maxPosistionX < 20 || minPosistionX < -15 && minPosistionX > -20)
		{
			cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 20, ref zoomSpeed, 0.5f);
		}

		if (maxPosistionX > 20 || minPosistionX < -20)
		{
			cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 25, ref zoomSpeed, 0.5f);
		}
	}
}
