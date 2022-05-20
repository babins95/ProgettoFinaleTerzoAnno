using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cratere : MonoBehaviour
{
    [SerializeField] GameManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBack>())
        {
            manager.falling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBack>())
        {
            manager.falling = false;
        }
    }

}
