using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text ResourceCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeResource(int resources)
    {
        ResourceCount.text = ": " + resources;
    }
}
