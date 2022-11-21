using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestructionVFX : MonoBehaviour
{
    public ParticleSystem Explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(Vector3 position)
    {
        Instantiate(Explosion, position, Quaternion.identity);
    }

}
