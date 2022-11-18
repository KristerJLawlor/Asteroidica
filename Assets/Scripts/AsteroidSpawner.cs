using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float SpawnTimer = .5f;
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject Asteroid3;
    public GameObject[] Asteroids;
    public Vector3 SpawnLocation;
    public int XSpawn = -10;
    public int YSpawn = 0;
    public int ZSpawn = 0;
    public float AsteroidSpeed = 2f;
    public bool CanSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        SpawnLocation = new Vector3(XSpawn, YSpawn, ZSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        YSpawn = Random.Range(-10, 10);
        ZSpawn = Random.Range(-30, 30);
        SpawnLocation = new Vector3(XSpawn, YSpawn, ZSpawn);
        AsteroidSpeed = Random.Range(0.5f, 5f);
        if (CanSpawn) 
        {
            Spawn();
            CanSpawn = false;

        }

    }

    void Spawn()
    {
        StartCoroutine(Waiting());

    }

    public IEnumerator Waiting()
    {
        //yield return new WaitForSeconds(SpawnTimer);
        GameObject temp = GameObject.Instantiate(Asteroid1, SpawnLocation, Quaternion.identity);
        temp.GetComponent<Rigidbody>().velocity = new Vector3(AsteroidSpeed, 0, 0);
        yield return new WaitForSeconds(SpawnTimer);
        CanSpawn = true;

    }


}
