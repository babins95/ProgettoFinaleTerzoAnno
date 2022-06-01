using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [HideInInspector]
    public bool filled;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBack>() && !filled)
        {
            manager.falling = true;
        }
    }
}
