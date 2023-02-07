using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager GetInstance {get => _instance;}
    public static float CameraSpeed { get => _instance._currentCameraSpeed; } 
    public static float CurrentVolume { get => _instance._currentVolume; } 
    public int maxLevel { get => _maxLevel;}

    [SerializeField] private int _maxLevel;
    [SerializeField] private AudioSource _ambience;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private AudioMixer _audioMixer;

    private float _currentVolume;
    private float _currentCameraSpeed;

    void Awake()
    {
        if(_instance == null)
            _instance = this;
            
        PlayAmbient();
    }

    void Start()
    {
        SetCameraSpeed(_gameSettings.cameraSpeed);
        SetVolume(_gameSettings.volume);
    }

    public static void PlayAmbient()
    {
        if(GetInstance._ambience == null) return;
        GetInstance._ambience.Play();
        GetInstance._ambience.loop = true;
    }


    public static void SetVolume(float sliderValue)
    {
        GetInstance._currentVolume = sliderValue;
        if(sliderValue == 0)
        {
            GetInstance._audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            GetInstance._audioMixer.SetFloat("MasterVolume", Mathf.Log10(GetInstance._currentVolume) * 20);
        }
    }

    public static void SetCameraSpeed(float sliderValue)
    {
        GetInstance._currentCameraSpeed = sliderValue;
    }
}
