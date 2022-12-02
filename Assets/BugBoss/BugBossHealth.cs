using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBossHealth : MonoBehaviour
{
    public Rigidbody BossRig;
    public int HP = 100;
    public int MaxHP = 100;
    public ParticleSystem Explosion;
    public GameObject Portal;
    public Quaternion PortalRotation;
    void Start()
    {
        BossRig = GetComponent<Rigidbody>();
        PortalRotation = Quaternion.Euler(0, -90, 0);
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
        GameObject.Destroy(this.gameObject);

        //spawn the portal that allows passage to next level
        Instantiate(Portal, new Vector3(400, 6, -28), PortalRotation);
    }

    public void OnDestroy()
    {
        Debug.Log("made it to OnDestroy");

        //spawn the portal that allows passage to next level
        //Instantiate(Portal, new Vector3(400, 6, -28), PortalRotation);
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