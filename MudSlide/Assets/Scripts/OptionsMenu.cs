using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }


    [SerializeField] UIDocument optionsMenuDocument;

    private Button back_button;
    public GameObject optionsMenuObject;
    public GameObject previousMenuObject;

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

        if(sceneName == "Menus")
        {
            Debug.Log("Menus scene");
            previousMenuObject.SetActive(true);
            optionsMenuObject.SetActive(false);

        } else if (sceneName == "EmptyGameScene")
        {
            Debug.Log("Empty Game Scene");
            previousMenuObject.SetActive(true);
            optionsMenuObject.SetActive(false);
        } else
        {
            Debug.Log("DO nothing");
        }
    }

}
