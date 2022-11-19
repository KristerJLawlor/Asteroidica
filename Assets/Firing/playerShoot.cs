using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerShoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public bool lastFire=false;
    public bool canShoot=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(.3f);
        canShoot = true;
    }
    public void Attack(InputAction.CallbackContext c)
    {
        if (c.phase == InputActionPhase.Started)
        {
            lastFire = true;
        }
        else if (c.phase == InputActionPhase.Canceled)
        {
            lastFire = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (lastFire && canShoot)
        {
            GameObject temp = GameObject.Instantiate(laserPrefab, this.GetComponent<CameraLook>().myRig.position + this.transform.forward * .9f, this.transform.rotation);
            //temp.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,90,90);
            temp.transform.up = this.transform.forward;
            temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 8 + this.GetComponent<CameraLook>().myRig.velocity;
            
            canShoot = false;
            StartCoroutine(Reload());
        }
    }
}
