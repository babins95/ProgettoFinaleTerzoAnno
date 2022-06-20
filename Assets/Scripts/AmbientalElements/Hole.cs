using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [HideInInspector]
    public bool filled;
    Collider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<PlayerBack>() && !filled)
    //    {
    //        manager.falling = true;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Crate>())
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Crate>() && !filled)
        {
            collider.isTrigger = false;
        }       
    }
}
