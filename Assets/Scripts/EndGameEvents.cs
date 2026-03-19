using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndGameEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _quit;
    private Button _restart;
    private Button _mainMenu;

    public void Awake()
    {
        _document = GetComponent<UIDocument>();

        _quit = _document.rootVisualElement.Q("QuitButton") as Button;
        _quit.RegisterCallback<ClickEvent>(OnQuitClick);

        _restart = _document.rootVisualElement.Q("RestartButton") as Button;
        _restart.RegisterCallback<ClickEvent>(OnRestartClick);

        _mainMenu = _document.rootVisualElement.Q("MainMenuButton") as Button;
        _mainMenu.RegisterCallback<ClickEvent>(OnMainMenuClick);
    }

    public void OnQuitClick(ClickEvent quit)
    {
        Application.Quit();
    }

    public void OnRestartClick(ClickEvent restart)
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMainMenuClick(ClickEvent mainMenu)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
