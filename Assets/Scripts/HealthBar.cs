using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void setMaxHealth(float maxHp){
        slider.maxValue = maxHp;
        slider.value = maxHp;

    }
    public void setHealth(float health){
        slider.value = health;
    }
}
