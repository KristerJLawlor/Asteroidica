using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketAmmoCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text RocketCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeResource(int rockets)
    {
        RocketCount.text = ": " + rockets;
    }
}
