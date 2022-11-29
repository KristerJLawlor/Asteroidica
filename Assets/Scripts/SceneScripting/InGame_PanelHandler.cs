using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_PanelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject p1;
    public GameObject p2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setGamePanel(int p)
    {
        switch (p)
        {
            case 0:
                p1.SetActive(false);
                p2.SetActive(false);
                break;
            case 1:
                p1.SetActive(true);
                p2.SetActive(false);
                break;
            case 2:
                p1.SetActive(false);
                p2.SetActive(true);
                break;
            default:
                break;
        }
    }
}