using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static Canvas thisPauseMenu;
    
    //Prendi il canvas e disattivi il gameObject
    void Awake()
    {
        thisPauseMenu = GetComponent<Canvas>();
        gameObject.SetActive(false);
    }
    //Carico la scena e tempo riparte
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        thisPauseMenu.gameObject.SetActive(false);
    }
}
