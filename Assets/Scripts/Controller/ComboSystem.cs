using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] TrendMeter trendMeter;
    [FormerlySerializedAs("comboValues")] [SerializeField] float[] comboThresholds;
    [SerializeField] private float comboDecayRate;
    
    private int currentCombo = 0;
    private float comboMeter = 0f;
    
    public event Action<int> OnComboChange;
    

    private void Start()
    {
        trendMeter.OnValueChanged += CheckCombo;
    }
    
    private void CheckCombo(float trendValue)
    {
        comboMeter = Mathf.Abs(trendValue);

        for (int i = comboThresholds.Length - 1; i >= 0; i--)
        {
            if (comboMeter > comboThresholds[i])
            {

                currentCombo = i;
                OnComboChange?.Invoke(currentCombo);
                break;
            }
        }
        
        
    }
    
    private void Update()
    {
        DecayCombo();
    }
    
    private void DecayCombo()
    {
        comboMeter = Mathf.Max(0, comboMeter- comboDecayRate * Time.deltaTime);
        
        for (int i = 0; i < comboThresholds.Length; i++)
        {
            if (comboMeter < comboThresholds[i] && i < currentCombo)
            {
                currentCombo = i;
                OnComboChange?.Invoke(currentCombo);
                break;
            }
        }
    }
    
    public int GetCurrentCombo()
    {
        return currentCombo;
    }
}
