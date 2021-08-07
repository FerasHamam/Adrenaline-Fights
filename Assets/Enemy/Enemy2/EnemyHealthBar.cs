using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Gradient gradient;
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void takeDamage()
    {
        slider.value--;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
