using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    Player player;
    bool stop;
    //[HideInInspector]
    public Vector2 narrowPosition;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    void OnInteract()
    {
        stop = false;
        if (!stop)
            PassThrough();
    }

   

    void PassThrough()
    {
        //animazione del passaggio
        if (narrowPosition != Vector2.zero && player.isFacing)
        {
            transform.position = narrowPosition;
            stop = true;
        }
    }

}
