using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takingDamage : MonoBehaviour
{
    public int HP = 20;
    public int maxHP = 20;
    public int lifeCount=1;
    public HealthBar healthbar;
    public LivesScript lifeCounter;
    public SceneChanger GoToNext;
    int oldResources;
    // Start is called before the first frame update
    void Start()
    {
        oldResources = PlayerPrefs.GetInt("Resource", 0);
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
            GameObject.Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Drop")
        {
            this.GetComponent<CameraLook>().currency += 100;
            oldResources += 100;
            PlayerPrefs.SetInt("Resource",oldResources);
            GameObject.Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Portal")
        {
            GoToNext.GoToNextScene();
        }
        if (HP <= 0 && lifeCount > 0)
        {
            lifeCount--;
            HP = maxHP;
        }
        if (HP <= 0 && lifeCount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            GoToNext.switchScenesLevel();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHP(HP);
        lifeCounter.ChangeLives(lifeCount);
    }
}
