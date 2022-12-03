using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{

    static LevelMusic instance;

    public AudioClip[] MusicClips;
    public AudioSource Audio;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
         if (instance == null) { instance = this; }
         else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

        // Hooks up the 'OnSceneLoaded' method to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
     }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log(scene.name);
        // Replacement variable (doesn't change the original audio source)
        AudioSource source = new AudioSource();

        // Plays different music in different scenes
        
        switch (scene.name)
        {
            case "Level1 Intro":
                source.clip = MusicClips[0];
                break;
            case "Level1 Boss":
                source.clip = MusicClips[1];
                break;
            case "Level2 Intro":
                source.clip = MusicClips[2];
                break;
            case "Level2 Boss":
                source.clip = MusicClips[3];
                break;
            case "Level3 Intro":
                source.clip = MusicClips[4];
                break;
            case "Level3 Boss":
                source.clip = MusicClips[5];
                break;
            case "Level Selector":
                source.clip = MusicClips[6];
                break;
            case "Tutorial":
                source.clip = MusicClips[7];
                break;
            default:
                source.clip = MusicClips[6];
                break;
        }
        



        // Only switch the music if it changed
        if (source.clip != Audio.clip)
        {
            Audio.enabled = false;
            Audio.clip = source.clip;
            Audio.enabled = true;
        }
    }
}
