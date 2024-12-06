using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    //public Text gameOverMessage;
    bool isGameOver = false;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Hero-01").GetComponent<PlayerController>();
    }

    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            //gameOverMessage.text = "You saved " + playerController.collectibles + " people!";
            gameOverScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
