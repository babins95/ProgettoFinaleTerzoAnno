using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEye : MonoBehaviour
{
    Player player;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
       player = GetComponentInParent<Player>();
       manager = GetComponentInParent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.isFacing = true;
        if(collision.gameObject.GetComponent<Crate>())
        {
            player.GetComponentInChildren<PlayerBack>().stillOnCrate = true;
        }

        if(collision.gameObject.GetComponent<Cratere>() || collision.gameObject.GetComponent<FallingTerrain>())
        {
            player.GetComponentInChildren<PlayerBack>().stillOnCrate = false;
            manager.onCrate = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.isFacing = false;
    }
}
