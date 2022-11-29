using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    public Button[] lvlButtons;
    public void ResetScenes()
    {
        for (int i = 1; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
        }
        PlayerPrefs.DeleteAll();
    }
}
