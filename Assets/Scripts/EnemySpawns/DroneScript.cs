using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    //Attach script to enemy object
    public Rigidbody myRig;

    public float SpawnTimer = 5f;
    public GameObject DroneObject;

    public Vector3 SpawnLocation;
    public Vector3 PlayerLocation;

    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        PlayerLocation = GameObject.Find("Main Camera").transform.position;
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Spawning()
    {
        yield return new WaitForSeconds(SpawnTimer);
    }
}
