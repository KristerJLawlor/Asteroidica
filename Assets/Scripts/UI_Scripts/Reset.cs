using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    public Button[] lvlButtons;
    public Button[] shopButtons;
    public void ResetScenes()
    {
        for (int i = 1; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
        }
        for (int i = 0; i < shopButtons.Length; i++)
        {
            shopButtons[i].interactable = true;
        }
        PlayerPrefs.DeleteAll();
    }
}
