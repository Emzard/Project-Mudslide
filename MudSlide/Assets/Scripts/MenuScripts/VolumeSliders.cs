using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeSliders : MonoBehaviour
{
    // Audio Source for music
    private AudioSource musicAudio;

    // Audio Source for sound
    private AudioSource soundAudio;

    // root visual element
    private VisualElement options_screen;

    public AudioClip jumpSound;

    // Start is called before the first frame update

    //private void Awake()
    //{
    //    options_screen = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("options-screen");
    //    musicAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    //    soundAudio = GetComponent<AudioSource>();

    //    // get music slider
    //    var music_slider = options_screen.Q<Slider>(className: "music-slider");
    //    GetCurrentVolume(music_slider, "music-volume");
    //    onAwake(music_slider);
    //}

    //void onAwake(Slider s)
    //{
    //    var tracker = s.Q(className: Slider.trackerUssClassName);
    //    var dragger = s.Q(className: Slider.draggerUssClassName);
    //    s.value = 20;

    //    Color color = new Color(0.05f, 1, 1, 1);

    //    var highlightTracker = new VisualElement()
    //    {
    //        name = "sub-tracker"
    //    };

    //    tracker.Add(highlightTracker); //Adding it as a child means it will be drawn on top
    //    highlightTracker.style.backgroundColor = color;


    //    highlightTracker.style.width = dragger.transform.position.x;
    //    highlightTracker.style.height = tracker.layout.height;

    //}

    void Start()
    {
        options_screen = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("options-screen");
        musicAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        soundAudio = GetComponent<AudioSource>();

        // get music slider
        var music_slider = options_screen.Q<Slider>(className: "music-slider");
        GetCurrentVolume(music_slider, "music-volume");
        SkinSlider(music_slider, musicAudio);
        musicAudio.Play();
        music_slider.value = 50;

        // get sound slider
        var sound_slider = options_screen.Q<Slider>(className: "sound-slider");
        GetCurrentVolume(sound_slider, "sound-volume");
        //sound_slider.value = 50;
        SkinSlider(sound_slider, soundAudio);

    }

    void GetCurrentVolume(Slider s, string player_pref)
    {
        if (PlayerPrefs.HasKey(player_pref)) { 
            s.value = PlayerPrefs.GetFloat(player_pref);
        } else
        {
            s.value = 50;
        }
    }


    void SkinSlider(Slider s, AudioSource audio)
    {
        var tracker = s.Q(className: Slider.trackerUssClassName);
        var dragger = s.Q(className: Slider.draggerUssClassName);
        s.value = 20;

        Color color = new Color(0.05f, 1, 1, 1);

        var highlightTracker = new VisualElement()
        {
            name = "sub-tracker"
        };

        tracker.Add(highlightTracker); //Adding it as a child means it will be drawn on top
        highlightTracker.style.backgroundColor = color;

        
        highlightTracker.style.width = dragger.transform.position.x;
        highlightTracker.style.height = tracker.layout.height;

        s.RegisterValueChangedCallback((evt) =>
        {
            Debug.Log("New value: " + evt.newValue);
            audio.volume = evt.newValue / 100f;

            if (s.ClassListContains("sound-slider"))
            {
                audio.PlayOneShot(jumpSound, 0.4f);
                PlayerPrefs.SetFloat("sound-volume", evt.newValue / 100f);
            }

            if (s.ClassListContains("music-slider"))
            {
                PlayerPrefs.SetFloat("music-volume",evt.newValue / 100f);
            }

            highlightTracker.style.width = dragger.transform.position.x;
            highlightTracker.style.height = tracker.layout.height;
        });
    }


}
