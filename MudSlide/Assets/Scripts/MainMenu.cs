using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIDocument mainMenuDocument;

    private Button new_game_button;
    private Button options_button;
    private Button quit_button;

    public GameObject mainMenuObject;
    public GameObject optionsMenuObject;

    void Awake()
    {
        VisualElement root = mainMenuDocument.rootVisualElement;

        new_game_button = root.Q<Button>("new_game_button");
        options_button = root.Q<Button>("options_button");
        quit_button = root.Q<Button>("quit_button");

        new_game_button.clickable.clicked += PlayGame;
        options_button.clickable.clicked += ShowOptionsMenu;
        quit_button.clickable.clicked += QuitGame;
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("PlayerSelectionScene");
    }

    private void ShowOptionsMenu()
    {
        print("Options Menu print");
        Debug.Log("Optionsss");
        mainMenuObject.SetActive(false);
        optionsMenuObject.SetActive(true);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
