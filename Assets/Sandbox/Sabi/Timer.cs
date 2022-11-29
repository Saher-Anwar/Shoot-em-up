using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [Header("Component")]
    public Text timerText;
    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    private float setTime;

    // Start is called before the first frame update
    void Start()
    {
        setTime = currentTime;
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
        if (countDown && currentTime <= 0 || !countDown && currentTime >= setTime)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
