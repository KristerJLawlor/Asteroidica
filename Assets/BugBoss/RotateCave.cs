using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCave : MonoBehaviour
{
    public Rigidbody myRig;
    public GameObject AsteroidField;
    public int DPS = 10;
    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidField.transform.RotateAround(myRig.transform.position, new Vector3(1, 0, 0), DPS * Time.deltaTime);
    }
}
