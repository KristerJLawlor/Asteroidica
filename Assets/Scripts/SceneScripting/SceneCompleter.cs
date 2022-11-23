using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCompleter : MonoBehaviour
{
    // Start is called before the first frame update
    public bool beenUsed = false;
    public GameObject p1;
    void Start()
    {
        Debug.Log("The scene name for the first scene is " + SceneManager.GetSceneAt(0).name);
        SceneManager.sceneLoaded += SceneSwapper;
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SceneSwapper(Scene s, LoadSceneMode m)
    {
        Debug.Log("The scene name for the first scene is " + SceneManager.GetSceneByBuildIndex(0).name);
        if (beenUsed)//$$ s.name == SceneManager.GetSceneByBuildIndex(0).name)
        {
            SceneManager.sceneLoaded -= SceneSwapper;
            Destroy(this.gameObject);
        }

        if (s.name != SceneManager.GetSceneByBuildIndex(0).name)
        {
            Debug.Log("Setting beenUsed to true!");
            beenUsed = true;
        }
    }
}
