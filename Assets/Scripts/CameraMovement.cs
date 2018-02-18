using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float speed = 1;
	public Vector3 targetPosition;
	public Transform target;
	public float distance;

	void LateUpdate () {
		distance = target.position.y - transform.position.y;
		if(distance > 1)
		{
			targetPosition = new Vector3(0, target.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, distance*Time.deltaTime);
		}
		else
		{
			targetPosition = new Vector3(0, transform.position.y + speed, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
		}
			
	}
}
