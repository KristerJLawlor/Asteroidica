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
    public void OnCollisionEnter(Collision collision)
    {
        GameObject.Destroy(this.gameObject);
       int pickupSpawn = Random.Range(1, 100);
        if (pickupSpawn % 10 == 0)
        {
            GameObject.Instantiate(pickup, this.GetComponent<AsteroidMovement>().AsteroidRig.position, this.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
