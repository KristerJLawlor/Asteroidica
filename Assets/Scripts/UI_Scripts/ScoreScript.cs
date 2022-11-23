using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TMP_Text Scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        Scoreboard.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeScore(int score)
    {
        Scoreboard.text = "Score: " + score;
    }
}
