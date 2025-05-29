using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
    public void SetCurrentHealth(int healthAmount)
    {
        slider.value = healthAmount;
    }
    public void UpdateHealth(int healthAmount, int maxHealth)
    {
        SetCurrentHealth(healthAmount);
        SetMaxHealth(maxHealth);
    }
}
