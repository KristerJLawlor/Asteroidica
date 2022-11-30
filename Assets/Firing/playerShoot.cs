using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerShoot : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject laserCannonPrefab;
    public GameObject scattershotPrefab;
    public GameObject missilePrefab;
    public GameObject selectedWeapon;
    public float ROF = .3f;
    public bool lastFire=false;
    public bool canShoot=false;
    public WeaponIndicater indicator;
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
            if (selectedWeapon == laserPrefab )
            {
                indicator.setActiveWeapon(1);
                selectedWeapon = scattershotPrefab;
            }
            else if (selectedWeapon == scattershotPrefab)
            {
                indicator.setActiveWeapon(2);
                selectedWeapon = laserCannonPrefab;
                ROF = .001f;
                
            }
            else if (selectedWeapon == laserCannonPrefab)
            {
                indicator.setActiveWeapon(3);
                selectedWeapon = missilePrefab;
                ROF = 2;
            }
            else
            {
                indicator.setActiveWeapon(0);
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
            if (selectedWeapon==scattershotPrefab)
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
                //USE THIS TO MOVE LAZER OFF CAMERA FACE
                temp.GetComponent<Rigidbody>().transform.position = new Vector3(temp.GetComponent<Rigidbody>().transform.position.x, temp.GetComponent<Rigidbody>().transform.position.y -1f, temp.GetComponent<Rigidbody>().transform.position.z);
                //USE THIS TO MOVE LAZER OFF CAMERA FACE
            }
            if (selectedWeapon == missilePrefab)
            {
                temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 20 + this.GetComponent<CameraLook>().myRig.velocity;
            }
            canShoot = false;
            StartCoroutine(Reload());
        }
    }
}
