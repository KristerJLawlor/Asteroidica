using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.GameCenter;

public class CameraLook : MonoBehaviour
{
    public Rigidbody myRig;
    Vector2 lastLook;
    float rotSpeed = .005f;
    float pitchSpeed = 0;
    float yawSpeed = 0;
    bool canLook=false;
    Vector3 lastInput;
    float Speed = 5;
    float acceleration = 12f;
    float maxSpeed = 8f;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        myRig = GetComponent<Rigidbody>();
        StartCoroutine(Wait());
    }
    public IEnumerator Wait()
    { 
        yield return new WaitForSeconds(2);
        canLook = true;
        this.GetComponent<playerShoot>().canShoot = true;
    }
    public void Move(InputAction.CallbackContext c)
    {
        if (c.phase == InputActionPhase.Started || c.phase == InputActionPhase.Performed)
        {
            Vector2 temp = c.ReadValue<Vector2>();
            lastInput = new Vector3(temp.x, 0, temp.y);//new Vector3(temp.x, 0, temp.y);
        }
        if (c.phase == InputActionPhase.Canceled)
        {
            lastInput = Vector3.zero;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (canLook)
        {
            pitchSpeed += (lastLook.y * rotSpeed) * -1;
            yawSpeed += (lastLook.x * rotSpeed);
            Vector3 temp = new Vector3(pitchSpeed, yawSpeed, 0);
            /* if (temp.magnitude > 3.14/2)
             {
                 temp = temp.normalized * (3.14f / 2);
             }*/
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + temp);
            Debug.Log(this.transform.rotation.eulerAngles);
            Vector3 Check = this.transform.rotation.eulerAngles;
            if (Check.x > 50 && Check.x < 180)
            {
                Check.x = 50;
            }

            if (Check.x < 310 && Check.x > 180)
            {
                Check.x = 310;
            }
            transform.rotation = Quaternion.Euler(Check);
            //Lateral Movement
            Vector3 newVel = lastInput.x*this.transform.right * Speed + this.transform.forward * lastInput.z * Speed;
            newVel = new Vector3(newVel.x, 0, newVel.z);
            myRig.velocity += newVel * Time.deltaTime;
            if (myRig.velocity.magnitude > maxSpeed)
            { 
                myRig.velocity = myRig.velocity.normalized*maxSpeed;
            }
            //Auto Decelleration
            Vector3 oldVel = new Vector3(myRig.velocity.x, 0, myRig.velocity.z);
            Vector3 velDif = newVel - oldVel;
            myRig.velocity += velDif.normalized * acceleration * Time.deltaTime;
            
            


        }
    }
    public void onLook(InputAction.CallbackContext l)
    {
        //Vector2 input = new Vector2();
        lastLook = l.ReadValue<Vector2>();
        
        
    }
    
}
