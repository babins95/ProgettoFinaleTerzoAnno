﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Cancello i salvataggi poi carico la scena
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("checkpointX");
        PlayerPrefs.DeleteKey("checkpointY");
        PlayerPrefs.DeleteKey("saved");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Il debug è solo per capire se viene premuto visto che funziona solo nella build
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
