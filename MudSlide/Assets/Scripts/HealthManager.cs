using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject heart0, heart1, heart2, heart3; // heart4;
    public GameObject GameOverScreen;
    public static int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current health is: " + health);
        switch (health)
        {
            case 0: // Game Over
                //heart0.SetActive(false);
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                //heart4.SetActive(false);
                Time.timeScale = 0;
                GameOverScreen.SetActive(true);
                break;
            case 1:
                //heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                //heart4.SetActive(false);
                break;
            case 2:
                //heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                //heart4.SetActive(false);
                break;
            case 3:
                //heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                //heart4.SetActive(false);
                break;
            case 4:
                //heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                //heart4.SetActive(true);
                break;
            default: // Game Over
                //heart0.SetActive(false);
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                //heart4.SetActive(false);
                Time.timeScale = 0;
                GameOverScreen.SetActive(true);
                break;
        }
    }
}
