using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidBossHealth : MonoBehaviour
{
    public Rigidbody BossRig;
    public int HP = 1500;
    public int MaxHP = 1500;
    public ParticleSystem Explosion;
    public GameObject Portal;
    void Start()
    {
        BossRig = GetComponent<Rigidbody>();
    }
    public void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "laser" || c.gameObject.tag == "Missile")
        {

            if (HP <= 0)
            {
                StartCoroutine(DestroyMe());
                int pickupSpawn = Random.Range(1, 100);
                //Assuning this is a laser
                Explode(this.transform.position);
                if (pickupSpawn % 10 == 0)
                {
                    //GameObject.Instantiate(pickup, this.GetComponent<AsteroidMovement>().AsteroidRig.position, this.transform.rotation);
                }
            }
            else
            {
                if (c.gameObject.tag == "Missile")
                {
                    HP -= 10;
                }
                HP-=2;
            }
        }


    }
    public IEnumerator DestroyMe()
    {
        Explode(this.transform.position + this.transform.up * 3);
        Explode(this.transform.position - this.transform.right * 3);
        Explode(this.transform.position + this.transform.right * 3);
        Explode(this.transform.position);
        Explode(this.transform.position - this.transform.up * 3);
        Explode(this.transform.position - this.transform.forward * 3);
        Explode(this.transform.position + this.transform.forward * 3);


        Explode(this.transform.position + this.transform.up * 11);
        Explode(this.transform.position - this.transform.right * 11);
        Explode(this.transform.position + this.transform.right * 11);
        Explode(this.transform.position - this.transform.up * 11);
        Explode(this.transform.position - this.transform.forward * 11);
        Explode(this.transform.position + this.transform.forward * 11);


        Explode(this.transform.position + this.transform.up * 20);
        Explode(this.transform.position - this.transform.right * 20);
        Explode(this.transform.position + this.transform.right * 20);
        Explode(this.transform.position - this.transform.up * 20);
        Explode(this.transform.position - this.transform.forward * 20);
        Explode(this.transform.position + this.transform.forward * 20);

        yield return new WaitForSeconds(2f);
        //StartCoroutine(SpawnPortal());
        GameObject.Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        Debug.Log("made it to OnDestroy");
        
        //spawn the portal that allows passage to next level
        Instantiate(Portal, Vector3.zero, Quaternion.identity);
    }

    
    /*public IEnumerator SpawnPortal()
    {
        Debug.Log("made it to SpawnPortal");
        //yield return new WaitForSeconds(2f);
        
        yield return new WaitForSeconds(4f);
        Instantiate(Portal, Vector3.zero, Quaternion.identity);
    }
    */


    // Update is called once per frame
    void Update()
    {

    }

    public void Explode(Vector3 position)
    {
        Instantiate(Explosion, position, Quaternion.identity);
    }
}