using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Sounds
{


    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Quit()
    {
        Application.Quit();
    }

    /*
    public void PlayGame()
    {
        PlaySound(sounds[0]);
        Invoke("LoadScene", 1);
    }
    */


    public void ExitGame()
    {
        PlaySound(sounds[0]);
        Invoke("Quit", 1);
        Debug.Log("Игра закрылась");

    }

}

