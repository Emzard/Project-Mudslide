using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoScript : MonoBehaviour
{
    [SerializeField]
    VideoPlayer vid_player;
    // Start is called before the first frame update
    void Start()
    {
        vid_player.loopPointReached += NavigateMainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Menus");
        }
    }

     void NavigateMainMenu(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menus");
    }
}
