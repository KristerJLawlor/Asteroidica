using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingScript : MonoBehaviour
{
    public GameObject Player;
    public int price;
    public Button thisButton;
    public bool isPurchased = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int PurchaseItem(int resource)
    {
        if(resource > price)
        {
            resource = resource - price;
            isPurchased = true;
            return resource;
        }
        else
        {
            thisButton.image.color = Color.red;
            return resource;
        }
    }
    bool isPurchasedFunction()
    {
        return isPurchased;
    }
}
