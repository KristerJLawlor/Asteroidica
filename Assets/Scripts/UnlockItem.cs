using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockItem : MonoBehaviour
{
    bool isUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void collided()
    {
        isUnlocked = true;
    }
    bool isPurchasedFunction()
    {
        return isUnlocked;
    }
}
