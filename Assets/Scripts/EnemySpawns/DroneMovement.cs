using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DroneMovement : MonoBehaviour
{
    public Rigidbody myRig;
    public Rigidbody playerRig;
    public GameObject DroneObject;
   

    public Vector3 PlayerLocation;
    public Vector3 DroneLocation;
    public Vector3 Direction;

    public int HP = 5;
    public int MaxHP = 5;

    public float Speed = 5f;

    public ParticleSystem Explosion;

    Quaternion DroneRotation;

    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        playerRig = GameObject.Find("Main Camera").GetComponent<Rigidbody>();
        PlayerLocation = playerRig.transform.position;
        DroneLocation = myRig.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocation = playerRig.transform.position;
        DroneLocation = myRig.transform.position;
        //myRig.MovePosition(PlayerLocation);

        /*myRig.velocity = new Vector3(PlayerLocation.x - DroneLocation.x, 
                                        PlayerLocation.y - DroneLocation.y, 
                                        PlayerLocation.z - DroneLocation.y).normalized * Speed;
        */

        //myRig.MovePosition(PlayerLocation);

        Direction = (PlayerLocation - DroneLocation).normalized;

        myRig.MovePosition(DroneLocation + Direction * Speed * Time.deltaTime);

        if (Direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(Direction),
                Time.deltaTime * 10
            );
        }

        }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Missile" || collision.gameObject.tag=="Player")
        {
            HP -= 5;
        }
        else 
        {
            HP--;
        }
            if (HP <= 0)
        {
            GameObject.FindObjectOfType<CameraLook>().score += 100;
            Explode(myRig.position);
            Destroy(this.gameObject);
            
        }
        
        
    }

    /*public void OnCollisionStay(Collision collision)
    {
        if (HP <= 0)
        {
            Explode(myRig.position);
            Destroy(this.gameObject);
        }
        else
        {
            HP--;
        }
    }
    */

    public void Explode(Vector3 position)
    {
        Instantiate(Explosion, position, Quaternion.identity);
    }
}
