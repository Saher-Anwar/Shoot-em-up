using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [Header("Component")]
    public Text timerText;
    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        timerText.text = currentTime.ToString("0.0");
    }
}
