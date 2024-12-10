using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralVolume : MonoBehaviour
{
    // get the audio source
    private AudioSource mainCameraAudio;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        // specify for main game scene


        if (PlayerPrefs.HasKey("music-volume"))
        {
            //set the volume
            mainCameraAudio.volume = PlayerPrefs.GetFloat("music-volume");
        } else
        {
            mainCameraAudio.volume = 0.5f;
            PlayerPrefs.SetFloat("music-volume", 0.5f);
        }
    }
}
