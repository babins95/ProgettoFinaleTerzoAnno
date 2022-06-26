using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnTrigger : MonoBehaviour
{
    //l'oggetto figlio registra la collisione e passa al player il parent
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>())
        {
            collision.GetComponentInParent<Player>().interactableObject = gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>())
        {
            collision.GetComponentInParent<Player>().interactableObject = null;
        }
    }
}
