using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public Transform player;

	[Header("Side Walls")]
	public GameObject wallsPrefab;
	public float currentWallY;
	public float wallTall = 11.5f;
	public float distanceBeforeSpawn = 10f;
	public int initialWalls = 6;
	public List<GameObject> wallPool;

	[Header("Platforms")]
	public GameObject blockPrefab;
	public float currentBlockY;
	public float distanceBetweenBlocks = 2f;
	public float distanceBeforeSpawnBlock = 10f;
	public int initBlocksLine = 10;
	public List<GameObject> blocksPool;

	private void Awake()
	{
		InitSideWalls();
		InitBlocks();	
	}

	private void Update()
	{
		if (currentWallY - player.position.y < distanceBeforeSpawn)
		{
			SpawnSideWall();
		}

		if(currentBlockY - player.position.y < distanceBeforeSpawnBlock)
		{
			SpawnBlocks();
		}
	}

	private void InitSideWalls()
	{
		for (int i = 0; i < initialWalls; ++i)
		{
			Vector2 pos = new Vector2(0, currentWallY);
			GameObject go = Instantiate(wallsPrefab, pos, Quaternion.identity, transform);
			wallPool.Add(go);
			currentWallY += wallTall;
		}
	}

	private void InitBlocks()
	{
		for (int i = 0; i < initBlocksLine; i++)
		{
			Vector2 pos = new Vector2(Random.Range(-5, 5), currentBlockY);
			GameObject go = Instantiate(blockPrefab, pos, Quaternion.identity, transform);
			blocksPool.Add(go);
			currentBlockY += distanceBetweenBlocks;
		}
	}

	private void SpawnSideWall()
	{
		wallPool[0].transform.position = new Vector2(0, currentWallY);
		currentWallY += wallTall;

		GameObject temp = wallPool[0];
		wallPool.RemoveAt(0);
		wallPool.Add(temp);
	}

	private void SpawnBlocks()
	{
		blocksPool[0].transform.position = new Vector2(Random.Range(-5, 5), currentBlockY);
		currentBlockY += distanceBetweenBlocks;

		GameObject temp = blocksPool[0];
		blocksPool.RemoveAt(0);
		blocksPool.Add(temp);
	}

}
