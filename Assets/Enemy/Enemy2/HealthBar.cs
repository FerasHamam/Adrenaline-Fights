using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{   
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void setMaxValue(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void Update()
    {
        slider.value = playerMovement.instance.health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
