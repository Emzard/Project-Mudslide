using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI LivesCounter;
    public TextMeshProUGUI Lose_Great_Text;
    public GameObject star_1, star_2, star_3; 
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        if (playerController.collectibles == 0)
        {
            // show no stars
            Lose_Great_Text.SetText("YOU LOSE!");
        }
        
        if (playerController.collectibles >= 5)
        {
            // show one star
            star_1.SetActive(true);
            Lose_Great_Text.SetText("NICE!");
        } 
        
        if (playerController.collectibles >= 10)
        {
            // show two stars
            star_2.SetActive(true);
            Lose_Great_Text.SetText("GOOD WORK!");
        }

        if (playerController.collectibles >= 15)
        {
            // show all stars
            star_3.SetActive(true);
            Lose_Great_Text.SetText("GREAT WORK!");
        }

        LivesCounter.SetText(playerController.collectibles.ToString());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("PlayerSelectionScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menus");
    }
}
