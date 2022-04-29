using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static Canvas pauseMenu;
    //di base è nella versione giovane, quindi se swap = true ha cambiato ad adolescente
    public static bool swap = false;
    public Player player;
    [SerializeField] Vector3 baseSpawnPosition;

    float playerX;
    float playerY;

    //scale di default del giocatore e scale attuale del giocatore
    Vector3 defaultPlayerScale;
    Vector3 playerScale;
    //scale del giocatore alla fine della caduta
    float scaleTarget = 0.5f;

    public bool falling;
    [SerializeField] int fallTimer = 5;

    PlayerInput input;

    void Start()
    {
        input = player.GetComponent<PlayerInput>();
        
        defaultPlayerScale = player.transform.localScale;
        playerScale.x= player.transform.localScale.x;
        playerScale.y= player.transform.localScale.y;

        pauseMenu = PauseMenu.thisPauseMenu;
        //se è presente un file di salvataggio
        if (PlayerPrefs.GetInt("saved") == 1)
        {
            //setta la posizione del giocatore a quella dell'ultimo checkpoint toccato
            Spawn();
        }
        else
        {
            //altrimenti parti dalla posizione di inizio gioco
            player.transform.position = baseSpawnPosition;
        }
    }

    void OnPause()
    {
        //se premi il pulsante di pausa il tempo si ferma e il gameobject si attiva altrimenti succede il contrario
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

    void Spawn()
    {
        playerX = PlayerPrefs.GetFloat("checkpointX");
        playerY = PlayerPrefs.GetFloat("checkpointY");
        player.transform.position = new Vector3(playerX, playerY, 0);

        //resetto la scala al defalt, nel caso tu sia "morto" da caduta
        player.transform.localScale = defaultPlayerScale;
        playerScale.x = player.transform.localScale.x;
        playerScale.y = player.transform.localScale.y;
        //stessa cosa per l'actionmap
        input.SwitchCurrentActionMap("Player");
    }


    private void Update()
    {
        if(falling)
        {
            if(fallTimer > 0)
            {
                fallTimer--;
            }
            else
            {
                //se stai cadendo interrompo il movimento e cambio l'actionmap ad una vuota
                //togliendo al giocatore la possibilità di agire
                player.moveVector = Vector2.zero;
                input.SwitchCurrentActionMap("Disabled");
                //e diminuisco i valore della scale fino ad arrivare alla dimensione
                //da fine caduta
                if (playerScale.x > scaleTarget)
                {
                    playerScale.x -= 0.01f;
                    playerScale.y -= 0.01f;

                    player.transform.localScale = new Vector3(playerScale.x, playerScale.y, 1);
                    fallTimer = 5;
                }
                else
                {
                    falling = false;
                    fallTimer = 5;
                    Spawn();
                }
            }
        }
    }
}
