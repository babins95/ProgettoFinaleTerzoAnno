using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBack : MonoBehaviour
{

    //tutto questo è diventato obsoleto con i cambi fatti alla cassa
    //non elimino lo script anche se è vuoto perchè comunque lo uso come controllo
    //per vedere se il giocatore è entrato con tutto il corpo dentro qualcosa

    //---------------------------------------------------------------------//
    //GameManager manager;
    //public bool stillOnCrate;

    //private void Start()
    //{
    //    manager = GetComponentInParent<Player>().gameManager;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<Crate>())
    //    {
    //        //onCrate è un bool di controllo che ho messo nel GameManager per vedere se
    //        //sopra una cassa o no, se lo sei non puoi cadere
    //        manager.onCrate = true;
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.gameObject.GetComponent<Crate>())
    //    {
    //        manager.onCrate = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //stillOnCrate è un controllo gestito dal PlayerEye, se davanti al giocatore c'è
    //    //una cassa lo metto a vero, torna a falso quando entri in una zona di caduta
    //    //senza questo secondo controllo quando fai un ponte di più casse si creano bug
    //    if(collision.gameObject.GetComponent<Crate>() && !stillOnCrate)
    //    {
    //        manager.onCrate = false;
    //    }
    //}
}
