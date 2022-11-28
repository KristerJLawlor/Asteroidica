using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidDamage : MonoBehaviour
{

    // Start is called before the first frame update
    GameObject pickup;
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider c)
    {

        StartCoroutine(DestroyMe());
        int pickupSpawn = Random.Range(1, 100);
        //Assuning this is a laser
        GetComponent<AsteroidMovement>().Explode(this.transform.position);
        if (pickupSpawn % 10 == 0)
        {
            //GameObject.Instantiate(pickup, this.GetComponent<AsteroidMovement>().AsteroidRig.position, this.transform.rotation);
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
