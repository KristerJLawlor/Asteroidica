using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingScript : MonoBehaviour
{
    //public GameObject Player;
    public int price;
    public Button thisButton;
    public bool isPurchased = false;
    int resource;
    // Start is called before the first frame update
    void Start()
    {
        //int resource = PlayerPrefs.GetInt("Resource", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PurchaseItem()
    {
        if (!isPurchased)
        {
            if (resource >= price)
            {
                resource = resource - price;
                isPurchased = true;
                thisButton.interactable = false;
                PlayerPrefs.SetInt("Resource", resource);

            }
            else
            {
                thisButton.image.color = Color.red;
                StartCoroutine(Wait());
                PlayerPrefs.SetInt("Resource", resource);
            }
        }
        else
        {
            thisButton.interactable = false;
        }
    }
    public bool isPurchasedFunction()
    {
        return isPurchased;
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        thisButton.image.color = Color.white;
    }
}
