using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takingDamage : MonoBehaviour
{
    public int HP = 20;
    public int maxHP = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        HP--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
