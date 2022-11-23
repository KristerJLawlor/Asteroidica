using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject AsteroidObject;
    public Rigidbody AsteroidRig;
    public float SpeedVar = 8f;
    public Vector3 AsteroidSpeed;

    public ParticleSystem Explosion;

    // Start is called before the first frame update
    void Start()
    {
       AsteroidRig = GetComponent<Rigidbody>();

        AsteroidSpeed = new Vector3(SpeedVar, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidRig.velocity = AsteroidSpeed;

        if(AsteroidRig.position.x > 180f)
        {
            Explode(AsteroidRig.position);
            Destroy(AsteroidObject);
        }
    }

    public void Explode(Vector3 position)
    {
        Instantiate(Explosion, position, Quaternion.identity);
    }
}
