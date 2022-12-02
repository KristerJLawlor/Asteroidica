using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidShooter : MonoBehaviour
{
    public GameObject laserPrefab;

    public float ROF = 2f;

    public bool lastFire = false;
    public bool canShoot = false;

    public Rigidbody myRig;
    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        Attack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(ROF);
        canShoot = true;
        Attack();
    }

    public void Attack()
    {
        if (canShoot)
        {
            GameObject temp = GameObject.Instantiate(laserPrefab, myRig.position + this.transform.forward * 2f, this.transform.rotation );
            temp.transform.up = this.transform.forward;
            temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 32 + myRig.velocity;

            GameObject temp1 = GameObject.Instantiate(laserPrefab, myRig.position + this.transform.forward * 2f, this.transform.rotation);
            temp1.transform.up = this.transform.forward;
            temp1.GetComponent<Rigidbody>().velocity = this.transform.forward * -32 + myRig.velocity;

            GameObject temp2 = GameObject.Instantiate(laserPrefab, myRig.position + this.transform.forward * 2f, this.transform.rotation);
            temp2.transform.up = this.transform.right;
            temp2.GetComponent<Rigidbody>().velocity = this.transform.right * 32 + myRig.velocity;

            GameObject temp3 = GameObject.Instantiate(laserPrefab, myRig.position + this.transform.forward * 2f, this.transform.rotation);
            temp3.transform.up = this.transform.right;
            temp3.GetComponent<Rigidbody>().velocity = this.transform.right * -32 + myRig.velocity;

        }
        canShoot = false;
        StartCoroutine(Reload());
    }

}

