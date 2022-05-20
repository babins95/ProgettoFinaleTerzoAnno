using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTerrain : MonoBehaviour
{
    [SerializeField] GameManager manager;
    bool stillFalling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se il giocatore collide con l'oggetto metto a true il fatto che stai cadendo
        //se sei nella versione sbagliata e se non è già iniziata la caduta
        if(collision.GetComponent<Adult>() && !manager.falling)
        {
            StartCoroutine("SetFalling");
            stillFalling = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Adult>() && !manager.falling)
        {
            StartCoroutine("SetFalling");
            stillFalling = true;
        }
    }

    //se esci dall'area prima dell'inizio della caduta interrompe il processo
    private void OnTriggerExit2D(Collider2D collision)
    {
        stillFalling = false;
        StopCoroutine(SetFalling());
    }

    IEnumerator SetFalling()
    {
        yield return new WaitForSeconds(0.5f);
        if (stillFalling)
        {
            manager.falling = true;
        }
    }
}
