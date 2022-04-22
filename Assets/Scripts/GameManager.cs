using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Canvas pauseMenu;
    public static bool swap = false;
    public Player player;
    [SerializeField] Vector3 baseSpawnPosition;

    float playerX;
    float playerY;

    // Start is called before the first frame update
    
    void Start()
    {
        pauseMenu = PauseMenu.thisPauseMenu;
        //se è presente un file di salvataggio
        if (PlayerPrefs.GetInt("saved") == 1)
        {
            //setta la posizione del giocatore a quella dell'ultimo checkpoint toccato
            playerX = PlayerPrefs.GetFloat("checkpointX");
            playerY = PlayerPrefs.GetFloat("checkpointY");
            player.transform.position = new Vector3(playerX, playerY, 0);
        }
        else
        {
            //altrimenti parti dalla posizione di inizio gioco
            player.transform.position = baseSpawnPosition;
        }
    }

    void OnPause()
    {
        //se premi pausa il tempo si ferma e il gameobject si attiva altrimenti succede il contrario
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
        }
    }



}
