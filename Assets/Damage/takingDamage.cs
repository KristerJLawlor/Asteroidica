using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takingDamage : MonoBehaviour
{
    public int HP = 20;
    public int maxHP = 20;
    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "laser" && 
            collision.gameObject.tag != "Drop" && 
            collision.gameObject.tag != "Ammo" &&
            collision.gameObject.tag != "Portal")
        {
            if (collision.gameObject.tag == "EnemyLaser")
            {
                HP -= 5;
            }
            else
            {
                HP -= 2;
            }
            
        }
        if (collision.gameObject.tag == "Drone")
        {
            collision.GetComponent<DroneMovement>().Explode(collision.GetComponent<DroneMovement>().myRig.position);
            Destroy(collision.gameObject);
            HP -= 5;
        }
        if (collision.gameObject.tag == "Ammo")
        {
            this.GetComponent<CameraLook>().ammoCount += 3;
        }
        if (collision.gameObject.tag == "Drop")
        {
            this.GetComponent<CameraLook>().currency += 100;
            GameObject.Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Portal")
        {
            //DEFINE THE CHARACTER MOVING TO THE NEXT STAGE OR HOME MAP
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHP(HP);
    }
}
