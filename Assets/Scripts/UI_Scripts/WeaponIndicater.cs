using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicater : MonoBehaviour
{
    // Start is called before the first frame update
    public Image defaultBlaster;
    public Image ScatterShot;
    public Image Laser;
    public Image Rocket;
    void Start()
    {
        defaultBlaster.color = Color.white;
        ScatterShot.color = Color.grey;
        Laser.color = Color.grey;
        Rocket.color = Color.grey;
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    public void setActiveWeapon(int weapon)
    {
        switch (weapon)
        {
            case 0:
                defaultBlaster.color = Color.white;
                ScatterShot.color = Color.grey;
                Laser.color = Color.grey;
                Rocket.color = Color.grey;
                break;
            case 1:
                defaultBlaster.color = Color.grey;
                ScatterShot.color = Color.white;
                Laser.color = Color.grey;
                Rocket.color = Color.grey;
                break;
            case 2:
                defaultBlaster.color = Color.grey;
                ScatterShot.color = Color.grey;
                Laser.color = Color.white;
                Rocket.color = Color.grey;
                break;
            case 3:
                defaultBlaster.color = Color.grey;
                ScatterShot.color = Color.grey;
                Laser.color = Color.grey;
                Rocket.color = Color.white;
                break;
            default:
                defaultBlaster.color = Color.white;
                ScatterShot.color = Color.grey;
                Laser.color = Color.grey;
                Rocket.color = Color.grey;
                break;
        }
    }
}
