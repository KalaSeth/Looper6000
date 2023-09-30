// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// WorldGeneratorx
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneratorx : MonoBehaviour
{
	public float spawnRange;

	public float amountToSpawn;

	private Vector3 spawnPoint;

	public GameObject asteroid;

	public float startSafeRange;

	private List<GameObject> objectsToPlace = new List<GameObject>();

	private void Start()
	{

		for (int i = 0; (float)i < amountToSpawn; i++)
		{
			PickSpawnPoint();
			while (Vector3.Distance(spawnPoint, Vector3.zero) < startSafeRange)
			{
				PickSpawnPoint();
			}
			objectsToPlace.Add(Object.Instantiate(asteroid, spawnPoint, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
			objectsToPlace[i].transform.parent = transform;
		}
		asteroid.SetActive(false);
	}

	private void Update()
	{
	}

	public void PickSpawnPoint()
	{
		spawnPoint = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		if (spawnPoint.magnitude > 1f)
		{
			spawnPoint.Normalize();
		}
		spawnPoint *= spawnRange;
	}
}
