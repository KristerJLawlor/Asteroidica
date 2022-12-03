using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    void Start()
    {
    }
    public void ResetScenes()
    {
        PlayerPrefs.DeleteAll();
    }
}
