using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : Sounds
{

    private void OnTriggerEnter2D(Collider2D collusion)
    {
        if (collusion.CompareTag("Player"))
        {
            UnLockLevel();
            PlaySound(sounds[0]);
            gameObject.GetComponent<Renderer>().enabled = false;
            Invoke("LoadScene", 1.3f);
        }
    }
    public void UnLockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", currentLevel + 1);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
