using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
