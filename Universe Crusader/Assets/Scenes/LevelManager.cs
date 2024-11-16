using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Sounds
{
    int levelUnLock;
    public Button[] buttons; 
    
    void Start()
    {
        levelUnLock = PlayerPrefs.GetInt("levels", 1);
        for (int i = 0; i < buttons.Length; i++) 
        { 
            buttons[i].interactable = false;
        }

        for (int i = 0; i < levelUnLock; i++)
        {
            buttons[i].interactable = true;
        }
    }

    
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); 
    }

}
