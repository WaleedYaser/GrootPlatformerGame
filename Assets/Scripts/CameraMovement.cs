using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	public GameManager gameManager;
	public Text textSpeed;
	public Transform timerImage;

	private float speed;
	public float speedMultiple = .05f;

	private Vector3 targetPosition;
	public Transform target;

	private float distance;

	public float startLimmit = 7f;
	public float maxDistanceBeforeLose = 7f;

	private float timer;

	void Start()
	{
		timer = 0;
	}
	void LateUpdate () {
		distance = target.position.y - transform.position.y;

		if (target.position.y < startLimmit)
			return;
		
		if (distance < -maxDistanceBeforeLose)
		{
			gameManager.GameOver();
		}
		else if (distance > 1)
		{
			targetPosition = new Vector3(0, target.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, distance*Time.deltaTime);
		}
		else
		{
			targetPosition = new Vector3(0, transform.position.y + speed, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
		}
		
		timer += Time.deltaTime;
		textSpeed.text = "x " + (int)(1 + (Time.time) / 60);
		speed = (1 + (timer) / 60) * speedMultiple;
		timerImage.rotation = Quaternion.Euler(0, 0, -((timer) % 60) * 360 / 60f);
	}
}
