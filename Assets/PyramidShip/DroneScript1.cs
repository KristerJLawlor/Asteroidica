using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript1 : MonoBehaviour
{
    //Attach script to enemy object
    public Rigidbody myRig;

    public float SpawnTimer = 10f;
    public GameObject DroneObject;

    public Vector3 SpawnLocation;
    public Vector3 PlayerLocation;

    public bool CanSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        //PlayerLocation = GameObject.Find("Main Camera").transform.position;
        //SpawnLocation = myRig.transform.position;
        SpawnLocation = new Vector3(myRig.transform.position.x + 10f, myRig.transform.position.y, myRig.transform.position.z);
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnLocation = new Vector3(myRig.transform.position.x, myRig.transform.position.y -5f, myRig.transform.position.z);
    }

    public IEnumerator Spawning()
    {
        while (CanSpawn == true)
        {
            yield return new WaitForSeconds(SpawnTimer);

            //Spawn the drone
            GameObject temp1 = Instantiate(DroneObject, SpawnLocation + new Vector3(0, -5, 0), Quaternion.identity);
            GameObject temp2 = Instantiate(DroneObject, SpawnLocation + new Vector3(5, 0, 0), Quaternion.identity);
            GameObject temp3 = Instantiate(DroneObject, SpawnLocation + new Vector3(-5, 0, 0), Quaternion.identity);


        }
    }
}
