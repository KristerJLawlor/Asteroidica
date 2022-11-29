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
        Debug.Log("Blarg");
        if (collision.gameObject.tag != "laser")
        {
            
            HP--;
        }
        if (collision.gameObject.tag == "Drone")
        {
            collision.GetComponent<DroneMovement>().Explode(collision.GetComponent<DroneMovement>().myRig.position);
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHP(HP);
    }
}
