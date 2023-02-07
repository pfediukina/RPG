using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private bool _isMainMenu;
    [SerializeField] private TextMeshProUGUI _menuName;
    [SerializeField] private TextMeshProUGUI _startName;

    [SerializeField] private SettingsUI _settings;

    private CanvasGroup _menuCanvas;

    void Start()
    {
        if(!_isMainMenu)
        {
            _menuCanvas = GetComponent<CanvasGroup>();
            _menuName.text = "MENU";
            _startName.text = "CONTINUE";
        }
    }

    public void StartGame()
    {
        if(_isMainMenu)
            SceneManager.LoadScene("GameScene");
        else
        {
            HideMenu();
        }
    }

    public void Settings()
    {
        _settings.ShowSettings();
    }

    public void Exit()
    {
        if(_isMainMenu)
            Application.Quit();
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void HideMenu()
    {
        _settings.HideSettings();
        _menuCanvas.interactable = false;
        _menuCanvas.blocksRaycasts = false;
        _menuCanvas.alpha = 0;
    }

    public void ShowMenu()
    {
        _menuCanvas.interactable = true;
        _menuCanvas.blocksRaycasts = true;
        _menuCanvas.alpha = 1;
    }
}
