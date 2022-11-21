using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerShoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject laserCannonPrefab;
    public GameObject selectedWeapon;
    public float ROF = .3f;
    public bool lastFire=false;
    public bool canShoot=false;
    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = laserPrefab;
    }
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(ROF);
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
        
        if (Input.GetKeyDown("space"))
        {
            if (selectedWeapon == laserPrefab)
            {
                selectedWeapon = laserCannonPrefab;
                ROF = .001f;
            }
            else 
            {
                selectedWeapon = laserPrefab;
                ROF = .3f;
            }
        }
        if (lastFire && canShoot)
        {
            GameObject temp = GameObject.Instantiate(selectedWeapon, this.GetComponent<CameraLook>().myRig.position + this.transform.forward * .9f, this.transform.rotation);
            //temp.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,90,90);
            temp.transform.up = this.transform.forward;
            temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 8 + this.GetComponent<CameraLook>().myRig.velocity;
            if (selectedWeapon == laserCannonPrefab)
            {
                temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 100 + this.GetComponent<CameraLook>().myRig.velocity;
            }
            canShoot = false;
            StartCoroutine(Reload());
        }
    }
}
