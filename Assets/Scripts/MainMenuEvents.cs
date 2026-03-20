using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _play;
    private Button _quit;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _play = _document.rootVisualElement.Q("PlayButton") as Button;
        _play.RegisterCallback<ClickEvent>(OnPlayClick);

        _quit = _document.rootVisualElement.Q("QuitButton") as Button;
        _quit.RegisterCallback<ClickEvent>(OnQuitClick);
    }

    private void OnPlayClick(ClickEvent play)
    {
        SceneManager.LoadScene("Game");
    }

    private void OnQuitClick(ClickEvent quit)
    {
        Application.Quit();
    }
}
