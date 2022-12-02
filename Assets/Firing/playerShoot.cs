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
    public WeaponIndicater indicater;
    public int mMissile;


    //audio    
    public AudioSource defaultlaseraudio;    
    public AudioSource scattershotaudio;    
    public AudioSource lasercannonaudio;
    public AudioSource missileaudio;

    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = laserPrefab;
        mMissile = 2;
    }
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(ROF);
        canShoot = true;
        mMissile = 2;
    }
    public void Attack(InputAction.CallbackContext c)
    {
        if (c.phase == InputActionPhase.Started)
        {
            lastFire = true;
            if (lastFire && selectedWeapon==laserCannonPrefab)
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
                indicater.setActiveWeapon(1);
                selectedWeapon = scattershotPrefab;
                scattershotPrefab.GetComponent<weaponScript>().TTL = 1.15f;
            }
            else if (selectedWeapon == scattershotPrefab)
            {
                indicater.setActiveWeapon(2);
                selectedWeapon = laserCannonPrefab;
                laserCannonPrefab.GetComponent<weaponScript>().TTL = 1.5f;
                ROF = .001f;
                
            }
            else if (selectedWeapon == laserCannonPrefab && this.GetComponent<CameraLook>().ammoCount>0)
            {
                indicater.setActiveWeapon(3);
                selectedWeapon = missilePrefab;
                ROF = 2;
            }
            else
            {
                indicater.setActiveWeapon(0);
                selectedWeapon = laserPrefab;
                laserPrefab.GetComponent<weaponScript>().TTL = 2f;
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

            //play audio
            defaultlaseraudio.Play();

            if (selectedWeapon==scattershotPrefab)
            {
                for (int i = 0; i < 4; i++) { 
                temp = GameObject.Instantiate(selectedWeapon, this.GetComponent<CameraLook>().myRig.position + this.transform.forward * .9f, this.transform.rotation);
                //temp.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0,90,90);
                temp.transform.up = this.transform.forward;
                temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 32 + this.GetComponent<CameraLook>().myRig.velocity + this.transform.right * Random.Range(-1.0f, 1.0f) + this.transform.up * Random.Range(-1.0f, 1.0f);

                    //play audio
                    scattershotaudio.Play();
                }
            }
            if (selectedWeapon == laserCannonPrefab)
            {
                temp.GetComponent<Rigidbody>().velocity = this.transform.forward * 100 + this.GetComponent<CameraLook>().myRig.velocity;
                //USE THIS TO MOVE LAZER OFF CAMERA FACE
                temp.GetComponent<Rigidbody>().transform.position = new Vector3(temp.GetComponent<Rigidbody>().transform.position.x, temp.GetComponent<Rigidbody>().transform.position.y -1f, temp.GetComponent<Rigidbody>().transform.position.z);
                //USE THIS TO MOVE LAZER OFF CAMERA FACE

                //play audio
                lasercannonaudio.Play();
            }
            if (selectedWeapon == missilePrefab)
            {
                this.GetComponent<CameraLook>().ammoCount--;
                temp.transform.forward = this.transform.forward *-1;
                temp.GetComponent<Rigidbody>().velocity = this.transform.forward * mMissile + this.GetComponent<CameraLook>().myRig.velocity;
                StartCoroutine(missileIncrease());
                 IEnumerator missileIncrease()
                {
                    yield return new WaitForSeconds(.5f);
                    mMissile = 50;
                    temp.GetComponent<Rigidbody>().velocity = this.transform.forward * mMissile + this.GetComponent<CameraLook>().myRig.velocity;

                }

                if (this.GetComponent<CameraLook>().ammoCount == 0)
                {
                    indicater.setActiveWeapon(0);
                    selectedWeapon = laserPrefab;
                    ROF = .3f;

                }
                //play audio
                missileaudio.Play();

            }
            canShoot = false;
            StartCoroutine(Reload());
        }
    }
    

}
