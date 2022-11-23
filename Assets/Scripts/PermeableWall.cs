using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermeableWall : MonoBehaviour
{
    // Start is called before the first frame update
    public bool solid = true;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Permeate(bool canProceed)
    {
        if (canProceed)
        {
            Destroy(this.gameObject);
        }
        else
        {

        }
    }
}
