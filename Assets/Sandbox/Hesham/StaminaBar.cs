using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    private float dValue;
    private float stamina;
    private float maxStamina;

    public void setMaxStamina(int max){
        slider.maxValue = max;
        slider.value = max;
        maxStamina = max;
        stamina = max;
        

    }
    public void decreaseStamina(){
        if(stamina != 0){
            stamina -= dValue * Time.deltaTime;
            slider.value = stamina;
        }
    }
    public void increaseStamina(){
        if(stamina != maxStamina){
            stamina += dValue * Time.deltaTime;
            slider.value = stamina;
        }
    }
    
}
