using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool beenUsed = false;
    public GameObject p1;
    public static int ROF = 0;
    public int unlocker;
    MasterMenuController MMC;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The scene name for the first scene is " + SceneManager.GetSceneAt(0).name);
        SceneManager.sceneLoaded += SceneSwapper;
        MMC = GameObject.Find("MainMenu").GetComponent<MasterMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    /*public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }*/
    public void SceneSwapper(Scene s, LoadSceneMode m)
    {
        //Debug.Log("The scene name for the first scene is " + SceneManager.GetSceneByBuildIndex(0).name);
        /*if (beenUsed)//$$ s.name == SceneManager.GetSceneByBuildIndex(0).name)
        {
            SceneManager.sceneLoaded -= SceneSwapper;
            Destroy(this.gameObject);
        }*/

        if (s.name != SceneManager.GetSceneByBuildIndex(8).name)
        {
            //Debug.Log("Setting beenUsed to true!");
            //beenUsed = true;
            Destroy(this.gameObject);
        }
    }
    public void switchScenesfirst()
    {
        if (!beenUsed)
        {
            Debug.Log("Loading game scene");
            SceneManager.LoadScene(1);//Or whatever index you want.
            p1.SetActive(false);
        }
        else
        {
            Debug.Log("Loading Menu scene");
            SceneManager.LoadScene(0);
        }
    }
    public void switchScenessecond()
    {
        if (!beenUsed)
        {
            Debug.Log("Loading game scene");
            SceneManager.LoadScene(2);//Or whatever index you want.
            p1.SetActive(false);
        }
        else
        {
            Debug.Log("Loading Menu scene");
            SceneManager.LoadScene(0);
        }
    }
    public void switchScenesthird()
    {
        if (!beenUsed)
        {
            Debug.Log("Loading game scene");
            SceneManager.LoadScene(4);//Or whatever index you want.
            p1.SetActive(false);
        }
        else
        {
            Debug.Log("Loading Menu scene");
            SceneManager.LoadScene(0);
        }
    }
    public void switchScenesfourth()
    {
        if (!beenUsed)
        {
            Debug.Log("Loading game scene");
            SceneManager.LoadScene(6);//Or whatever index you want.
            p1.SetActive(false);
        }
        else
        {
            Debug.Log("Loading Menu scene");
            SceneManager.LoadScene(0);
        }
    }
    public void switchScenesLevel()
    {
        if (!beenUsed)
        {
            Debug.Log("Loading game scene");
            SceneManager.LoadScene(1);//Or whatever index you want.
            p1.SetActive(false);
        }
        else
        {
            Debug.Log("Loading Menu scene");
            SceneManager.LoadScene(1);
        }
    }
    public void GoToNextScene()
    {
        //unlcoker = MMC.unlockedScene;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (unlocker > MMC.unlockedScene)
        {
            MMC.unlockedScene = unlocker;
            Debug.Log("Where am I " + MMC.unlockedScene);
        }
    }
}
