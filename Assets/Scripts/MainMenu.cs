using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {

        SceneManager.LoadScene("MainScene");
        Cursor.visible = false;

    }

    public void PlayCredits()
    {
        //SceneManager.LoadScene("CreditScene");
        Debug.Log("Pressed Credit");
    }

    public void HowToPlay()
    {
        Debug.Log("Pressed How to Play");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
