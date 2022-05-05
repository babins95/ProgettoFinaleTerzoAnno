using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //alla collisione con il giocatore salva la posizione del checkpoint
        //il bool saved serve per vedere se c'è una posizione salvata o meno
        //saved == 0 --> nessun salvataggio, saved == 1 -->salvataggio presente
        PlayerPrefs.SetFloat("checkpointX", this.transform.position.x);
        PlayerPrefs.SetFloat("checkpointY", this.transform.position.y);
        PlayerPrefs.SetInt("saved", 1);
        PlayerPrefs.Save();


        SwitchCounter.isOnCheckpoint = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SwitchCounter.isOnCheckpoint = false;
    }
}
