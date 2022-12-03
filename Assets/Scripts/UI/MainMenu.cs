using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void showMission1()
    {
        SceneManager.LoadScene("Mission1");
    }

    public void showMission2()
    {
        SceneManager.LoadScene("Mission2");
    }

    public void showMission3()
    {
        SceneManager.LoadScene("Mission3");
    }

    public void openMainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void startLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void startLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void startLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

}
