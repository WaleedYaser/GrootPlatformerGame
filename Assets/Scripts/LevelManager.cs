using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public Transform player;
	public GameObject wallsPrefab;

	private float currentY;

	public float wallTall = 11.5f;
	public float distanceBeforeSpawn = 10f;
	public int initialWalls = 6;

	public List<GameObject> wallPool;

	private void Awake()
	{
		InitSideWalls();
	}

	private void Update()
	{
		if(currentY - player.position.y < distanceBeforeSpawn)
		{
			SpawnSideWall();
		}
	}

	private void InitSideWalls()
	{
		for(int i = 0; i < initialWalls; ++i)
		{
			Vector2 pos = new Vector2(0, currentY);
			GameObject go = Instantiate(wallsPrefab, pos, Quaternion.identity, transform);
			wallPool.Add(go);
			currentY += wallTall;
		}
	}

	private void SpawnSideWall()
	{
		wallPool[0].transform.position = new Vector2(0, currentY);
		currentY += wallTall;

		GameObject temp = wallPool[0];
		wallPool.RemoveAt(0);
		wallPool.Add(temp);
	}

}
