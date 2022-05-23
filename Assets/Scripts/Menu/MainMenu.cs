using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public new GameObject newGameBannerText;
    //Cancello i salvataggi poi carico la scena
    public void NewGame()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("saved", 0);
        PlayerPrefs.SetInt("newCameraPos", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
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
    public void NewGameBanner()
    {
        if (PlayerPrefs.GetInt("saved") == 1)
        {
            newGameBannerText.SetActive(true);
            EventSystem.current.SetSelectedGameObject(newGameBannerText.transform.GetChild(2).gameObject);
        }
        else
        {
            NewGame();
        }
    }
}
