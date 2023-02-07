using System.Diagnostics.SymbolStore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _cameraSpeedSlider;

    [SerializeField] private TextMeshProUGUI _volumeText;
    [SerializeField] private TextMeshProUGUI _cameraSpeedText;

    private CanvasGroup _menuCanvas;

    void Start()
    {
        _menuCanvas = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        _volumeText.text = Mathf.CeilToInt(_volumeSlider.value * 100).ToString();
        _cameraSpeedText.text = Mathf.CeilToInt(_cameraSpeedSlider.value * 100).ToString();
    }

    public void Save()
    {
        GameManager.SetVolume(_volumeSlider.value);
        GameManager.SetCameraSpeed(_cameraSpeedSlider.value);
    }

    public void Cancel()
    {
        HideSettings();
    }

    public void HideSettings()
    {
        _menuCanvas.interactable = false;
        _menuCanvas.blocksRaycasts = false;
        _menuCanvas.alpha = 0;
    }

    public void ShowSettings()
    {
        _volumeText.text = Mathf.CeilToInt(GameManager.CurrentVolume).ToString();
        _cameraSpeedText.text = Mathf.CeilToInt(GameManager.CameraSpeed).ToString();
        
        _volumeSlider.value = GameManager.CurrentVolume;
        _cameraSpeedSlider.value = GameManager.CameraSpeed;

        _menuCanvas.interactable = true;
        _menuCanvas.blocksRaycasts = true;
        _menuCanvas.alpha = 1;
    }
}
