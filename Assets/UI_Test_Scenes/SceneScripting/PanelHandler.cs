using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public Button shopB;
    public Button credB;
    public Button instructB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPanel(int p)
    {
        switch(p)
        {
            case 0:
                p1.SetActive(true);
                p2.SetActive(false);
                shopB.interactable = true;
                credB.interactable = true;
                instructB.interactable = true;
                break;
            case 1:
                p1.SetActive(false);
                p2.SetActive(true);
                shopB.interactable = false;
                credB.interactable = false;
                instructB.interactable = false;
                break;
            case 2:
                p1.SetActive(true);
                p3.SetActive(false);
                shopB.interactable = true;
                credB.interactable = true;
                instructB.interactable = true;
                break;
            case 3:
                p1.SetActive(false);
                p3.SetActive(true);
                shopB.interactable = false;
                credB.interactable = false;
                instructB.interactable = false;
                break;
            case 4:
                p1.SetActive(true);
                p4.SetActive(false);
                shopB.interactable = true;
                credB.interactable = true;
                instructB.interactable = true;
                break;
            case 5:
                p1.SetActive(false);
                p4.SetActive(true);
                shopB.interactable = false;
                credB.interactable = false;
                instructB.interactable = false;
                break;
            default:
                break;
        }
    }
}
