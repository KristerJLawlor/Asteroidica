using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class asteroidDamage : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject pickup;
    public GameObject ammo;
    public GameObject Portal;

    public string scenename;

    public int scorethreshold = 2500;
    public int score;
    public bool isDead = false;
    
    void Start()
    {
        scenename = SceneManager.GetActiveScene().name;
    }
    public void OnTriggerEnter(Collider c)
    {
        if (!isDead && c.gameObject.tag != "Portal")
        {
            isDead = true;
            StartCoroutine(DestroyMe());
            
            score += 100;
            GameObject.FindObjectOfType<CameraLook>().score+= score;
            //Assuning this is a laser
            GetComponent<AsteroidMovement>().Explode(this.transform.position);
        }

    }
    public IEnumerator DestroyMe()
    {
        int pickupSpawn = Random.Range(1, 100);
        int ammoSpawn = Random.Range(1, 100);
        if (pickupSpawn % 4 == 0)
        {
        GameObject.Instantiate(pickup, this.GetComponent<AsteroidMovement>().AsteroidRig.position, this.transform.rotation);
        }
        else if (ammoSpawn % 5 == 0)
        {
            GameObject.Instantiate(ammo, this.GetComponent<AsteroidMovement>().AsteroidRig.position, this.transform.rotation);
        }
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
        //Spawn portal to next level if score is above threshold
        if((scenename == "Level1 Intro" ||
            scenename == "Level2 Intro" || 
            scenename == "Level3 Intro") && score > scorethreshold  )
        {
            Instantiate(Portal, Vector3.zero, Quaternion.identity);
        }
    }
}
