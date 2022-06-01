using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEye : MonoBehaviour
{
    Player player;

    void Start()
    {
       player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.isFacing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.isFacing = false;
    }
}
