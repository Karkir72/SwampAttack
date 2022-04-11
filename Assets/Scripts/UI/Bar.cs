using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    public void OnValueChanged(int value, int maxValue) 
    {
        Slider.value = (float)value / maxValue;
    }
}
