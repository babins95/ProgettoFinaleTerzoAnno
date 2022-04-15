using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool swap = false;
    public Player player;
    [SerializeField] Vector3 baseSpawnPosition;

    float playerX;
    float playerY;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }


}
