using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;
    public void SetMaxHP(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
    }
    public void SetHP(int Health)
    {
        slider.value = Health;
    }
}
