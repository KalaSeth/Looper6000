// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// WorldGeneratorx
using System.Collections;
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
		Spawnthem();
	}

	private void Update()
	{
		if (gameObject.transform.childCount < 20)
		{
			Spawnthem();
		}
	}

	public void Spawnthem()
	{
		for (int i = 0; (float)i < amountToSpawn; i++)
		{
			PickSpawnPoint();
			while (Vector3.Distance(spawnPoint, Vector3.zero) < startSafeRange)
			{
				PickSpawnPoint();
			}
			objectsToPlace.Add(Instantiate(asteroid, spawnPoint, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)), gameObject.transform));
			//objectsToPlace[i].transform.parent = transform;
		}
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
