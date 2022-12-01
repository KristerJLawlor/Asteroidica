using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MasterMenuController : MonoBehaviour
{
    public bool UseMe = false;
    public Canvas MM;
    public int unlockedScene = 1;
    public Button[] lvlButtons;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneSwapper;
        SceneManager.LoadScene(1);
    }
    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneSwapper;
    }
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SceneSwapper(Scene s, LoadSceneMode m)
    {
        MasterMenuController[] temp = GameObject.FindObjectsOfType<MasterMenuController>();
        Debug.Log("Inside scene load " +s.buildIndex);
        if(temp.Length == 1)
        {
            UseMe = true;
        }
        else
        {
            if (UseMe)
            {
                Destroy(this.gameObject);
            }

        }
        if(s.buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        if (s.buildIndex == 1)
        {
            MM.gameObject.SetActive(true);
            for (int i = 0; i < lvlButtons.Length; i++)
            {
                if (unlockedScene < 4)
                {
                    Debug.Log("Dere she Is " + i + " and " + unlockedScene);
                    if (i >= unlockedScene) 
                    { 
                        lvlButtons[i].interactable = false;
                    }
                    else
                    {
                        lvlButtons[i].interactable = true;
                    }
                }
                /*else
                {
                    if (i + 1 > unlockedScene / 2)
                        lvlButtons[i].interactable = false;
                }*/
            }
        }
        else
        {
            MM.gameObject.SetActive(false);
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
    public void LevelLoader(int Level)
    {
        SceneManager.LoadScene(Level);
    }
}
