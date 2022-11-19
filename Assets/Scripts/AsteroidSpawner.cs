using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //Attach script to camera
    public float SpawnTimer = .5f;
    public GameObject RandomAsteroid;
    public GameObject[] Asteroids;
    public Vector3 SpawnLocation;
    public float XSpawn;
    public float YSpawn;
    public float ZSpawn;
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
        XSpawn = -40f;
        YSpawn = Random.Range(-10f, 40f);
        ZSpawn = Random.Range(-10f, 10f);
        SpawnLocation = new Vector3(XSpawn, YSpawn, ZSpawn);
        AsteroidSpeed = Random.Range(0.5f, 10f);

        RandomAsteroid = Asteroids[Random.Range(0, 3)];

    }

    void Spawn()
    {
        //StartCoroutine(Waiting());

    }

    public IEnumerator Spawning()
    {
        while (CanSpawn == true)
        {
            yield return new WaitForSeconds(SpawnTimer);
            GameObject temp = Instantiate(RandomAsteroid, SpawnLocation, Quaternion.identity);
            temp.GetComponent<Rigidbody>().velocity = new Vector3(AsteroidSpeed, 0, 0);
            //yield return new WaitForSeconds(SpawnTimer);
            //CanSpawn = true;

        }

    }


}
