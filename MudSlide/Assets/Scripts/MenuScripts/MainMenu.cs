using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIDocument mainMenuDocument;
    [SerializeField] UIDocument optionsMenuDocument;

    private Button new_game_button;
    private Button options_button;
    private Button quit_button;

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
        //mainMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        //optionsMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;

        VisualElement main_screen = mainMenuDocument.rootVisualElement.Q<VisualElement>("main-menu-screen");
        main_screen.style.display = DisplayStyle.None;

        VisualElement options_screen = optionsMenuDocument.rootVisualElement.Q<VisualElement>("options-screen");
        options_screen.style.display = DisplayStyle.Flex;
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
