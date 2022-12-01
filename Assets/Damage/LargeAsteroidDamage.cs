using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LargeAsteroidDamage : MonoBehaviour
{
    public int HP = 3;
    public int MaxHP = 3;
    public GameObject pickup;
    public GameObject ammo;
    public GameObject Cam;
    public int score;
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider c)
    {

        if (HP <= 0 || c.gameObject.tag=="Missile")
        {
            StartCoroutine(DestroyMe());
            int pickupSpawn = Random.Range(1, 100);
            int ammoSpawn = Random.Range(1, 100);
            score += 100;
            Cam.GetComponent<CameraLook>().score = score;
            //Assuning this is a laser
            GetComponent<AsteroidMovement>().Explode(this.transform.position);
            //if (pickupSpawn % 4 == 0)
            //{
            GameObject.Instantiate(pickup, this.GetComponent<AsteroidMovement>().AsteroidRig.position, this.transform.rotation);
            //}
            //else if (ammoSpawn % 5 == 0)
            //{
            //    GameObject.Instantiate(ammo, this.GetComponent<AsteroidMovement>().AsteroidRig.position, this.transform.rotation);
            //}
        }
        else 
        {
            HP--;
        }
    }
    public IEnumerator DestroyMe()
    {
        GetComponent<AsteroidMovement>().Explode(this.transform.position+this.transform.up*3);
        GetComponent<AsteroidMovement>().Explode(this.transform.position-this.transform.right * 3);
        GetComponent<AsteroidMovement>().Explode(this.transform.position+this.transform.right * 3);
        GetComponent<AsteroidMovement>().Explode(this.transform.position);
        GetComponent<AsteroidMovement>().Explode(this.transform.position-this.transform.up * 3);
        GetComponent<AsteroidMovement>().Explode(this.transform.position - this.transform.forward * 3);
        GetComponent<AsteroidMovement>().Explode(this.transform.position + this.transform.forward * 3);
        yield return new WaitForSeconds(.75f);
        GameObject.Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
       
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }
}
