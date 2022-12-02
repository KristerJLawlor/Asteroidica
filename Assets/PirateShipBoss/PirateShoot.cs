using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShoot : MonoBehaviour
{
    public GameObject cannonPrefab;

    public float ROF = 4f;

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
            GameObject temp = GameObject.Instantiate(cannonPrefab, myRig.position + new Vector3(0, 3, 0), this.transform.rotation);
            temp.transform.up = this.transform.forward;
            temp.GetComponent<Rigidbody>().velocity = this.transform.right * 32 + myRig.velocity;


            GameObject temp3 = GameObject.Instantiate(cannonPrefab, myRig.position + new Vector3(0, 3, -20), this.transform.rotation);
            temp3.transform.up = this.transform.forward;
            temp3.GetComponent<Rigidbody>().velocity = this.transform.right * 32 + myRig.velocity;

            GameObject temp4 = GameObject.Instantiate(cannonPrefab, myRig.position + new Vector3(0, 3, 20), this.transform.rotation);
            temp4.transform.up = this.transform.forward;
            temp4.GetComponent<Rigidbody>().velocity = this.transform.right * 32 + myRig.velocity;

        }
        canShoot = false;
        StartCoroutine(Reload());
    }

}
