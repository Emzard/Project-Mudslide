using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseButton;

    [SerializeField] UIDocument pauseMenuDocument;
    [SerializeField] UIDocument optionsMenuDocument;

    private VisualElement pause_screen;
    private VisualElement options_screen;

    private Button home_button;
    private Button restart_button;
    private Button options_button;
    private Button resume_button;
    void Awake()
    {
        VisualElement root = pauseMenuDocument.rootVisualElement;

        home_button = root.Q<Button>("home_button");
        restart_button = root.Q<Button>("restart_button");
        options_button = root.Q<Button>("options_button");
        resume_button = root.Q<Button>("resume_button");

        home_button.clickable.clicked += NavigateMainMenu;
        restart_button.clickable.clicked += RestartGame;
        options_button.clickable.clicked += LoadSettingsScreen;
        resume_button.clickable.clicked += ResumeGame;
    }

    // display pause menu
    public void Pause()
    {
        pause_screen = pauseMenuDocument.rootVisualElement.Q<VisualElement>("pause-screen");
        pause_screen.style.display = DisplayStyle.Flex;
        //Time.timeScale = 0; // why set timescale? this runs according to real time
        // there might be a better way to pause a game
    }

    public void NavigateMainMenu()
    {
        SceneManager.LoadScene("Menus");
    }

    public void RestartGame()
    {
        Loader.Load(Loader.Scene.EmptyGameScene);
    }

    public void LoadSettingsScreen()
    {
        pause_screen = pauseMenuDocument.rootVisualElement.Q<VisualElement>("pause-screen");
        pause_screen.style.display = DisplayStyle.None;
        options_screen = optionsMenuDocument.rootVisualElement.Q<VisualElement>("options-screen");
        options_screen.style.display = DisplayStyle.Flex;
    }

    public void ResumeGame()
    {
        pause_screen = pauseMenuDocument.rootVisualElement.Q<VisualElement>("pause-screen");
        pause_screen.style.display = DisplayStyle.None;
        //Time.timeScale = 1; //another way to run the timescale
    }

}
