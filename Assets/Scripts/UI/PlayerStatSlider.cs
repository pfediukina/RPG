using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatSlider : MonoBehaviour
{
    [SerializeField] private Slider _statSlider;
    [SerializeField] private GameObject _fillArea;
    [SerializeField] private TextMeshProUGUI _currentStatText;
    [SerializeField] private TextMeshProUGUI _maxStatText;

    public void SetSlider(float currentValue, float maxValue)
    {
        float value = currentValue / maxValue;
        SetSliderValue(value);
        SetText(currentValue, maxValue);
    }

    private void SetSliderValue(float value)
    {   
        float newValue = Mathf.Clamp(value, 0, 1);
        _statSlider.value = value;
        _fillArea.SetActive(value == 0 ? false : true);
    }

    private void SetText(float currentValue, float maxValue)
    {
        if(_currentStatText != null)
            _currentStatText.text = Mathf.CeilToInt(Mathf.Clamp(currentValue, 0, maxValue)).ToString();
        if(_maxStatText != null)
            _maxStatText.text = Mathf.CeilToInt(maxValue).ToString();
    }
}
