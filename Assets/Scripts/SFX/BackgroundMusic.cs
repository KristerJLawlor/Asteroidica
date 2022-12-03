using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic; // The music clip you want to play. The [SerializeField] tag specifies that this variable is viewable in Unity's inspector. I prefer not to use public variables if I can get away with using private ones.

    private AudioSource _audio; // The reference to my AudioSource (look in the Start() function for more details)

    /*********************/
    /* Protected Mono Methods */
    /*********************/
    protected void Start()
    {
        // Get my AudioSource component and store a reference to it in _audio
        // The point of doing this is because GetComponent() is expensive for computer resources
        // So if we can get away with only calling it one time at the start, then let's do that.
        // From this point on, we can refer to our AudioSource through _audio, which makes the computer happier than GetComponent.
        _audio = GetComponent<AudioSource>();

        // We set the audio clip to play as your background music.
        _audio.clip = backgroundMusic;
    }

    /*********************/
    /* Public Interface */
    /*********************/

    public void PlayPauseMusic()
    {
        // Check if the music is currently playing.
        if (_audio.isPlaying)
            _audio.Pause(); // Pause if it is
        else
            _audio.Play(); // Play if it isn't
    }

    public void PlayStop()
    {
        if (_audio.isPlaying)
            _audio.Stop();
        else
            _audio.Play();
    }

    public void PlayMusic()
    {
        _audio.Play();
    }

    public void StopMusic()
    {
        _audio.Stop();
    }

    public void PauseMusic()
    {
        _audio.Pause();
    }
}