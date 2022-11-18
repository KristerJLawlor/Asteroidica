using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TMP_Text Clock;
    int Timer;
    // Start is called before the first frame update
    void Start()
    {
        Clock.text = "Time: 0";
        //Timer = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Mathf.RoundToInt(Time.timeSinceLevelLoad*10);
        Clock.text = "Time: " + Timer;
    }
    /*public void ChangeTime(int S, int M, int H)
    {
        Clock.text = "Time: " + H + ":" + M + ":" + S;
    }*/
}
