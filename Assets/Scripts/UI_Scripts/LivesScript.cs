using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text LifeTotal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLives(int lives)
    {
        LifeTotal.text = "Lives: " + lives;
    }
}
