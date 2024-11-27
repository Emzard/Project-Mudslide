using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] UIDocument optionsMenuDocument;
    [SerializeField] UIDocument previousMenuDocument;

    private Button back_button;

    void Awake()
    {

        VisualElement root = optionsMenuDocument.rootVisualElement;

        back_button = root.Q<Button>("back_button");
        back_button.clickable.clicked += BackButton;
    }


    private void BackButton()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Menus")
        {
            VisualElement main_screen = previousMenuDocument.rootVisualElement.Q<VisualElement>("main-menu-screen");
            main_screen.style.display = DisplayStyle.Flex;
        }
        
        if (sceneName == "MainGame")
        {
            VisualElement pause_screen = previousMenuDocument.rootVisualElement.Q<VisualElement>("pause-screen");
            pause_screen.style.display = DisplayStyle.Flex;
        }

        VisualElement options_screen = optionsMenuDocument.rootVisualElement.Q<VisualElement>("options-screen");
        options_screen.style.display = DisplayStyle.None;

    }

}
