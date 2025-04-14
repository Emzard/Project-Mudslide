using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
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
            Time.timeScale = 0;
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

    IEnumerator IncreaseGamespeed()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(15);
            Time.timeScale += 0.1f;
        }
    }
}
