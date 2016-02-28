using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject monster;
    public float xmin;
    public float xmax;
    public float zmin;
    public float zmax;
    public int i;
	// Use this for initialization
	void Start () {
        for (int c = 0; c < i; c++)
        {
            SpawnWaves();
        }
	}
	
	// Update is called once per frame
	void SpawnWaves () {
        Vector3 spawnPosition = new Vector3(Random.Range(xmin, xmax), 57.0f, Random.Range(zmin, zmax));
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(monster, spawnPosition, spawnRotation);
	}
}
