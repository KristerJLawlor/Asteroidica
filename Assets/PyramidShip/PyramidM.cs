using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidM : MonoBehaviour
{
    public Rigidbody myRig;
    public GameObject AsteroidField;
    public int DPS = 5;
    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        myRig.angularVelocity = new Vector3(0, .5f, 0);
        myRig.velocity = new Vector3(0, -5, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        //make pyramid go up and down
        if(myRig.transform.position.y <= -10)
        {
            myRig.velocity = new Vector3(0, 1, 0);
        }
        if (myRig.transform.position.y >= 10)
        {
            myRig.velocity = new Vector3(0, -1, 0);
        }

        //keep the pyramid rotating
        myRig.angularVelocity = new Vector3(0, .5f, 0);


        AsteroidField.transform.RotateAround(myRig.transform.position, new Vector3(0, 1, 0), DPS * Time.deltaTime);
    }
}
