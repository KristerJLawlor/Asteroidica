using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimalAsteroidSpawner : MonoBehaviour
{
    //Attach script to camera
    public float SpawnTimer = .5f;
    public GameObject RandomAsteroid;
    public GameObject RandomAsteroid1;
    public GameObject[] Asteroids;
    public Vector3 SpawnLocation;
    public Vector3 SpawnLocation1;
    public float XSpawn;
    public float YSpawn;
    public float ZSpawn;
    public float YSpawn1;
    public float ZSpawn1;
    public float AsteroidSpeed = 8f;
    public bool CanSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        SpawnLocation = new Vector3(XSpawn, YSpawn, ZSpawn);
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
        //Define the coordinates for the spawn points for both asteroids
        XSpawn = -200f;
        YSpawn = Random.Range(-70f, 0f);
        ZSpawn = Random.Range(-150f, 150f);
        SpawnLocation = new Vector3(XSpawn, YSpawn, ZSpawn);
        YSpawn1 = Random.Range(0f, 70f);
        ZSpawn1 = Random.Range(-150f, 150f);
        SpawnLocation1 = new Vector3(XSpawn, YSpawn1, ZSpawn1);

        // randomly get the asteroid prefab that will be spawned
        RandomAsteroid = Asteroids[Random.Range(0, 3)];
        RandomAsteroid1 = Asteroids[Random.Range(0, 3)];
    }

    public IEnumerator Spawning()
    {
        while (CanSpawn == true)
        {
            yield return new WaitForSeconds(SpawnTimer);

            //Spawn the two asteroids
            GameObject temp1 = Instantiate(RandomAsteroid, SpawnLocation, Quaternion.identity);

            GameObject temp2 = Instantiate(RandomAsteroid1, SpawnLocation1, Quaternion.identity);

        }

    }


}
