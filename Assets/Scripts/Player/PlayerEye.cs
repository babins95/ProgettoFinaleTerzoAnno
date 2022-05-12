using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEye : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
       player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

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
