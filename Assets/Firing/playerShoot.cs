using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerShoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject laserCannonPrefab;
    public bool scatterShot=false;
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
            if (lastFire && selectedWeapon == laserCannonPrefab)
            {
                this.GetComponent<CameraLook>().canLook = false;
            }
        }
        else if (c.phase == InputActionPhase.Canceled)
        {
            lastFire = false;
            this.GetComponent<CameraLook>().canLook = true;
        }
    }
    public void weaponSwap(InputAction.CallbackContext s)
    {
        if (s.phase == InputActionPhase.Performed)
        {
            if (selectedWeapon == laserPrefab && scatterShot==false)
            {
                scatterShot = true;
            }
            else if (selectedWeapon == laserPrefab && scatterShot)
            {
                selectedWeapon = laserCannonPrefab;
                    ROF = .001f;
                scatterShot = false;
            }
            else
            {
                selectedWeapon = laserPrefab;
                ROF = .3f;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
        if (lastFire && canShoot)
        {
            GameObject temp = GameObject.Instantiate(selectedWeapon, this.GetComponent<CameraLook>().myRig.position + this.transform.forward * .9f, this.transform.rotation);
            //temp.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,90,90);
            temp.transform.up = this.transform.forward;
            temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 32 + this.GetComponent<CameraLook>().myRig.velocity;
            if (scatterShot)
            {
                for (int i = 0; i < 4; i++) { 
                temp = GameObject.Instantiate(selectedWeapon, this.GetComponent<CameraLook>().myRig.position + this.transform.forward * .9f, this.transform.rotation);
                //temp.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,90,90);
                temp.transform.up = this.transform.forward;
                temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 32 + this.GetComponent<CameraLook>().myRig.velocity + this.transform.right * Random.Range(-1.0f, 1.0f) + this.transform.up * Random.Range(-1.0f, 1.0f);
            }
            }
            if (selectedWeapon == laserCannonPrefab)
            {
                temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 100 + this.GetComponent<CameraLook>().myRig.velocity;
                //TESTING
                temp.GetComponent<Rigidbody>().transform.position = new Vector3(temp.GetComponent<Rigidbody>().transform.position.x, temp.GetComponent<Rigidbody>().transform.position.y -.25f, temp.GetComponent<Rigidbody>().transform.position.z);
                //TESTING
            }
            canShoot = false;
            StartCoroutine(Reload());
        }
    }
}
