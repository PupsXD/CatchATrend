using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrendMeter : MonoBehaviour
{
    
    [SerializeField] private Image cringeMeter;

    [SerializeField] private float minValue =  0f;
    [SerializeField] private float maxValue = 1f;

    [SerializeField] private Gradient meterGradient;
    
    private float _currentValue;
    
    public event Action<float> OnValueChanged;
    public float NormalizedValue => (_currentValue - minValue) / (maxValue - minValue);

    private void Start()
    {
        _currentValue = (minValue + maxValue) / 2; // Начальное значение в середине
        UpdateMeter();
    }

    public void AddCringe(float amount)
    {
        UpdateValue(-amount);
    }
    
    public void AddHype(float amount)
    {
        UpdateValue(amount);
    }

    private void UpdateValue(float delta)
    {
        _currentValue = Mathf.Clamp(_currentValue + delta, minValue, maxValue);
        UpdateMeter();
        OnValueChanged?.Invoke(NormalizedValue);
    }

    private void UpdateMeter()
    {
        cringeMeter.fillAmount = NormalizedValue;
    
        // Получаем все ключи градиента
        GradientColorKey[] colorKeys = meterGradient.colorKeys;
    
        // Находим, между какими ключами находится текущее значение
        for (int i = 0; i < colorKeys.Length - 1; i++)
        {
            if (NormalizedValue >= colorKeys[i].time && NormalizedValue <= colorKeys[i + 1].time)
            {
                // Вычисляем локальное нормализованное значение между двумя ключами
                float localNormalized = Mathf.InverseLerp(colorKeys[i].time, colorKeys[i + 1].time, NormalizedValue);
            
                // Интерполируем цвет между двумя ключами
                cringeMeter.color = Color.Lerp(colorKeys[i].color, colorKeys[i + 1].color, localNormalized);
                break;
            }
        }
    }
}
