using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public ResourcesScript resourceCounter;
    public Button[] shopButtons;
    // Start is called before the first frame update
    void Start()
    {
        int Resources = PlayerPrefs.GetInt("Resource", 0);
        resourceCounter.ChangeResource(Resources);
        for (int i = 0; i < shopButtons.Length; i++)
        {
            if (PlayerPrefs.GetInt("ShopSetDefault", 0) == i+1)
            {
                print("Sold");
                shopButtons[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int Resources = PlayerPrefs.GetInt("Resource",0);
        resourceCounter.ChangeResource(Resources);
    }
}
