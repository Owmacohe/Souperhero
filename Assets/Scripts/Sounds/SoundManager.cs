using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] sounds;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();


        playeSound("Souperhero_Daytime2");
        playeSound("ambianceSFX");
       
    }

    
    public void playeSound(string soundName)
    {
        switch (soundName)
        {
            case "Souperhero_Daytime2":
                audioSource.PlayOneShot(sounds[0]);
                break;
            case "ambianceSFX":
                audioSource.PlayOneShot(sounds[1]);
                break;

            case "buttonClick":
                audioSource.PlayOneShot(sounds[2]);
                break;

        }
    }



}
